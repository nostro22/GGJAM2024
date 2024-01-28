using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaUI : MonoBehaviour
{
    [SerializeField] private RectTransform back;
    private void OnEnable()
    {
        LeanTween.alpha(back, 0f, 0);
        LeanTween.alpha(back, 0.7f, 0.3f);
    }
}
