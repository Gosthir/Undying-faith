using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    private HeroKnight heroKnight;
    private AudioManager audioManager;

    public bool BonusAttackDmgON = false;
    public int BonusAttackDamagePoints;
    public int BonusAttackDamagePointsMultiplayer;
    public int BonusAttackDamage;

    public bool BonusHealthOn = false;
    public int BonusHealth;
    public int BonusHealthPoints;
    public int BonusHealthPointsMultiplayer;

    public float laserTimer;
    public float laserCooldown;
    public bool laser = false;
    public int laserPoints = 0;


    public float odepchniecieTimer = 0f;
    public float odepchniecieCooldown; // Cooldown duration in seconds
    public bool odepchniecie = false;
    public int odepchnieciePoints = 0;
    public int odepchnięcieDMG;

    public float ogienTimer;
    public float ogienCooldown;
    public bool boskiOgien = false;
    public int boskiOgienPoints = 0;
    public int BurnDMG;
    public int BurnDuration;
    public float BurnRange;

    public float duchTimer;
    public float duchCooldown;
    public bool formaDucha = false;
    public int formaDuchaPoints = 0;
    private bool isFormaDuchaActive = false;

    public float dashTimer;
    public float dashCooldown;
    public bool Dash = false;
    public float dashSpeed;
    public float dashDistance = 3f; 
    public float dashDuration = 0.5f; 
    public float dashBlinkInterval = 0.1f;

    public float meteorytTimer;
    public float meteorytCooldown;
    public bool meteoryt = false;
    public int meteorytPoints = 0;
    public GameObject meteorPrefab;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    private void Start()
    {
        heroKnight = GetComponent<HeroKnight>();
        Debug.Log("Odepchniecie timer: " + odepchniecieTimer);
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
    //Odepchniecie
    private void PushEnemiesAway(Transform origin, float range, float force)
    {
        Debug.Log("PushEnemiesAway() method called!");
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
    //BOSKI OGIEN
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
    //dash
    IEnumerator Blink(Vector3 blinkPosition, float blinkDuration, float blinkInterval)
    {
        Renderer renderer = GetComponent<Renderer>();

        // Set the player to invisible
        renderer.enabled = false;

        // Wait for the blink interval
        yield return new WaitForSeconds(blinkInterval);

        // Move the player to the desired position
        transform.position = blinkPosition;

        // Set the player to visible
        renderer.enabled = true;

        // Wait for the remaining blink duration
        yield return new WaitForSeconds(blinkDuration - blinkInterval);

        // Set the player to visible again
        renderer.enabled = true;
    }

    private void Update()
    {
        Transform AttackPoint = heroKnight.AttackPoint;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (laser && laserTimer <= 0f)
            {
                /*GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
                laser.transform.right = transform.right;
                Rigidbody2D laserRB = laser.GetComponent<Rigidbody2D>();
                laserRB.velocity = laser.transform.right * laserSpeed; */
                laserTimer = laserCooldown;
            }
        }
        if (laserTimer > 0f)
        {
            laserTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (odepchniecie && odepchniecieTimer <= 0f)
            {
                PushEnemiesAway(transform, 3f, 9f);
                odepchniecieTimer = odepchniecieCooldown;
            }
        }
        if (odepchniecieTimer > 0f)
        {
            odepchniecieTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (boskiOgien && ogienTimer <= 0f)
            {
                // Get reference to attack point and enemy layers from heroKnight script
                AttackPoint = heroKnight.AttackPoint;
                LayerMask enemyLayers = heroKnight.enemyLayers;

                // Start coroutine to repeat attack for specified duration
                StartCoroutine(RepeatAttack(AttackPoint, enemyLayers, BurnDMG, BurnRange));
                ogienTimer = ogienCooldown;
            }
        }
        if (ogienTimer > 0f)
        {
            ogienTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if(formaDucha && duchTimer <= 0f)
            {
                StartCoroutine(DisableEnemyAttacksForSeconds(2f + formaDuchaPoints));
                duchTimer = duchCooldown;
            }

        }
        if (duchTimer > 0f)
        {
            duchTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (Dash && dashTimer <= 0f)
            {


                // Find the HeroKnight component attached to the player object
                HeroKnight heroKnight = GetComponent<HeroKnight>();


                audioManager.PlaySFX(audioManager.DashSound);


                // Determine the direction the player is facing
                int facingDirection = heroKnight.m_facingDirection;

                // Blink the player to the desired location
                Vector3 blinkPosition = transform.position + facingDirection * Vector3.right * dashDistance;
                StartCoroutine(Blink(blinkPosition, dashDuration, dashBlinkInterval));

                // Move the player in the direction they are facing
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 dashDirection = facingDirection * Vector2.right * dashSpeed;
                    rb.velocity = dashDirection;
                    dashTimer = dashCooldown;
                }
            }
        }
        if (dashTimer > 0f)
        {
            dashTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (meteoryt && meteorytTimer <= 0f)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = -Camera.main.transform.position.z; // Set the z position to the distance from the camera
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

                // Instantiate the meteor prefab at the mouse position
                GameObject meteor = Instantiate(meteorPrefab, worldPos, Quaternion.identity);

                // Add a downward velocity to make the meteor fall
                Rigidbody2D meteorRb = meteor.GetComponent<Rigidbody2D>();
                meteorRb.velocity = new Vector2(0, -10f);
                dashTimer = dashCooldown;
            }
        }
        if (meteorytTimer > 0f)
        {
            meteorytTimer -= Time.deltaTime;
        }
    }
}