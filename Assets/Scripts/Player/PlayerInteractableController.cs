using System;
using System.Collections;
using ScriptableObjects;
using UI.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using VisualText = UI.Gameplay.VisualText;

public class PlayerInteractableController : MonoBehaviour,IParentObject
{
    public bool inStun;
    private PlayerInputController inputController;
    private PlayerController playerController;
    private CharacterAnimator characterAnimator;
    private GameObject objectToDrag;
    private Collider[] collidersBuffer2;
    private Collider[] collidersBuffer3;
    private bool isCollideWithWall;
    private bool isPossibleThrow;
    [SerializeField] private PlayerParametersSO playerParametersSo;
    [SerializeField] private Transform pointToDropInteractuables;
    [SerializeField] private ParticleSystem particleStun;
    [SerializeField] private Transform originRay;
    [FormerlySerializedAs("originBoxObstaclesCast")] [SerializeField] private Transform originObstaclesCast;
    [SerializeField] private float radiusObstacleCast;
    [FormerlySerializedAs("originBoxCleaningCast")] [SerializeField] private Transform originDragCast;
    [SerializeField] private float radiusDragCast;
    [SerializeField] private GameObject arrowDashDirection;
    #region Interactuables
    private GameObject objectDragged;
    #endregion
    #region LayersMask
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private LayerMask HitsAreaLayerMask;
    [SerializeField] private LayerMask NonInteractableLayerMask;
    #endregion
    private VisualText visualText;

    private void Start()
    {
        visualText = GetComponent<VisualText>();
        inputController = GetComponentInParent<PlayerInputController>();
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
        Parent = pointToDropInteractuables;
        collidersBuffer2 = new Collider[2];
        collidersBuffer3 = new Collider[2];
        InitializePlayer();
    }

    public void InitializePlayer()
    {
        playerController = GetComponent<PlayerController>();
        playerController.OnStunEvent += OnDropObject;
        EventsManager.OnDeactivateInputs.SubscribeMethod(OnDisableInputs);
        EventsManager.OnActivateInputs.SubscribeMethod(OnEnableInputs);
        if(GameManager.Instance.IsGamePlaying()) OnEnableInputs();
        interactableLayerMask = playerParametersSo.defaultInteractLayer;
        arrowDashDirection.SetActive(false);
    }
    private void OnDestroy()
    {   EventsManager.OnDeactivateInputs.RemoveOneShotMethod(OnDisableInputs);
        EventsManager.OnActivateInputs.RemoveOneShotMethod(OnEnableInputs);
    }
    
    private void OnEnableInputs()
    {
        inputController.OnInteractEvent += OnInteract;
        inputController.OnCancelInteractEvent += OnCancelInteract;
        inputController.OnStartThrowEvent += StartThrow;
        inputController.OnExitThrowEvent += ExitThrow;
        inputController.OnUseEvent += OnUse;
        inputController.OnCancelUseEvent += OnCancelUse;
    }

    private void OnDisableInputs()
    {
        inputController.OnInteractEvent -= OnInteract;
        inputController.OnCancelInteractEvent -= OnCancelInteract;
        inputController.OnStartThrowEvent -= StartThrow;
        inputController.OnExitThrowEvent -= ExitThrow;
        inputController.OnUseEvent -= OnUse;
        inputController.OnCancelUseEvent -= OnCancelUse;
        OnCancelUse();
    }

    private void OnInteract()
    {
        if (!objectToDrag) return;
        if(objectDragged) OnDropObject();
        OnDragObject(objectToDrag);
    }

    private void OnCancelInteract()
    {
        if(objectDragged != null) OnDropObject();
    }

    private void OnUse()
    {
        
    }

    private void OnCancelUse()
    {
        
    }
    
    private void StartThrow()
    {
        if (objectDragged != null) OnStartThrowObject();
    }
    
    private void ExitThrow()
    {
        if (objectDragged != null) OnExitThrowObject();
    }
    

    private void FixedUpdate()
    {
        CheckRaycastInteractable();
    }

    private void CheckRaycastInteractable()
    {
        #region Check Hits and Throwables Objects
        var objectsFounded2 =  Physics.OverlapBoxNonAlloc(transform.position,Vector3.one*1.1f/2,collidersBuffer2,transform.rotation,HitsAreaLayerMask);
        if (objectsFounded2 != 0)
        {
            for (int i = 0; i < objectsFounded2; i++)
            {
                var collider = collidersBuffer2[i];
                if (collider.TryGetComponent<IThrowable>(out var oThrowable))
                {
                    if (!inStun)
                    {
                        if (oThrowable.InThrow && oThrowable.PlayerThrow != gameObject)
                        {
                            inStun = true;
                            if(collider.TryGetComponent<Rigidbody>(out var rigidbody))
                                rigidbody.velocity = Vector3.zero;
                            StunEffect();
                            StartCoroutine(ResetStun());   
                        }        
                    }    
                }
            }
        }
        #endregion
        #region CheckObstacles
        isPossibleThrow = Physics.Raycast(originRay.position, originRay.forward,1.4f, NonInteractableLayerMask);
        #endregion
        #region Check Objects Draggables
        var objectsFounded3 =  Physics.OverlapSphereNonAlloc(originDragCast.position+transform.forward,radiusDragCast,collidersBuffer3,interactableLayerMask);

        if (objectsFounded3 > 0)
        {
            if (objectToDrag != collidersBuffer3[0].gameObject)
            {
                if (collidersBuffer3[0].TryGetComponent<IDraggable>(out var interactable))
                {
                    if (objectToDrag)
                    {
                        var oldVisualkey = objectToDrag.GetComponentInChildren<VisualInteractableKey>();
                        if (oldVisualkey) oldVisualkey.Hide();
                        objectToDrag = null;   
                    }
                    objectToDrag = collidersBuffer3[0].gameObject;
                    var newVisualkey = objectToDrag.GetComponentInChildren<VisualInteractableKey>();
                    if (newVisualkey) newVisualkey.Show(GetComponentInParent<PlayerInput>()); 
                }
            }
        }
        else
        {
            if (objectToDrag)
            {
                var oldVisualkey = objectToDrag.GetComponentInChildren<VisualInteractableKey>();
                if (oldVisualkey) oldVisualkey.Hide();
                objectToDrag = null;   
            }   
        }
        #endregion
    }
    
    private void StunEffect()
    {
        playerController.OnStunEvent?.Invoke();
        AudioManager.Instance.PlayEffectPlayerStun();
        if(visualText) visualText.Show(VisualTextType.Stun);
        particleStun.Play();
    }
    public IEnumerator ResetStun()
    {
        yield return new WaitForSeconds(playerParametersSo.timeDelayStun);
        if(visualText) visualText.Hide();
        inStun = false;
    }

    private void OnDrawGizmos()
    {
        //Visual Ray
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(originRay.position,originRay.forward*1.4f);
        //Visual CheckBox Obstacles
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(originObstaclesCast.position+transform.forward,radiusObstacleCast);
        //Visual Draggable Objects
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(originDragCast.position+transform.forward,radiusDragCast);
        //Visual Hits and Throwables Objects
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position,Vector3.one*1.1f);
    }
    
    private void OnDragObject(GameObject gameObject)
    {
        objectDragged = gameObject;
        if (arrowDashDirection.activeSelf)
        {
            playerController.UpdateMovementType(MovementType.Normal);
            arrowDashDirection.SetActive(false);
        }
        var newVisualkey = objectToDrag.GetComponentInChildren<VisualInteractableKey>();
        if (newVisualkey) newVisualkey.Hide();
        SetIKConstraints();
        objectToDrag.TryGetComponent<IDraggable>(out var draggable);
        draggable.OnDrag();
        AudioManager.Instance.PlayEffectPlayerDrag();
       objectToDrag = null;
    }

    private void SetIKConstraints()
    {
        if (objectToDrag.TryGetComponent<AnimationTargetConstraintIK>(out var targetConstraint))
        {
            if (objectToDrag.TryGetComponent<IParentableObject>(out var parentableObject))
                parentableObject.SetParentObject(characterAnimator.gameObject);
        }
        else
        {
            if (objectToDrag.TryGetComponent<IParentableObject>(out var parentableObject))
                parentableObject.SetParentObject(gameObject);
        }
    }
    
    private void RemoveIKConstraints()
    {
        if(objectDragged.TryGetComponent<IParentableObject>(out var parentableObject))
            parentableObject.RemoveParentObject();
        objectDragged.transform.position = isCollideWithWall
            ? transform.position
            : pointToDropInteractuables.position;
    }
    
    private void OnDropObject()
    {
        if(!objectDragged) return;
            RemoveIKConstraints();
            objectDragged.TryGetComponent<IDraggable>(out var draggable);
            draggable.OnDrop();
            if (arrowDashDirection.activeSelf)
            {
                
                arrowDashDirection.SetActive(false);
            }
            objectDragged.SetActive(true);
            objectDragged = null;
            AudioManager.Instance.PlayEffectPlayerDrop();
            interactableLayerMask = playerParametersSo.defaultInteractLayer;   
    }
    
    private void OnStartThrowObject()
    {
            if (objectDragged.TryGetComponent<IThrowable>(out var throwable))
            {

                arrowDashDirection.SetActive(true);
            }
            else
            {
                OnDropObject();
            }
    }
    
    private void OnExitThrowObject()
    {
            arrowDashDirection.SetActive(false);
            if (objectDragged.TryGetComponent<IThrowable>(out var throwable) && !isPossibleThrow)
            {
                RemoveIKConstraints();
                objectDragged.SetActive(true);
                throwable.OnThrow(gameObject);
                objectDragged = null;
                AudioManager.Instance.PlayEffectPlayerThrow();
                interactableLayerMask = playerParametersSo.defaultInteractLayer;
            }
            else
            {
                OnDropObject();
            }
    }
    
    
    private void DisableObjectDragged()
    {
        if (objectDragged)
        {
            objectDragged.SetActive(false);
        }
    }
    
    private void ActiveObjectDragged()
    {
        if (objectDragged)
        {
            objectDragged.SetActive(true);
        }
    }

    public Transform Parent { get; set; }
}