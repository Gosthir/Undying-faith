using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : MonoBehaviour
{
    public bool isAttackEnabled = true;
    public HeroKnight player;
    public ProgressBar progressBar;
    public Transform AttackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public LayerMask attackLayerMask;
    public int maxHealth = 100;
    int currentHealth;
    public int attackDamageEnemy = 20;
    public Animator b_animator;
    public bool isDead = false;
    
    void Start()
    {
        currentHealth = maxHealth;
        Physics2D.IgnoreLayerCollision(3, 7, true);
    }


    void FixedUpdate()
    {
        if (isDead)
            return;
    }
    void Die()
    {
        b_animator.SetBool("Dead", true);
        isDead = true;
    }
    public void TakeDamage(int damage)
    {
        if (isDead)
            return;
        currentHealth -= damage;
        b_animator.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
