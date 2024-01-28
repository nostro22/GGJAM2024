using UnityEngine;
using UnityEngine.Events;

public enum GameState {
    WaitingToStart,
    CountdownToStart,
    GamePlaying,
    GameCompleted,
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject interactStarterPlayer;

    [SerializeField] private UnityEvent OnCompletedGame;
    // [SerializeField] private GameObject pausePanel;
    // [SerializeField] private GameObject referencesKeyPausePanel;
    private GameState gameState = GameState.WaitingToStart;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        // referencesKeyPausePanel.SetActive(false);
        EventsManager.OnInteractEventToStartGame.SubscribeMethod(OnInteract);
        EventsManager.OnCountDownToStartLevelTime.SubscribeMethod((() =>
        {
            UpdateGameState(GameState.GamePlaying);
        }));
        OnValueChanged();
    }

    private void OnValueChanged()
    {
        if (gameState == GameState.WaitingToStart)
        {
            Time.timeScale = 1;
            interactStarterPlayer.SetActive(true);
        }
        else if (gameState == GameState.CountdownToStart)
        {
            Time.timeScale = 1;
            interactStarterPlayer.SetActive(false);
        }
        else if (gameState == GameState.GamePlaying)
        {
            Time.timeScale = 1;
            // EventsManager.OnPauseGame.SubscribeMethod(OnPauseGameInteract);
            EventsManager.OnActivateInputs.Invoke();
            EventsManager.OnStartGame.Invoke();
        }
        else if (gameState == GameState.GameCompleted)
        {
            Time.timeScale = 1;
            // EventsManager.OnPauseGame.RemoveOneShotMethod(OnPauseGameInteract);
            EventsManager.OnDeactivateInputs.Invoke();
            EventsManager.OnEndGame.Invoke();
            OnCompletedGame.Invoke();
        }
    }

    private void OnInteract() {
        if (gameState == GameState.WaitingToStart) {

            interactStarterPlayer.SetActive(false);
            SetPlayerReady();
        }
    }
    
    private void SetPlayerReady() {
        gameState = GameState.GamePlaying;
        OnValueChanged();
    }
    
    public bool IsGamePlaying() {
        return gameState == GameState.GamePlaying;
    }

    public bool IsCountdownToStartActive() {
        return gameState == GameState.CountdownToStart;
    }

    public bool IsGameOver() {
        return gameState == GameState.GameCompleted;
    }

    public bool IsWaitingToStart() {
        return gameState == GameState.WaitingToStart;
    }
    
    // public void OnPauseGameInteract()
    // {
    //     if(IsGameOver()) return;
    //     // referencesKeyPausePanel.SetActive(!pausePanel.activeSelf);
    //     pausePanel.SetActive(!pausePanel.activeSelf);
    //     gameState = !pausePanel.activeSelf ? GameState.GamePlaying : GameState.GamePause;
    //     OnValueChanged();
    // }


    public void BackToSelectGame()
    {
        ResetPlayersToManager();
        LoadSceneManager.Instance.LoadNewScene(ScenesIndexes.CHARACTER_SELECT);
    }
    
    public void UpdateGameState(GameState gameGameState)
    {
        gameState = gameGameState;
        OnValueChanged();
    }
    
    private void ResetPlayersToManager()
    {
        PlayerManager.Instance.ResetPlayersParentAndRemoveChild();
    }
}
