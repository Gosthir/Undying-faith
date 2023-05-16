using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour
{
    public GameObject GameOverScreen;
    private int initialAttackDamageEnemy;
    private Abilities abilities;
    public float m_speed = 4.0f;
    [SerializeField] float m_rollForce = 6.0f;
    [SerializeField] GameObject m_slideDust;
    [SerializeField] private HealthBar _healthbar;
    public Enemy enemy;
    public Boss Bosss; 
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    private bool m_isWallSliding = false;
    private bool m_grounded = false;
    private bool m_rolling = false;
    public int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;
    public bool m_IsAlive = true;

    // Walka
    public Transform AttackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public int AttackDamageHero { get; set; } = 20;
    public int maxHealth = 100;
    public float currentHealth;
    public bool m_blockButtonHeld = false;
    private bool m_isBlocking = false;
    //DMG i Zdrowie
    public int TotalAttackDamage
    {
        get { return AttackDamageHero + abilities.BonusAttackDamage; }
    }
    private int previousBonusHealth;


    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }




    // Use this for initialization
    void Start()
    {

        initialAttackDamageEnemy = enemy.attackDamageEnemy;  
        abilities = GetComponent<Abilities>();
        maxHealth = 100 + abilities.BonusHealth;
        abilities = GetComponent<Abilities>();
        enemy = GameObject.FindObjectOfType<Enemy>(); // Find the Enemy game object in the scene 
        Bosss = GameObject.FindObjectOfType<Boss>(); // Find the Enemy game object in the scene 
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        //moje
        currentHealth = maxHealth;
        _healthbar.UpdateHealthBar(maxHealth, currentHealth);

        // Find all game objects with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Set the initial attack damage for each enemy
        foreach (GameObject enemyObj in enemies)
        {
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            if (enemy != null)
            {
                initialAttackDamageEnemy = enemy.attackDamageEnemy;
            }
        }
    }

    void FixedUpdate()
    {
        if (abilities.BonusHealth != previousBonusHealth)
        {
            currentHealth += abilities.BonusHealth - previousBonusHealth;
            maxHealth += abilities.BonusHealth - previousBonusHealth;
            previousBonusHealth = abilities.BonusHealth;


            
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Block
        if (Input.GetMouseButtonDown(1) && !m_rolling && m_IsAlive)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
            m_isBlocking = true;

            // Update the attack damage for all enemies with the "Enemy" tag
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemyObj in enemies)
            {
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.attackDamageEnemy = Mathf.Max(initialAttackDamageEnemy - 15, 0);
                }
            }
        }
        else if (Input.GetMouseButtonUp(1) && m_IsAlive)
        {
            m_animator.SetBool("IdleBlock", false);
            m_isBlocking = false;

            // Reset the attack damage for all enemies with the "Enemy" tag
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemyObj in enemies)
            {
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.attackDamageEnemy = initialAttackDamageEnemy;
                }
            }
        }

        // Take damage
        /*
        if (enemy.attackDamageEnemy > 0)
        {
            if (m_isBlocking)
            {
                enemy.attackDamageEnemy -= 1;
            }
            else
            {
                // Health regen or idle state logic here
            }
        } */





        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0 && m_IsAlive)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
            var tempPos = AttackPoint.localPosition;
            tempPos.x = 0.85f;
            AttackPoint.localPosition = tempPos;

        }

        else if (inputX < 0 && m_IsAlive)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
            var tempPos = AttackPoint.localPosition;
            tempPos.x = -0.85f;


            AttackPoint.localPosition = tempPos;
        }

        // Move
        if (!m_rolling && m_IsAlive && !m_isBlocking && !this.m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && !this.m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && !this.m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);


        

        //Attack
        if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling && m_IsAlive) 
        {
            // Get a random attack audio clip from AudioManager
            int randomClipNumber = Random.Range(1, 4);
            AudioClip attackClip = null;

            switch (randomClipNumber)
            {
                case 1:
                    attackClip = audioManager.Atak1;
                    break;
                case 2:
                    attackClip = audioManager.Atak2;
                    break;
                case 3:
                    attackClip = audioManager.Atak3;
                    break;
            }

            // Play the attack audio clip from AudioManager
            audioManager.PlaySFX(attackClip);
            Attack(); //deklaruję funkcję
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

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
                if(enemy.CompareTag("Enemy")) { 
                    enemy.GetComponent<Enemy>().TakeDamage(TotalAttackDamage);
                }
            }
            foreach (Collider2D bosss in hitEnemies)
            {
                if (bosss.CompareTag("Boss")) { 
                bosss.GetComponent<Boss>().TakeDamage(TotalAttackDamage);
                }
            }
    }
        
        

        if (Input.GetMouseButtonUp(1) && m_IsAlive)
            m_animator.SetBool("IdleBlock", false);

        // Roll
        if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding && m_IsAlive)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }

        //Run
       if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }
        //Healthbar
        _healthbar.UpdateHealthBar(maxHealth, currentHealth);

        
    }

     internal void TakeDamageHero(int damage)
    {
        if(m_IsAlive){ 
        currentHealth -= damage;
        //HURT HERE
        m_animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            m_IsAlive = false;
            m_grounded = false;
            m_animator.SetBool("Dead",true);
                GameOverScreen.SetActive(true);

            }
        }
    }
}
