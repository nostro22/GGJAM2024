using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI.Gameplay
{
    public class VisualText : MonoBehaviour
    {
        [SerializeField] private GameObject visualObject;
        [SerializeField] private TextMeshProUGUI visualText;
        [SerializeField] private bool hideOnStart = true;
        [SerializeField] private VisualTextSO visualTextSO;
        
        private void Start()
        {
            if(hideOnStart)Hide();
        }

        public void Set(VisualTextSO visualTextSo)
        {
            visualTextSO = visualTextSo;
        }
        public void Show(VisualTextType visualTextType)
        {
            visualText.text = visualTextSO.GetVisualTextByTextType(visualTextType);
            visualObject.SetActive(true);
        }

        public void Hide()
        {
            visualText.text = string.Empty;
            visualObject.SetActive(false);
        }
    }
}