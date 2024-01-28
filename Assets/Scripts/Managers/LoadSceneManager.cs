using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager Instance { get; private set; }
    [SerializeField] private GameObject panelChangeLevel;
    [SerializeField] private GameObject loadingIcon;
    [SerializeField] private ScenesIndexes sceneLoad = ScenesIndexes.NONE;
    private AsyncOperation scenesLoading = new AsyncOperation();
    private bool isLoadingScene;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadNewScene(ScenesIndexes.MAIN_MENU);
    }
    
    private void EnterPanel()
    {
        LeanTween.alpha(loadingIcon.GetComponent<RectTransform>(), 0f, 0.2f).setOnComplete(()=>
        {
            isLoadingScene = false;
            EventsManager.OnStartChangeScene.Invoke();
            LeanTween.scale(panelChangeLevel.GetComponent<RectTransform>(), Vector3.zero, 0.3f);
        });
    }

    private void ExitPanel()
    {
        LeanTween.scale(panelChangeLevel.GetComponent<RectTransform>(), Vector3.one * 5, 0.3f).setOnStart(() =>
        {
            isLoadingScene = true;
            panelChangeLevel.SetActive(true);
        }).setOnComplete(() =>
        {
            EventsManager.OnEndChangeScene.Invoke();
            LeanTween.alpha(loadingIcon.GetComponent<RectTransform>(), 1f, 0.2f);
        });
    }

    public void LoadNewScene(ScenesIndexes sceneLoad)
    { 
        if(isLoadingScene) return;
        if (EventSystem.current)
            EventSystem.current.SetSelectedGameObject(null);
        this.sceneLoad = sceneLoad;
        StartCoroutine(LoadAsynchronously());
    }
    
    public void ReloadCurrentScene()
    {
        if(isLoadingScene) return;
        if (EventSystem.current)
            EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        ExitPanel();
        yield return new WaitForSeconds(1f);
        scenesLoading = (SceneManager.LoadSceneAsync((int)sceneLoad));
        while (!scenesLoading.isDone)
        {
            LeanTween.rotateAround(loadingIcon, Vector3.forward, 360, 5);
            yield return new WaitForEndOfFrame();
        }
        EnterPanel();
    }
}


public enum ScenesIndexes
{
    BOOT,
    MAIN_MENU,
    CHARACTER_SELECT,
    SELECTGAME,
    BOOM,
    FOOTBALL,
    NONE
}
