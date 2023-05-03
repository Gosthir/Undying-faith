using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (isAttackEnabled)
        {
            animator.SetTrigger("Isdoingdmg");
            Transform transform1 = transform.Find("AttackPoint");
            AttackPoint = transform1;
            // Wykrywanie przeciwników 
            Collider2D[] hitHero = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, attackLayerMask);
            // Obrażenia
            foreach (Collider2D player in hitHero)
            {
                player.GetComponent<HeroKnight>().TakeDamageHero(attackDamageEnemy);
            }
        }
    }
}
