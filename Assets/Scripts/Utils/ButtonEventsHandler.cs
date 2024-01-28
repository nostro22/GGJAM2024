using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonEventsHandler : MonoBehaviour,ICancelHandler
{
    [SerializeField] private UnityEvent OnCancelEvent;
    
    public void OnCancel(BaseEventData eventData)
    {
        OnCancelEvent.Invoke();
    }
}
