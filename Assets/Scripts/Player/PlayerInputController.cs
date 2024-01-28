using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    #region GameplayEvents
    public event Action<Vector2> OnMoveEvent;
    public event Action OnDashEvent;
    public event Action OnUseEvent;
    public event Action OnCancelUseEvent;
    public event Action OnInteractEvent;
    public event Action OnCancelInteractEvent;
    public event Action OnStartThrowEvent;
    public event Action OnExitThrowEvent;
    public event Action OnOpenPopupsEvent;
    public event Action OnCancelOpenPopupsEvent;
    #endregion

    #region CharacterSelectorUI
    public event Action<Vector2> OnSelectEvent;
    public event Action<PlayerInput> OnEnterEvent;
    public event Action<PlayerInput> OnExitEvent;
    #endregion
    
    #region PauseUI
    public event Action OnPauseEvent;
    #endregion

    #region StartUI
    public event Action OnStartEvent;
    #endregion
    
    public void OnSelectCharacter(InputAction.CallbackContext context)
    {
        OnSelectEvent?.Invoke(context.ReadValue<Vector2>());
    }
    
    public void OnEnterCharacter(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnEnterEvent?.Invoke(GetComponent<PlayerInput>());
        }
    }
    public void OnExitCharacter(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnExitEvent?.Invoke(GetComponent<PlayerInput>());
        }
    }
    
    public void OnStart(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnStartEvent?.Invoke();
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
    
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnPauseEvent?.Invoke();
        }
    }
    
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnDashEvent?.Invoke();
        }
    }
    
    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnUseEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            OnCancelUseEvent?.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnInteractEvent?.Invoke();
        }
    }

    public void OnCancelInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnCancelInteractEvent?.Invoke();
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnStartThrowEvent?.Invoke();
        }
        
        if (context.phase == InputActionPhase.Canceled)
        {
            OnExitThrowEvent?.Invoke();
        }
    }

    public void OnOpenPopup(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnOpenPopupsEvent?.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            OnCancelOpenPopupsEvent?.Invoke();
        }
    }

    public void ActiveHaptics()
    {
        var gamepad = GetComponentInParent<PlayerInput>().GetDevice<Gamepad>();
        if (gamepad == null) return;
        gamepad.SetMotorSpeeds(0.1f,0.1f);
    }

    public void DisactiveHaptics()
    {
        var gamepad = GetComponentInParent<PlayerInput>().GetDevice<Gamepad>();
        if (gamepad == null) return;
        gamepad.SetMotorSpeeds(0f,0f);
    }
}
