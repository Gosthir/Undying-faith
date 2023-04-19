using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform AttackPoint;
    public float attackRange = 3f;
    public LayerMask enemyLayers;
    public int maxHealth = 100;
    int currentHealth;
    public int attackDamage = 20;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Physics2D.IgnoreLayerCollision(3, 7, true);
    }

    public void TakeDamage(int damage)
    {
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

        //Disable the enemy
        
        //GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();

        void Attack()
        {
            AttackPoint = transform.Find("AttackPoint");
            // Wykrywanie przeciwników 
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position,
                                                                 attackRange,
                                                                 enemyLayers);
            // Obrażenia
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit " + enemy.name);
                enemy.GetComponent<Enemy>()
                     .TakeDamage(attackDamage);

            }
        }
    }
}

