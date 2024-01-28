using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            other.GetComponent<PlayerController>().OnDeadEvent.Invoke();
        }
    }
}
