using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetWalkingAnimation(bool isWalking)
    {
        animator.SetBool("IsWalking", isWalking);
    }

    public void TriggerAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public void TriggerHitAnimation()
    {
        animator.SetTrigger("Hit");
    }
        public void TriggerDeathAnimation()
    {
        animator.SetTrigger("Death");
    }
}