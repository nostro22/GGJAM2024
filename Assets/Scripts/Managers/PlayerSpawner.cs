using System.Collections;
using UI.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance;

    [Header("Characters References")]
    [SerializeField] private GameType gameType;
    [SerializeField] private CharacterDatabase characterDatabase;
    [SerializeField] private GameObject playerGameplayBehavoiur;
    [Header("Players Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;
    private int indexPlayer = 0;
    // [Header("Initial Reference")]
    // [SerializeField] private VisualInteractableKey visualInteractableKey;
    
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
    }

    private IEnumerator Start()
    {
        EventsManager.OnAddNewInputPlayer.SubscribeMethod(InstanceOnonPlayerJoined);
        if (PlayerManager.Instance.firstPlayer)
            PlayerManager.Instance.firstPlayer.GetComponent<PlayerInputController>().OnInteractEvent += OnFirstInteract;
        yield return new WaitForSeconds(0.1f);
        foreach (var basePlayer in PlayerManager.Instance.players)
            InstanceOnonPlayerJoined(basePlayer.GetComponent<PlayerInput>());
        if(gameType == GameType.boom)
            AudioManager.Instance.PlayMusicTraffic();
        if (gameType == GameType.football)
        {
            AudioManager.Instance.PlayMusicFootball();
            AudioManager.Instance.PlayMusicAmbientFootball();
        }
        
    }

    private void InstanceOnonPlayerJoined(PlayerInput obj)
    {
        if (obj.user.id == 1)
        {
            // visualInteractableKey.Show();
            obj.GetComponent<PlayerInputController>().OnInteractEvent += OnFirstInteract;
        }
        obj.SwitchCurrentActionMap("Gameplay");
        var characterSelecState = PlayerManager.Instance.GetPlayerCharacterSelectedForID(obj.user.id);
        var playerGameplayeBehaviour =Instantiate(playerGameplayBehavoiur, obj.transform,false);
        obj.transform.SetParent(null);
        SceneManager.MoveGameObjectToScene(obj.gameObject,SceneManager.GetActiveScene());
        obj.transform.SetAsLastSibling();
        SetPlayerPositionSpawnAndCharacter(playerGameplayeBehaviour, characterSelecState);
        EnablePause();
    }

    public void DisablePause()
    {
        foreach (var basePlayer in PlayerManager.Instance.players)
            basePlayer.GetComponent<PlayerInputController>().OnPauseEvent -= OnPauseInteract;
    }

    public void EnablePause()
    {
        
        foreach (var basePlayer in PlayerManager.Instance.players)
            basePlayer.GetComponent<PlayerInputController>().OnPauseEvent += OnPauseInteract;
    }
    private void SetPlayerPositionSpawnAndCharacter(GameObject playerBehaviour,CharacterSelectState selectState)
    {
        var character = characterDatabase.GetCharacterById(selectState.CharacterId);
        if (character != null)
        {
            playerBehaviour.transform.position = spawnPoints[indexPlayer].position;
            var characterInstantiate = Instantiate(character.GameplayPrefab,Vector3.zero, Quaternion.identity);
            characterInstantiate.transform.SetParent(playerBehaviour.transform,false);
            indexPlayer++;
            var colorPlayer = UtilsPlayer.GetColoByIndex(indexPlayer,gameType);
            playerBehaviour.GetComponent<PlayerController>().SetIndexPlayer(indexPlayer);
            playerBehaviour.GetComponent<PlayerController>().SetColorPlayerBehaviour(colorPlayer);
        }
    }
    
    private void OnFirstInteract()
    {
        if (GameManager.Instance.IsWaitingToStart())
        {
            EventsManager.OnInteractEventToStartGame.Invoke();
            PlayerManager.Instance.firstPlayer.GetComponent<PlayerInputController>().OnInteractEvent -= OnFirstInteract;
        }
    }


    private void RemoveOnPauseEventToPlayers()
    {
        EventsManager.OnAddNewInputPlayer.RemoveOneShotMethod(InstanceOnonPlayerJoined);
        DisablePause();
    }

    private void OnDestroy()
    {
        RemoveOnPauseEventToPlayers();
    }

    private void OnPauseInteract()
    {
        EventsManager.OnPauseGame.Invoke();
    }
}


