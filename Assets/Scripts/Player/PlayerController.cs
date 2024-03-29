using System;
using System.Collections;
using ScriptableObjects;
using UI.Gameplay;
using UnityEngine;

public enum MovementType
{
    Normal,Car,OnlyRotate
}

public class PlayerController : MonoBehaviour,IThrowable
{
    [SerializeField] private PlayerParametersSO playerParameters;
    [SerializeField] private MovementType movementType;
    [SerializeField] private VisualIconMark visualIconGround;
    [SerializeField] private VisualIconMark visualIconArrow;
    public Action OnDashEvent;
    public Action OnStunEvent;
    private bool inDash;
    public int IndexPlayer;
    private PlayerInputController inputController;
    private PlayerInteractableController interactableController;
    private CharacterController characterController;
    private CharacterAnimator characterAnimator;
    private float startTimeDash;
    private float currentSpeed;
    private Vector2 movementInput;
    private Vector3 movement;
    private Vector3 externMovement;
    private Vector3 playerVelocity;
    private IMovable ImovableBuffer;
    private void Awake()
    {
        inputController = GetComponentInParent<PlayerInputController>();
        interactableController = gameObject.GetComponent<PlayerInteractableController>();
        characterController = GetComponent<CharacterController>();
    }
    
    private void Start()
    {
        Initialize();
    }
    

    public void Initialize()
    {
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
        OnDashEvent += characterAnimator.OnDashAnimation;
        OnStunEvent += characterAnimator.OnStunAnimation;
        characterController.enabled = true;
        UpdateMovement();
        EventsManager.OnDeactivateInputs.SubscribeMethod(OnDisableInputs);
        EventsManager.OnActivateInputs.SubscribeMethod(OnEnableInputs);
        if(GameManager.Instance.IsGamePlaying()) OnEnableInputs();
        characterController.detectCollisions = true;
        OnThrow(gameObject);
    }

    public void SetIndexPlayer(int index)
    {
        IndexPlayer = index;
    }

    public void SetColorPlayerBehaviour(Color color)
    {
        visualIconGround.SetColorIcon(color);
        visualIconArrow.SetColorIcon(color);
    }
    public void OnEnableInputs()
    {
        inputController.OnMoveEvent += OnMove;
        inputController.OnDashEvent += OnDash;
    }

    public void OnDisableInputs()
    {
        inputController.OnMoveEvent -= OnMove;
        inputController.OnDashEvent -= OnDash;
        OnMove(Vector2.zero);
    }
    
    private void OnDestroy()
    {   EventsManager.OnDeactivateInputs.RemoveOneShotMethod(OnDisableInputs);
        EventsManager.OnActivateInputs.RemoveOneShotMethod(OnEnableInputs);
    }

    public void UpdateMovement(IMovable imovable = null)
    {
        if (imovable == null)
        {
            ImovableBuffer = null;
            currentSpeed = playerParameters.playerSpeed;
            UpdateMovementType(MovementType.Normal);
        }
        else
        {
            ImovableBuffer = imovable;
            UpdateMovementType(imovable.MovementType);
        }
    }

    public void UpdateMovementType(MovementType newMovementType)
    {
        movementType = newMovementType;
    }

    private void OnMove(Vector2 inputDir)
    {
        movementInput = inputDir;
    }

    private void OnDash()
    {
        if (!inDash && !interactableController.inStun)
        {
            inDash = true;
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        AudioManager.Instance.PlayEffectPlayerDash();
        inDash = true;
        InThrow = inDash;
        OnDashEvent?.Invoke();
        Vector3 dashDirection = transform.forward;
        float dashTimer = 0f;

        while (dashTimer < playerParameters.durationDash)
        {
            float dashSpeed = playerParameters.distanceDash / playerParameters.durationDash;
            characterController.Move(dashDirection * dashSpeed * Time.deltaTime);

            dashTimer += Time.deltaTime;
            yield return null;
        }
        inDash = false;
        InThrow = inDash;
    }

    private void Movement()
    {
        //Movement
        movement = new Vector3(movementInput.x, 0, movementInput.y).normalized;
        switch (movementType)
        {
            case MovementType.Normal:
                characterAnimator.UpdateWalk(movement.magnitude);
                characterController.Move(movement * currentSpeed * Time.deltaTime);
                break;
            case MovementType.OnlyRotate:
                characterAnimator.UpdateWalk(0);
                break;
        }
        
        //Apply Externs Movement Directions if any 
        characterController.Move(externMovement * Time.deltaTime);
        
        //Rotation
        if (movement != Vector3.zero)
            gameObject.transform.forward =  Vector3.Slerp(gameObject.transform.forward,movement,15*Time.deltaTime);
        
        //Apply gravity
        if (characterController.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        playerVelocity.y += playerParameters.gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void Update()
    {
        if (!interactableController.inStun)
        {
            Movement();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("SimpleObjects"))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb)
            {
                Vector3 forceDirection = hit.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();
                rb.AddForceAtPosition(forceDirection, transform.position, ForceMode.Impulse);
            }
        }
    }

    public bool InThrow { get; set; }
    public GameObject PlayerThrow { get; set; }
    public void OnThrow(GameObject playerThrow)
    {
        PlayerThrow = playerThrow;
    }
}

