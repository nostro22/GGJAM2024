using UnityEngine;

namespace UI.Gameplay
{
    public class VisualObject : MonoBehaviour
    {
        [SerializeField] private GameObject visualObject;
        [SerializeField] private bool hideOnStart = true;
        private void Start()
        {
           if(hideOnStart)Hide();
        }

        public void Show()
        {
            visualObject.SetActive(true);
        }

        public void Hide()
        {
            visualObject.SetActive(false);
        }
        
    }
}