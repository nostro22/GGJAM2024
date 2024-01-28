using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.Gameplay
{
    public class VisualInteractableKey : MonoBehaviour
    {
        [SerializeField] private GameObject visualKey;
        [SerializeField] private Image iconKey;
        [SerializeField] private bool hideOnStart = true;
        [SerializeField] private VisualInteractableKeySO visualInteractableKeySO;
        [SerializeField] private bool IsOnlyCurrentPlayer;
        public InputActionReference actionReference;
        private TextMeshProUGUI textMeshProUGUI;
        private string deviceLayoutName = String.Empty;
        private string controlPath = String.Empty;
        private string displayString = String.Empty;
        private void Awake()
        {
            textMeshProUGUI = iconKey.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            SetActionSprite();
            if (hideOnStart) Hide();
        }

        public void SetActionSprite(PlayerInput playerInput = null)
        {
            if (actionReference != null)
            {
                InputAction action = actionReference.action;

                if (action != null)
                {
                    if (playerInput != null)
                    {
                        var bindingIndex = action.bindings.IndexOf(x => x.effectivePath.Contains(playerInput.currentControlScheme));

                        if (bindingIndex != -1)
                        {
                            displayString = action.GetBindingDisplayString(bindingIndex, out deviceLayoutName, out controlPath,InputBinding.DisplayStringOptions.DontUseShortDisplayNames);
                        }
                            
                        UpdateVisibleIcon();
                        
                    }
                    
                    if(IsOnlyCurrentPlayer) return;
   
                    if (PlayerManager.Instance.firstPlayer)
                    {
                        var playerFirstInput = PlayerManager.Instance.firstPlayer.GetComponent<PlayerInput>();
                        var bindingIndex = action.bindings.IndexOf(x => x.effectivePath.Contains(playerFirstInput.currentControlScheme));
                        
                        if (bindingIndex != -1)
                        {
                            displayString = action.GetBindingDisplayString(bindingIndex, out deviceLayoutName, out controlPath);
                        }
                            
                        UpdateVisibleIcon();
                    }
                    
                }
                else
                {
                    Debug.LogError("The Action Reference does not reference a valid action.");
                }
            }
            else
            {
                Debug.LogError("The Action Reference is not assigned.");
            }
        }

        private void UpdateVisibleIcon()
        {
            var icon = default(Sprite);
            if (InputSystem.IsFirstLayoutBasedOnSecond(deviceLayoutName, "DualShockGamepad"))
            {
                if (textMeshProUGUI != null)
                    textMeshProUGUI.gameObject.SetActive(false);
                icon = visualInteractableKeySO.Ps4.GetSprite(controlPath);
                if (icon)
                    iconKey.sprite = icon;
            }
            else if (InputSystem.IsFirstLayoutBasedOnSecond(deviceLayoutName, "Gamepad"))
            {
                if (textMeshProUGUI != null)
                    textMeshProUGUI.gameObject.SetActive(false);
                icon = visualInteractableKeySO.Xbox.GetSprite(controlPath);
                if (icon)
                    iconKey.sprite = icon;
            }
            else if (InputSystem.IsFirstLayoutBasedOnSecond(deviceLayoutName, "Keyboard"))
            {
                if (textMeshProUGUI != null)
                    textMeshProUGUI.gameObject.SetActive(false);
                icon = visualInteractableKeySO.Keyboard.GetSprite(controlPath);
                if (icon)
                    iconKey.sprite = icon;
            }
            else
            {
                if (textMeshProUGUI != null)
                {
                    textMeshProUGUI.gameObject.SetActive(true);
                    textMeshProUGUI.text = displayString;
                    iconKey.sprite = visualInteractableKeySO.BackgroundSpriteForKeyboard;
                }
            }
        }

        public void Show()
        {
            visualKey.SetActive(true);
        }
        
        public void Show(PlayerInput playerInput)
        {
            IsOnlyCurrentPlayer = true;
            SetActionSprite(playerInput);
            visualKey.SetActive(true);
        }

        public void Hide()
        {
            visualKey.SetActive(false);
        }
    }
}

