using UnityEngine;

public class CharacterAnimator : MonoBehaviour, IParentObject
{
    private Animator animator;
    private int currentRig;
    private int ON_DASH = Animator.StringToHash("attack");
    private int ON_STUN = Animator.StringToHash("stun");
    private int IS_WALKING = Animator.StringToHash("move");
    private int ON_JUMP = Animator.StringToHash("jump");
    private int ON_ATTACK = Animator.StringToHash("attack");
    private int ON_DEAD = Animator.StringToHash("dead");
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
    
    public void OnDeadAnimation()
    {
        animator.SetBool(ON_DEAD,true);
    }
    
    public void OnAttackAnimation()
    {
        animator.SetTrigger(ON_ATTACK);
    }

    public void UpdateWalk(bool value)
    {
        animator.SetBool(IS_WALKING, value);
    }
    
    

public Transform Parent { get; set; }
}