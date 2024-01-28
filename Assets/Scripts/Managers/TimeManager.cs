using ScriptableObjects;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textStartCounter;
    [SerializeField] private TextMeshProUGUI textPlayingCounter;
    [SerializeField] private TimerSO timerSO;
    private float countdownToStartTimer;
    private float gamePlayingTimer;
    private bool isSafeTime;
    public static TimeManager Instance { get; private set; }

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
        
        EventsManager.OnCountDownToStartLevelTime.SubscribeMethod((() =>
        {
            textStartCounter.gameObject.SetActive(false);
            if(timerSO.TotalPlayingTimer!=0) textPlayingCounter.transform.parent.gameObject.SetActive(true);
        }));
        EventsManager.OnInteractEventToStartGame.SubscribeMethod((() =>
        {
            textStartCounter.gameObject.SetActive(true);
        }));
    }

    void Start()
    {
        gamePlayingTimer = timerSO.TotalPlayingTimer;
        countdownToStartTimer = timerSO.CountdownToStartTimer;
        textStartCounter.gameObject.SetActive(false);
        if(timerSO.TotalPlayingTimer!=0) textPlayingCounter.transform.parent.gameObject.SetActive(false);
    }

    
    void Update()
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            UpdateStartTimerText();
            countdownToStartTimer -= Time.deltaTime;
            if (countdownToStartTimer < 0f) {
                EventsManager.OnCountDownToStartLevelTime.Invoke();
            }
        }
        else if (GameManager.Instance.IsGamePlaying() && timerSO.TotalPlayingTimer != 0)
        {
            UpdatePlayingTimerText();
            gamePlayingTimer -= Time.deltaTime;
            if (gamePlayingTimer < 0f) {
                EventsManager.OnCountDownToEndLevelTime.Invoke();
            }
                
            if (gamePlayingTimer <= timerSO.SafePlayingTimer && !isSafeTime)
            {
                isSafeTime = true;
                EventsManager.OnSafeTime.Invoke();
            }   
        }
    }
    
    public bool IsSafeTime()
    {
        return isSafeTime;
    }
    
    private void UpdatePlayingTimerText()
    {
        int minutes = Mathf.FloorToInt(gamePlayingTimer / 60f);
        int seconds = Mathf.FloorToInt(gamePlayingTimer % 60f);
        textPlayingCounter.text =string.Format("{00}:{1:00}", minutes, seconds);
    }
    

    private void UpdateStartTimerText()
    {
        textStartCounter.text = string.IsNullOrEmpty(timerSO.TextToAppearInStartTime) ? countdownToStartTimer.ToString("0") : timerSO.TextToAppearInStartTime;
    }
    
    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }

    public float GetCurrentPlayingTimer()
    {
        return gamePlayingTimer;
    }
}
