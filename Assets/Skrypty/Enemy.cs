using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HeroKnight player;
    public ProgressBar progressBar;
    public Transform AttackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public LayerMask attackLayerMask;
    public int maxHealth = 100;
    int currentHealth;
    public int attackDamageEnemy = 20;
    public Animator animator;
    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Physics2D.IgnoreLayerCollision(3, 7, true);
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;
        currentHealth -= damage;
        //HURT HERE
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("Dead", true);
        //Die animation
        Debug.Log("dead");
        isDead = true;
        progressBar.DeadEnemies++;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead)
            return;
        if (Input.GetKeyDown("l"))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Isdoingdmg");
        Transform transform1 = transform.Find("AttackPoint");
        AttackPoint = transform1;
        // Wykrywanie przeciwnik�w 
        Collider2D[] hitHero = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, attackLayerMask);
        // Obra�enia
        foreach (Collider2D player in hitHero)
        {
            player.GetComponent<HeroKnight>().TakeDamageHero(attackDamageEnemy);
        }
    }
}
