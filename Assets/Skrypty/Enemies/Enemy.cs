using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
    public Vector2 PositionofEnemy;
    public int bigger;
    public int lesser;
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
        PositionofEnemy = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
        
        if (PositionofEnemy[0] < bigger && PositionofEnemy[0] > lesser)
        {
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("SkeletIdle"))
            {
                if(isAttackEnabled) { 
                StartCoroutine(attack());
                }
            }
        }
    }

    IEnumerator attack()
    {
        if (isAttackEnabled)
        {
            animator.SetTrigger("Isdoingdmg");
            yield return new WaitForSeconds(1);
            Transform transform1 = transform.Find("AttackPoint");
            AttackPoint = transform1;
            // Wykrywanie przeciwników 
            Collider2D[] hitHero = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, attackLayerMask);
            // Obrażenia
            foreach (Collider2D player in hitHero)
            {
                player.GetComponent<HeroKnight>().TakeDamageHero(attackDamageEnemy);
            }
            yield return new WaitForSeconds(1);
        }

    }
}
