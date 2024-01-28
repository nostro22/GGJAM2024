using ScriptableObjects;
using UnityEngine;

public class SpawnWorldUI : MonoBehaviour
{
    [SerializeField] private SpawnWorldUISO spawnWorldUiso;

    private void Awake()
    {
       var ui = Instantiate(spawnWorldUiso.UIPrefab);
       ui.transform.position = transform.position + spawnWorldUiso.offset;
       ui.transform.SetParent(transform,true);
    }
}