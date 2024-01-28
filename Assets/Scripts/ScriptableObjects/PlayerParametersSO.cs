using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewPlayerParemetersSO",menuName = "ScriptableObjects/PlayerParameters",order = 0)]
    public class PlayerParametersSO : ScriptableObject
    {
        public float playerSpeed = 3.0f;
        public float durationDash = 0.2f;
        public float distanceDash = 2f;
        public float jumpHeight = 1.0f;
        public float gravityValue = -9.81f;
        public LayerMask defaultInteractLayer;
        public LayerMask defaultCleaningLayer;
        public LayerMask defaultTimerEventObjectsLayer;
        public float timeDelayStun = 0.5f;
    }
}