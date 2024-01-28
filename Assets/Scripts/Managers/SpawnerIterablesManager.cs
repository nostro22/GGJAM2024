using ScriptableObjects;
using UnityEngine;

public class SpawnerIterablesManager : MonoBehaviour
{
   [SerializeField] private Transform[] spawnPoints;
   // [SerializeField] private ToolContainerSO toolContainerSO;
   [SerializeField] private Vector3 direccionToSpawnTools;
   public void Awake()
   {
      int indexTool = 0;
      foreach (var spawnPoint in spawnPoints)
      {
         // if(indexTool < toolContainerSO.ToolList.Count)
         // {
         //    for (int i = 0; i < toolContainerSO.toolsAmount; i++)
         //    {
         //       var toolSo = toolContainerSO.ToolList[indexTool];
         //       var ngo = Instantiate(toolSo.prefab);
         //       ngo.transform.position = spawnPoint.position + (direccionToSpawnTools*i);  
         //    }
         // }
         indexTool++;
      }
   }
}
