using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    public Animator animator;

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            if(animator != null)
            {
            animator.SetTrigger("attack01");
            }
        }

  
    }

}
