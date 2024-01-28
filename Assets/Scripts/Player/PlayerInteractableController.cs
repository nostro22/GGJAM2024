using System.Collections;
using ScriptableObjects;
using UI.Gameplay;
using UnityEngine;
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
    #region Interactuables
    private GameObject objectDragged;
    #endregion
    #region LayersMask
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private LayerMask HitsAreaLayerMask;
    [SerializeField] private LayerMask NonInteractableLayerMask;
    #endregion

    private bool isAttack;
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
        playerController.OnAttackEvent?.Invoke();
        isAttack = true;
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
        #region Check Attack
        var objectsFounded3 =  Physics.OverlapSphereNonAlloc(originDragCast.position+transform.forward,radiusDragCast,collidersBuffer3,interactableLayerMask);

        if (objectsFounded3 > 0)
        {
            if (isAttack)
            {
                if (objectToDrag != collidersBuffer3[0].gameObject)
                {
                    if (collidersBuffer3[0].CompareTag("SimpleObjects"))
                    {
                        Rigidbody rb = collidersBuffer3[0].attachedRigidbody;
                        if (rb)
                        {
                            Vector3 forceDirection = collidersBuffer3[0].transform.position - transform.position;
                            forceDirection.y = 0;
                            forceDirection.Normalize();
                            rb.AddForceAtPosition(forceDirection, transform.position, ForceMode.Impulse);
                        }
                    }

                    if (collidersBuffer3[0].CompareTag("Player") && collidersBuffer3[0].gameObject != gameObject)
                    {
                        var playerToCollide = collidersBuffer3[0].GetComponent<PlayerInteractableController>();
                        if (!playerToCollide.inStun)
                        {
                            playerToCollide.inStun = true;
                            playerToCollide.StunEffect();
                        }
                    }
                }

                isAttack = false;
            }
        }
        #endregion
    }
    
    public void StunEffect()
    {
        Debug.Log("Stun");
        playerController.OnStunEvent?.Invoke();
        // AudioManager.Instance.PlayEffectPlayerStun();
        if(visualText) visualText.Show(VisualTextType.Stun);
        particleStun.Play();
        StartCoroutine(ResetStun());
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
        //Visual Draggable Objects
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(originDragCast.position+transform.forward,radiusDragCast);
    }
    
    private void OnDragObject(GameObject gameObject)
    {
        objectDragged = gameObject;
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
            objectDragged.SetActive(true);
            objectDragged = null;
            AudioManager.Instance.PlayEffectPlayerDrop();
            interactableLayerMask = playerParametersSo.defaultInteractLayer;   
    }

    private void OnStartThrowObject()
    {
    }

    private void OnExitThrowObject()
    {
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