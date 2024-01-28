using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsTrigger : MonoBehaviour
{
    
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            AudioManager.Instance.PlayEffectPlayerHitWithCar();
            other.GetComponent<PlayerController>().OnDeadEvent.Invoke();
        }
    }
}
