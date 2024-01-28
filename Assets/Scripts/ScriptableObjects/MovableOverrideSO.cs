using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewMovableOverrideSO",menuName = "ScriptableObjects/MovableOverride",order = 0)]
    public class MovableOverrideSO : ScriptableObject
    {
        public float overrideSpeed;
    }
}