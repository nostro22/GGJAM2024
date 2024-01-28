using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    [SerializeField] private CharacterDatabase characterDatabase;
    [SerializeField] private GameObject characterContainer;
    [SerializeField] private GameObject joinContainer;
    [SerializeField] private Transform SpawnpointCharacter;
    [SerializeField] private GameObject characterVisualIntro;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private TMP_Text playerNameText;
    public PlayerInput playerInput;
    private int characterIndex;
    private bool OnChange;

    private void Start()
    {
        rightButton.onClick.AddListener(OnRightCharacter);
        leftButton.onClick.AddListener(OnLeftCharacter);
    }

    public void UpdateDisplay(CharacterSelectState state)
    {
        if (characterVisualIntro) Destroy(characterVisualIntro);
        if (state.CharacterId != -1)
        {
            var character = characterDatabase.GetCharacterById(state.CharacterId);
            characterIndex = character.Id;
            characterVisualIntro = Instantiate(character.IntroPrefab,SpawnpointCharacter,false);
        }
        playerNameText.text = $"Player {state.ClientId}";
    }

    public void DisableDisplay()
    {
        if (characterVisualIntro) Destroy(characterVisualIntro);
        joinContainer.SetActive(true);
        characterContainer.SetActive(false);
        playerInput = null;
    }

    public void RemoveInputCard()
    {
        if (playerInput)
        {
            playerInput.GetComponent<PlayerInputController>().OnSelectEvent -= PlayerInputControllerOnOnMoveEvent;   
        }
    }

    public void EnableDisplay(PlayerInput playerInput)
    {
        AddInputCard(playerInput);
        joinContainer.SetActive(false);
        characterContainer.SetActive(true);
        var color = UtilsPlayer.GetColoByIndex((int)playerInput.user.id);
        leftButton.image.color = color;
        rightButton.image.color = color;
    }

    public void AddInputCard(PlayerInput playerInput)
    {
        playerInput.SwitchCurrentActionMap("UISelector");
        this.playerInput = playerInput;
        playerInput.GetComponent<PlayerInputController>().OnSelectEvent += PlayerInputControllerOnOnMoveEvent;
    }

    private void PlayerInputControllerOnOnMoveEvent(Vector2 obj)
    {
        if (!OnChange)
        {
            switch (obj.x)
            {
                case 1f:
                    OnLeftCharacter();
                    OnChange = true;
                    break;
                case -1:
                    OnRightCharacter();
                    OnChange = true;
                    break;
            }
        }
        else
        {
            if (obj.x == 0f) OnChange = false;
        }
    }

    private void OnLeftCharacter()
    {
        characterIndex--;
        if (characterIndex < 1) characterIndex = characterDatabase.GetAllCharacters().Length;
        var character = new CharacterSelectState(playerInput.user.id,characterIndex);
        PlayerManager.Instance.SetPlayerCharacterSelected(playerInput.user.id,character);
        UpdateDisplay(character);
    }

    private void OnRightCharacter()
    {
        characterIndex++;
        if (characterIndex > characterDatabase.GetAllCharacters().Length) characterIndex = 1;
        var character = new CharacterSelectState(playerInput.user.id,characterIndex);
        PlayerManager.Instance.SetPlayerCharacterSelected(playerInput.user.id,character);
        UpdateDisplay(character);
    }
}
