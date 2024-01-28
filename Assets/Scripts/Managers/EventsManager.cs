using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventsManager
{
    #region Generals
    public static CustomRepeatEvent OnCompleteTimerEventObject;
    public static CustomUniqueEvent OnSafeTime;
    public static CustomUniqueEvent OnExitArea;
    public static CustomUniqueEvent OnCountDownToStartLevelTime;
    public static CustomUniqueEvent OnCountDownToEndLevelTime;
    public static CustomUniqueEvent OnStartGame;
    public static CustomUniqueEvent OnInteractEventToStartGame;
    public static CustomUniqueEvent OnEndGame;
    public static CustomRepeatEvent<PlayerInput> OnAddNewInputPlayer;
    public static CustomRepeatEvent<PlayerInput> OnRemoveNewInputPlayer;
    public static CustomRepeatEvent<PlayerInput> OnPlayerChangeScheme;
    public static CustomRepeatEvent OnPauseGame;
    public static CustomUniqueEvent OnInteractEventToMainMenuGame;
    public static CustomUniqueEvent OnEndChangeScene;
    public static CustomUniqueEvent OnStartChangeScene;
    public static CustomRepeatEvent OnActivateInputs;
    public static CustomRepeatEvent OnDeactivateInputs;
    #endregion
    
    static EventsManager()
    {
        CreateCustomEvents();
    }

    private static void CreateCustomEvents()
    {
        OnCountDownToEndLevelTime = new CustomUniqueEvent();
        OnCountDownToStartLevelTime = new CustomUniqueEvent();
        OnActivateInputs = new CustomRepeatEvent();
        OnDeactivateInputs = new CustomRepeatEvent();
        OnExitArea = new CustomUniqueEvent();
        OnCompleteTimerEventObject = new CustomRepeatEvent();
        OnSafeTime = new CustomUniqueEvent();
        OnEndChangeScene = new CustomUniqueEvent();
        OnStartChangeScene = new CustomUniqueEvent();
        OnPauseGame = new CustomRepeatEvent();
        OnAddNewInputPlayer = new CustomRepeatEvent<PlayerInput>();
        OnRemoveNewInputPlayer = new CustomRepeatEvent<PlayerInput>();
        OnEndGame = new CustomUniqueEvent();
        OnStartGame = new CustomUniqueEvent();
        OnInteractEventToStartGame = new CustomUniqueEvent();
        OnInteractEventToMainMenuGame = new CustomUniqueEvent();
        OnPlayerChangeScheme = new CustomRepeatEvent<PlayerInput>();
    }
    
    public static void OnDestroy()
    {
        // Creates new events to clear the old ones
        CreateCustomEvents();
    }
}