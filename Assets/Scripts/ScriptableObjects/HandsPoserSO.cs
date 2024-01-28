using System;
using UnityEngine;

namespace ScriptableObjects
{
    public class HandsPoserSO : ScriptableObject
    {
        [Header("Parent Drag")] 
        public int animationRigLayer;
        public int animationLayerUse;

        [Header("Position and Rotation Hands")]
        public HandTrack leftHand;
        public HandTrack rightHand;

    }


    [Serializable]
    public class HandTrack
    {
        [Range(0,1)]
        public float weight;
        public Vector3 position;
        public Quaternion rotation;
    }
}