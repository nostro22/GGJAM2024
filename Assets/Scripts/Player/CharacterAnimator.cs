using System;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour, IParentObject
{
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;
    [SerializeField] private AnimationEventHandler animationEventHandler;
    private Animator animator;
    private int currentRig;
    private int ON_DASH = Animator.StringToHash("Dash");
    private int ON_STUN = Animator.StringToHash("Stun");
    private int IS_WALKING = Animator.StringToHash("Movement");
    private void Awake()
    {
        animator = transform.GetChild(0).GetComponentInChildren<Animator>();
        ResetAnimationsDrags();
    }

    public void OnDashAnimation()
    {
        animator.SetTrigger(ON_DASH);
    }

    public void OnStunAnimation()
    {
        animator.SetTrigger(ON_STUN);
    }

    public void UpdateWalk(float value)
    {
        animator.SetFloat(IS_WALKING, value);
    }

    public void UpdateParentIK(AnimationTargetConstraintIK animationIK, int rig)
    {
        leftHand.SetLocalPositionAndRotation(animationIK.HandsPoserSO.leftHand.position
            , animationIK.HandsPoserSO.leftHand.rotation);
        rightHand.SetLocalPositionAndRotation(animationIK.HandsPoserSO.rightHand.position
            , animationIK.HandsPoserSO.rightHand.rotation);
    }

    public void ClearHandsIK()
    {
        ResetAnimationsDrags();
        animationEventHandler.RemoveAllEvents(animator);
    }

    public void UpdateToolLayerUse(int layer, int use)
    {
        animator.SetLayerWeight(layer, use);
    }

    private void ResetAnimationsDrags()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(2, 0);
        animator.SetLayerWeight(3, 0);
        animator.SetLayerWeight(4, 0);
        animator.SetLayerWeight(5, 0);
    }

    public void AddAnimationActionEvent(Action methodToCall, float time,string animationName)
    {
       animationEventHandler.AddAnimationActionEvent(methodToCall,time,animationName,animator);
    }
    
    public void AddAnimationEndedEvent(Action methodToCall, float time,string animationName)
    {
        animationEventHandler.AddAnimationEndedEvent(methodToCall,time,animationName,animator);
    }

public Transform Parent { get; set; }
}