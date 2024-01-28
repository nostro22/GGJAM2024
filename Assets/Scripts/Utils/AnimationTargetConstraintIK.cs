using System;
using ScriptableObjects;
using UnityEngine;
public class AnimationTargetConstraintIK : MonoBehaviour
{
   public bool isUssable;
   public HandsPoserSO HandsPoserSO;
   public bool isUseAnimationEvent;
   public bool isOnlyUseAnimation;
   public float time;
   public string animationName;
   public Action animationEventAction;
}

