using System;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour, IParentObject
{
    private Animator animator;
    private int currentRig;
    private int ON_DASH = Animator.StringToHash("attack");
    private int ON_STUN = Animator.StringToHash("stun");
    private int IS_WALKING = Animator.StringToHash("move");
    private int ON_JUMP = Animator.StringToHash("jump");
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnDashAnimation()
    {
        animator.SetTrigger(ON_DASH);
    }

    public void OnStunAnimation()
    {
        animator.SetTrigger(ON_STUN);
    }
    
    public void OnJumpAnimation()
    {
        animator.SetTrigger(ON_JUMP);
    }

    public void UpdateWalk(bool value)
    {
        animator.SetBool(IS_WALKING, value);
    }
    
    

public Transform Parent { get; set; }
}