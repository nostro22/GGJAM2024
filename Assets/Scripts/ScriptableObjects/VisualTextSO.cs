using UnityEngine;

namespace ScriptableObjects
{
    public enum VisualTextType
    {
        Stun,
        Error,
        Warning,
        Full,
        Drag,
        Drop,
        Cleaned,
        Empty,
        Back,
        Accept,
        Join,
        Resume,
        SelectCharacter,
        GoToWork
    }
    
    [CreateAssetMenu(fileName = "NewVisualTextSO",menuName = "ScriptableObjects/VisualText",order = 0)]
    public class VisualTextSO : ScriptableObject
    {
        [SerializeField] private VisualText[] visualTexts;
        public string GetVisualTextByTextType(VisualTextType textType)
        {
            foreach (var visualText in visualTexts)
            {
                if (visualText.textType == textType)
                    return visualText.value;
            }

            return  string.Empty;;
        }
    }

    [System.Serializable]
    public class VisualText
    {
        public VisualTextType textType;
        public string value;
    }
}