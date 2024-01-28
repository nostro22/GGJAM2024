using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.CharacterSelect
{
    public class CopyWriteJoinCode : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textJoinCode;
        private Button button;

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                GUIUtility.systemCopyBuffer = textJoinCode.text;
                Debug.Log("Join Code copied to clipboard: " + textJoinCode.text);
            });
        }
    }
}