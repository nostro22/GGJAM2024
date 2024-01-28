using UnityEngine;
using UnityEngine.UI;

namespace UI.Gameplay
{
    public class VisualIconMark : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private Sprite defaultIcon;
        [SerializeField] private Sprite changedIcon;


        public void SetDefaultIcon(Sprite sprite)
        {
            defaultIcon = sprite;
            ResetToDefaultIcon();
        }
        
        private void Awake()
        {
            ResetToDefaultIcon();
        }

        public void Show()
        {
            iconImage.gameObject.SetActive(true);
        }

        public void Hide()
        {
            iconImage.gameObject.SetActive(false);
        }

        public void ChangeIcon()
        {
            if (changedIcon)
                iconImage.sprite = changedIcon;
        }

        public void ResetToDefaultIcon()
        {
            if (defaultIcon)
                iconImage.sprite = defaultIcon;
        }

        public void SetColorIcon(Color color)
        {
            iconImage.color = color;
        }
    }
}
