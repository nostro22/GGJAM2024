using UnityEngine;
using UnityEngine.Events;

public class InteractableController : MonoBehaviour
{
    public UnityEvent<GameObject> OnEnterInteract;
    public UnityEvent OnExitInteract;
}
