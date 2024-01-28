using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
namespace UI.Gameplay
{
    public class VisualBar : MonoBehaviour
    {
        [SerializeField] private Slider sliderBar;
        [SerializeField] private GameObject sliderBarObject;
        [SerializeField] private bool OnHideStart = false;
        public void Initialize(int maxValue,int minValue)
        {
            sliderBar.minValue = minValue;
            sliderBar.maxValue = maxValue;
            sliderBar.value = 0;
            if (OnHideStart)
            {
                Hide();
            }
            else
            {
                Show();   
            }
        }

        public void UpdateBar(float amountValue)
        {
            sliderBar.value = amountValue;
        }

        public void Show()
        {
            sliderBarObject.SetActive(true);
        }

        public void Hide()
        {
            sliderBarObject.SetActive(false);
        }
        
    }
}