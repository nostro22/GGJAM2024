using System.Collections;
using UnityEngine;
public class ResetCollider : MonoBehaviour
{
       private Collider colliderToIgnore;
       private Collider myCollider;

       private void Start()
       {
              myCollider = GetComponent<Collider>();
       }

       public void SetCollideToIgnore(GameObject newCollider)
       {
              StopAllCoroutines();
              if(colliderToIgnore!= null) 
                     Physics.IgnoreCollision(colliderToIgnore,myCollider,false);
              colliderToIgnore = newCollider.GetComponent<Collider>();
       }

       public void ResetColliderToPlayer()
       {
              StartCoroutine(ResetCollideToPlayer());
       }
       
       private IEnumerator ResetCollideToPlayer()
       {
              Physics.IgnoreCollision(colliderToIgnore,myCollider,true);
              yield return new WaitForSeconds(0.4f);
              Physics.IgnoreCollision(colliderToIgnore,myCollider,false);
       }
}
