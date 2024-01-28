using System;
using UnityEngine;
using UnityEngine.Events;

public enum collisionDetectionType
{
    ByTag,
    ByLayer
}
[System.Serializable]
public class ColliderEvent : UnityEvent<Collider> { }
public class ColliderTriggerEvents : MonoBehaviour
{
    public ColliderEvent EnterObjectEvent;
    public ColliderEvent StayObjectEvent;
    public ColliderEvent ExitObjectEvent;
    [SerializeField] private collisionDetectionType collisionDetectionType;
    [SerializeField] private string tagToCollide;
    private bool isEnterObject = false;

    private void OnTriggerEnter(Collider other)
    {
        switch (collisionDetectionType)
        {
            case collisionDetectionType.ByTag:
                if (other.CompareTag(tagToCollide))
                {
                    isEnterObject = true;
                    EnterObjectEvent.Invoke(other);   
                }
                break;
            case collisionDetectionType.ByLayer:
                isEnterObject = true;
                EnterObjectEvent.Invoke(other);   
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (collisionDetectionType)
        {
            case collisionDetectionType.ByTag:
                if (other.CompareTag(tagToCollide))
                {
                    isEnterObject = false;
                    ExitObjectEvent.Invoke(other);       
                }
                break;
            case collisionDetectionType.ByLayer:
                isEnterObject = false;
                ExitObjectEvent.Invoke(other);      
                break;
            default:
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch (collisionDetectionType)
        {
            case collisionDetectionType.ByTag:
                if (other.CompareTag(tagToCollide))
                {
                    isEnterObject = true;
                    StayObjectEvent.Invoke(other);   
                }
                break;
            case collisionDetectionType.ByLayer:
                isEnterObject = true;
                StayObjectEvent.Invoke(other);   
                break;
            default:
                break;
        }
    }

    public bool ObjetoEstaDentro()
    {
        return isEnterObject;
    }
}
