using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterSelectDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerCard[] playerCards;
    [SerializeField] private Button lockInButton;
    [SerializeField] private InputActionProperty joinActionProperty;
    private void Awake()
    {
        lockInButton.onClick.AddListener(GoToWorkButton);
        EventsManager.OnAddNewInputPlayer.SubscribeMethod(SetAddNewInputPlayer);
        EventsManager.OnRemoveNewInputPlayer.SubscribeMethod(RemovePlayerCardWithPlayerInput);
        foreach (var playerCard in playerCards)
            playerCard.DisableDisplay();
    }

    private void Start()
    {
        PlayerInputManager.instance.joinBehavior = PlayerJoinBehavior.JoinPlayersWhenJoinActionIsTriggered;
        PlayerInputManager.instance.joinAction = joinActionProperty;
        PlayerInputManager.instance.EnableJoining();
        foreach (var basePlayer in PlayerManager.Instance.players)
        {
            var playerInput = basePlayer.GetComponent<PlayerInput>();
            SetAddNewInputPlayer(playerInput);
        }
        PlayerManager.Instance.AddPlayersEvents();
        EventsManager.OnStartChangeScene.SubscribeMethod((() =>
        {
            PlayerManager.Instance.firstPlayer.GetComponent<PlayerInputController>().OnExitEvent += BackScene;
        }));
    }
    
    private void BackScene(PlayerInput playerInput)
    {
        PlayerManager.Instance.firstPlayer.GetComponent<PlayerInputController>().OnExitEvent -= BackScene;
        EventsManager.OnEndChangeScene.SubscribeMethod(ResetCharactersWithPlayers);
        LoadSceneManager.Instance.LoadNewScene(ScenesIndexes.MAIN_MENU);
    }

    private void ResetCharactersWithPlayers()
    {
        PlayerManager.Instance.ResetToCharacterSelectScene();
    }

    private void GoToWorkButton()
    {
        foreach (var playerCard in playerCards)
            playerCard.RemoveInputCard();
        LoadSceneManager.Instance.LoadNewScene(ScenesIndexes.SELECTGAME);
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.RemovePlayersEvents();
        EventsManager.OnAddNewInputPlayer.RemoveOneShotMethod(SetAddNewInputPlayer);
        EventsManager.OnRemoveNewInputPlayer.RemoveOneShotMethod(RemovePlayerCardWithPlayerInput);
    }

    private void SetAddNewInputPlayer(PlayerInput playerInput)
    {
        SetPlayerCardWithPlayerInput(playerInput);
    }

    private void SetPlayerCardWithPlayerInput(PlayerInput playerInput)
    {
        var user = playerInput.user;
        if (playerCards[user.id - 1].playerInput) return;
        playerCards[user.id-1].EnableDisplay(playerInput);
        playerCards[user.id-1].UpdateDisplay(PlayerManager.Instance.GetPlayerCharacterSelectedForID(user.id));
    }

    private void RemovePlayerCardWithPlayerInput(PlayerInput playerInput)
    {
        var user = playerInput.user;
        if (!playerCards[user.id - 1].playerInput) return;
        playerCards[user.id-1].RemoveInputCard();
        playerCards[user.id-1].DisableDisplay();
    }

}
