using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewSpawnWorldUISO", menuName = "ScriptableObjects/SpawnWorldUISO", order = 0)]
    public class SpawnWorldUISO : ScriptableObject
    {
        public GameObject UIPrefab;
        public Vector3 offset;
    }
}