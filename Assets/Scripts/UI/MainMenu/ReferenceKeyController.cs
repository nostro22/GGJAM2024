using System;
using ScriptableObjects;
using UI.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;
using VisualText = UI.Gameplay.VisualText;

namespace UI.MainMenu
{
    public class ReferenceKeyController : MonoBehaviour
    {
        [SerializeField] private VisualTextSO visualTextSO;
        [SerializeField] private VisualText[] visualTexts;
        [SerializeField] private VisualTextType[] visualTextTypes;
        [SerializeField] private VisualInteractableKey[] visualInteractableKeys;
        private bool isInitialized;
        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            if (PlayerManager.Instance.firstPlayer)
            {
                UpdateScheme(PlayerManager.Instance.firstPlayer.GetComponent<PlayerInput>());
            }
            else
            {
                EventsManager.OnPlayerChangeScheme.SubscribeMethod(UpdateScheme);
            }
        }

        private void OnDestroy()
        {
            EventsManager.OnPlayerChangeScheme.RemoveOneShotMethod(UpdateScheme);
        }

        private void UpdateScheme(PlayerInput playerInput)
        {
            if(!isInitialized) Initialize();
            foreach (var visualKey in visualInteractableKeys)
            {
                visualKey.Show();
                visualKey.SetActionSprite(playerInput);
            }
        }

        private void Initialize()
        {
            for (var index = 0; index < visualTextTypes.Length; index++)
            {
                var visualText = visualTexts[index];
                visualText.Set(visualTextSO);
                visualText.Show(visualTextTypes[index]);
            }
            isInitialized = true;
        }
    }
}