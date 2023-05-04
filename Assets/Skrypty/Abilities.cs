using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    private HeroKnight heroKnight;


    public bool laser = false;
    public int laserPoints = 0;


    public bool odepchniecie = false;
    public int odepchnieciePoints = 0;


    public bool boskiOgien = false;
    public int boskiOgienPoints = 0;
    public int BurnDMG = 10;
    public int BurnDuration = 3;
    public float BurnRange;


    public bool formaDucha = false;
    public int formaDuchaPoints = 0;
    private bool isFormaDuchaActive = false;


    public bool bariera = false;
    public int barieraPoints = 0;


    public bool AtakZGory = false;
    public int atakZGoryPoints = 0;


    public bool oslepienie = false;
    public int oslepieniePoints = 0;


    public bool meteoryt = false;
    public int meteorytPoints = 0;


    private void Start()
    {
        heroKnight = GetComponent<HeroKnight>();
    }

    // FORMA DUCHA
    private IEnumerator DisableEnemyAttacksForSeconds(float seconds)
    {
        // Find all sprites with an "Enemy" script
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        // Disable attacks for all "Enemy" sprites
        foreach (Enemy enemy in enemies)
        {
            enemy.isAttackEnabled = false;
        }

        // Set transparency of hero to 25%
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color spriteColor = spriteRenderer.color;
        spriteColor.a = 0.25f;
        spriteRenderer.color = spriteColor;

        // Wait for specified seconds
        yield return new WaitForSeconds(seconds);

        // Reset transparency of hero to 100%
        spriteColor.a = 1f;
        spriteRenderer.color = spriteColor;

        // Enable attacks for all "Enemy" sprites
        foreach (Enemy enemy in enemies)
        {
            enemy.isAttackEnabled = true;
        }
    }
    //BOSKI OGIEÑ
    private IEnumerator RepeatAttack(Transform attackPoint, LayerMask enemyLayers, int damage, float BurnRange)
    {
        float elapsedTime = 0;
        while (elapsedTime < BurnDuration)
        {
            // Detect enemies within range
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, BurnRange, enemyLayers);

            // Deal damage to enemies
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    Debug.Log("We hit " + enemy.name);
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                }
                else if (enemy.CompareTag("Boss"))
                {
                    enemy.GetComponent<Boss>().TakeDamage(damage);
                }
            }

            // Wait for 0.5 seconds before repeating attack
            yield return new WaitForSeconds(0.5f);
            elapsedTime += 0.5f;
        }
    }

    private void Update()
    {
        Transform AttackPoint = heroKnight.AttackPoint;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            laser = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            odepchniecie = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (boskiOgien)
            {
                // Get reference to attack point and enemy layers from heroKnight script
                AttackPoint = heroKnight.AttackPoint;
                LayerMask enemyLayers = heroKnight.enemyLayers;

                // Set BurnDuration to 3
                BurnDuration = 3;

                // Start coroutine to repeat attack for specified duration
                StartCoroutine(RepeatAttack(AttackPoint, enemyLayers, BurnDMG, BurnRange));
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if(formaDucha)
            {
                StartCoroutine(DisableEnemyAttacksForSeconds(2f + formaDuchaPoints));
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            bariera = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            AtakZGory = true;
        }
    }
}