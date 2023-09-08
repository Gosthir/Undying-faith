using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public Enemy enemy;
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
    public float odepchniecieCooldown;
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
    private Camera mainCamera;
    public GameObject explosionPrefab; // Prefab of the explosion object
    public float spawnInterval = 2f; // Interval between spawning objects
    public float spawnRange = 5f; // Horizontal range of spawning objects
    private float timer;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    private void Start()
    {
        heroKnight = GetComponent<HeroKnight>();
        Debug.Log("Odepchniecie timer: " + odepchniecieTimer);
        mainCamera = Camera.main;
        timer = spawnInterval;
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
    public IEnumerator DisableEnemyAttacksForSeconds(float seconds)
    {
        // Find all sprites with an "Enemy" script
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            enemy.canAttack = false;
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

        foreach (Enemy enemy in enemies)
        {
            enemy.canAttack = true;
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
        //meteor timer
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = spawnInterval;
        }

        Transform AttackPoint = heroKnight.AttackPoint;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (laser && laserTimer <= 0f)
            {
                audioManager.PlaySFX(audioManager.Laser);
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
            if (heroKnight.m_IsAlive && odepchniecie && odepchniecieTimer <= 0f)
            {
                audioManager.PlaySFX(audioManager.Odepchniecie);
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
            if (heroKnight.m_IsAlive && boskiOgien && ogienTimer <= 0f)
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
                audioManager.PlaySFX(audioManager.Duch);
                StartCoroutine(this.DisableEnemyAttacksForSeconds(2f + formaDuchaPoints));
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
                SpawnObject();
                audioManager.PlaySFX(audioManager.Meteor_Explosion);
                meteorytTimer = meteorytCooldown;
            }
        }
        if (meteorytTimer > 0f)
        {
            
            meteorytTimer -= Time.deltaTime;
        }





        void SpawnObject()
        {
            // Calculate random x position within the spawn range
            float spawnX = Random.Range(-spawnRange, spawnRange);

            // Calculate the position at the top of the camera's range
            float spawnY = mainCamera.transform.position.y + mainCamera.orthographicSize;

            // Spawn the meteor at the calculated position
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
            GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

            // Attach collision handler to the meteor
            MeteorCollisionHandler collisionHandler = meteor.GetComponent<MeteorCollisionHandler>();
            if (collisionHandler == null)
            {
                collisionHandler = meteor.AddComponent<MeteorCollisionHandler>();
            }
            collisionHandler.explosionPrefab = explosionPrefab;

            // Destroy the meteor after a delay
            Destroy(meteor, 5f);
        }
    }
}