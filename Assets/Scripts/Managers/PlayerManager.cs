using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField] private Transform playerHolder;
    [SerializeField] private GameObject playerPrefab;
    public List<GameObject> players;
    public GameObject firstPlayer;
    public bool DebugMode;
    private Dictionary<ulong,CharacterSelectState> playersCharacterSelectStates;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        PlayerInputManager.instance.onPlayerJoined += InstanceOnPlayerJoined;
        playersCharacterSelectStates = new Dictionary<ulong, CharacterSelectState>();
        PlayerInputManager.instance.playerPrefab = playerPrefab;
    }

    private void InstanceOnPlayerJoined(PlayerInput obj)
    {
        obj.transform.SetParent(playerHolder,false);
        obj.name = $"Player {obj.user.id}";
        SetPlayerWithPlayerInput(obj);
        if(obj.user.id == 1)
        {
            firstPlayer = obj.gameObject;
            EventsManager.OnPlayerChangeScheme.Invoke(obj);
            EventsManager.OnInteractEventToMainMenuGame.Invoke();
        }
        else
        {
            obj.GetComponent<PlayerInputController>().OnEnterEvent += SetPlayerWithPlayerInput;
            obj.GetComponent<PlayerInputController>().OnExitEvent += RemovePlayerWithPlayerInput;   
        }
    }

    public void ResetToCharacterSelectScene()
    {
        EventsManager.OnRemoveNewInputPlayer.Invoke(firstPlayer.GetComponent<PlayerInput>());
        for (var index = 0; index < players.Count; index++)
        {
            var player = players[index];
            if (player.GetComponent<PlayerInput>().user.id == 1) continue;
            RemovePlayerWithPlayerInput(player.GetComponent<PlayerInput>());
        }
    }

    public void RemovePlayersEvents()
    {
        foreach (var player in players)
        {
            if (player.GetComponent<PlayerInput>().user.id == 1) continue;
            player.GetComponent<PlayerInputController>().OnEnterEvent -= SetPlayerWithPlayerInput;
            player.GetComponent<PlayerInputController>().OnExitEvent -= RemovePlayerWithPlayerInput;
        }
    }

    public void AddPlayersEvents()
    {
        foreach (var player in players)
        {
            if (player.GetComponent<PlayerInput>().user.id == 1) continue;
            player.GetComponent<PlayerInputController>().OnEnterEvent += SetPlayerWithPlayerInput;
            player.GetComponent<PlayerInputController>().OnExitEvent += RemovePlayerWithPlayerInput;
        }
    }

    private void SetPlayerWithPlayerInput(PlayerInput obj)
    {
        var characterSelect = new CharacterSelectState(obj.user.id, 3);
        SetPlayerCharacterSelected(obj.user.id,characterSelect);
        if(players.Contains(obj.gameObject)) return;
        players.Add(obj.gameObject);
        EventsManager.OnAddNewInputPlayer.Invoke(obj);
    }

    private void RemovePlayerWithPlayerInput(PlayerInput obj)
    {
        RemovePlayerCharacterSelected(obj.user.id);
        if(!players.Contains(obj.gameObject)) return;
        players.Remove(obj.gameObject);
        EventsManager.OnRemoveNewInputPlayer.Invoke(obj);
    }

    #region CharacterDataPlayers

    public CharacterSelectState GetPlayerCharacterSelectedForID(ulong id)
    {
        foreach (var character in playersCharacterSelectStates)
        {
            if (character.Value.ClientId == id)
            {
                return character.Value;
            }
        }
        return null;
    }

    public void SetPlayerCharacterSelected(ulong id,CharacterSelectState selectState)
    {
        playersCharacterSelectStates[id] = selectState;
    }
    
    private void RemovePlayerCharacterSelected(ulong id)
    {
        if (playersCharacterSelectStates.ContainsKey(id))
        {
            playersCharacterSelectStates.Remove(id);
        }
    }
    #endregion
    
    public void ResetPlayersParentAndRemoveChild()
    {
        foreach (var player in players)
        {
            player.transform.SetParent(playerHolder);
            Destroy(player.transform.GetChild(0).gameObject);
        }
    }
}
