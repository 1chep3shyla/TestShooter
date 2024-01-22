using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health = 100f;
    private PlayerAnimatorController animatorController;

    void Start()
    {
        animatorController = GetComponent<PlayerAnimatorController>();
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        animatorController.TriggerHitAnimation();
        if (health <= 0)
        {
            Die();
        }
    }

    void Die() 
    {
        Destroy(gameObject,5f);
        animatorController.TriggerDeathAnimation();
    }
}