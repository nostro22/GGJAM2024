using UnityEngine;

public class ObjectPooleable : MonoBehaviour
{
   private ObjectPool poolParent;

   public void SetPool(ObjectPool objectPool)
   {
      poolParent = objectPool;
   }
   private void OnDisable()
   {
      poolParent.Pull(gameObject);
   }
}
