using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    private HeroKnight heroKnight;

    public bool BonusAttackDmgON = false;
    public int BonusAttackDamagePoints;
    public int BonusAttackDamagePointsMultiplayer;
    public int BonusAttackDamage;

    public bool BonusHealthOn = false;
    public int BonusHealth;
    public int BonusHealthPoints;
    public int BonusHealthPointsMultiplayer;

    public bool laser = false;
    /*public int laserPoints = 0;
    public GameObject laserPrefab;
    public float laserSpeed;
    public int laserDamage; */

    public bool odepchniecie = false;
    public int odepchnieciePoints = 0;
    public int odepchnięcieDMG;

    public bool boskiOgien = false;
    public int boskiOgienPoints = 0;
    public int BurnDMG;
    public int BurnDuration;
    public float BurnRange;

    public bool formaDucha = false;
    public int formaDuchaPoints = 0;
    private bool isFormaDuchaActive = false;

    public bool AtakZGory = false;
    public int atakZGoryPoints = 0;

    public bool meteoryt = false;
    public int meteorytPoints = 0;
    public GameObject meteorPrefab;

    private void Start()
    {
        heroKnight = GetComponent<HeroKnight>();

    }
    

    void FixedUpdate()
    {
        if(BonusAttackDmgON)
        {
            BonusAttackDamage = BonusAttackDamagePoints * BonusAttackDamagePointsMultiplayer;
        }
        
        if(BonusHealthOn)
        {
            BonusHealth = BonusHealthPoints * BonusHealthPointsMultiplayer;
        }
        
        //BurnDMG = BurnDMG + boskiOgienPoints * 3;
        //BurnDuration = BurnDuration + boskiOgienPoints;
        odepchnięcieDMG = odepchnieciePoints * 10;
    }

    //Odepchni�cie 
    private void PushEnemiesAway(Transform origin, float range, float force)
    {
        // Detect enemies within range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(origin.position, range, heroKnight.enemyLayers);

        // Push enemies away from the player
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Vector2 direction = enemy.transform.position - origin.position;
                enemy.GetComponent<Rigidbody2D>().AddForce(direction.normalized * force, ForceMode2D.Impulse);
                enemy.GetComponent<Enemy>().TakeDamage((int)odepchnięcieDMG);
            }
            else if (enemy.CompareTag("Boss"))
            {
                Vector2 direction = enemy.transform.position - origin.position;
                enemy.GetComponent<Rigidbody2D>().AddForce(direction.normalized * force, ForceMode2D.Impulse);
                enemy.GetComponent<Boss>().TakeDamage((int)odepchnięcieDMG);
            }
        }
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
    //BOSKI OGIE�
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
            if (laser)
            {
                /*GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
                laser.transform.right = transform.right;
                Rigidbody2D laserRB = laser.GetComponent<Rigidbody2D>();
                laserRB.velocity = laser.transform.right * laserSpeed; */
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
           if (odepchniecie)
            {
                PushEnemiesAway(transform, 3f, 9f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (boskiOgien)
            {
                // Get reference to attack point and enemy layers from heroKnight script
                AttackPoint = heroKnight.AttackPoint;
                LayerMask enemyLayers = heroKnight.enemyLayers;

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
            if (AtakZGory)
            {

            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (meteoryt)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = -Camera.main.transform.position.z; // Set the z position to the distance from the camera
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

                // Instantiate the meteor prefab at the mouse position
                GameObject meteor = Instantiate(meteorPrefab, worldPos, Quaternion.identity);

                // Add a downward velocity to make the meteor fall
                Rigidbody2D meteorRb = meteor.GetComponent<Rigidbody2D>();
                meteorRb.velocity = new Vector2(0, -10f);
            }
        }
    }
}