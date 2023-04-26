using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ProgressBar progressBar;
    public Transform AttackPoint;
    public float attackRange = 3f;
    public LayerMask enemyLayers;
    public int maxHealth = 100;
    int currentHealth;
    public int attackDamage = 20;
    public Animator animator;
    bool isDead = false;
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
    void Update()
    {
        if (isDead)
            return;
        Attack();

    }
    void Attack()
    {
        AttackPoint = transform.Find("AttackPoint");
        // Wykrywanie przeciwnik�w 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);
        // Obra�enia
        foreach (Collider2D player in hitEnemies)
        {
            Debug.Log("We hit " + player.name);
            player.GetComponent<Enemy>()
                 .TakeDamage(attackDamage);

        }
    }
}
