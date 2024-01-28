using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemHandler : MonoBehaviour
{
    public GameObject selectedObject;
    public float delayToSelect;
    private void OnEnable()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForSeconds(delayToSelect);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedObject);
    }

    public void OverrideObjectSelected()
    {
        StartCoroutine(Initialize());
    }
    
}
