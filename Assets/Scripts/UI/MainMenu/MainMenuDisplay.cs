using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private InputActionProperty joinActionProperty;
    private void Start()
    {
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        startPanel.SetActive(false);
        // AudioManager.Instance.PlayMusicMainMenu();
        PlayerInputManager.instance.DisableJoining();
        if (PlayerManager.Instance.firstPlayer) return;
        menuPanel.SetActive(false);
        startPanel.SetActive(true);
        EventsManager.OnStartChangeScene.SubscribeMethod(() =>
        {
            EventsManager.OnInteractEventToMainMenuGame.SubscribeMethod(ShowMainMenu);
            PlayerInputManager.instance.joinAction = joinActionProperty;
            PlayerInputManager.instance.joinBehavior = PlayerJoinBehavior.JoinPlayersWhenJoinActionIsTriggered;
            PlayerInputManager.instance.EnableJoining();
        });
    }


    private void ShowMainMenu()
    {
        menuPanel.SetActive(true);
        startPanel.SetActive(false);
        PlayerInputManager.instance.DisableJoining();
        EventsManager.OnInteractEventToMainMenuGame.RemoveOneShotMethod(ShowMainMenu);
    }

    public void PlayButton()
    {
        LoadSceneManager.Instance.LoadNewScene(ScenesIndexes.CHARACTER_SELECT);
        menuPanel.SetActive(false);
    }
    

    public void Options()
    {
        optionsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }
    
    public void QuitButton()
    {
        Application.Quit();
    }
}
