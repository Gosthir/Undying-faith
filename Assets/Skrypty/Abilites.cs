using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public bool laser = false;
    public int laserPoints = 0;

    public bool odepchniecie = false;
    public int odepchnieciePoints = 0;

    public bool boskiOgien = false;
    public int boskiOgienPoints = 0;

    public bool formaDucha = false;
    public int formaDuchaPoints = 0;
    //k
    private bool isFormaDuchaActive = false;
    //k

    public bool bariera = false;
    public int barieraPoints = 0;

    public bool atakZGory = false;
    public int atakZGoryPoints = 0;

    public bool oslepienie = false;
    public int oslepieniePoints = 0;

    public bool meteoryt = false;
    public int meteorytPoints = 0;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // FORMA DUCHA
    private IEnumerator DisableCollidersForSeconds(float seconds)
    {
        // Disable collisions between the "Player" layer and the "Enemies" layer
        if (!isFormaDuchaActive)
        {
            int playerLayer = LayerMask.NameToLayer("Player");
            int enemyLayer = LayerMask.NameToLayer("Enemies");
            int layerMask = ~(1 << playerLayer | 1 << enemyLayer);
            Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.layer == playerLayer || collider.gameObject.layer == enemyLayer)
                {
                    collider.enabled = false;
                }
            }
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

        // Enable collisions between the "Player" layer and the "Enemies" layer
        if (!isFormaDuchaActive)
        {
            int playerLayer = LayerMask.NameToLayer("Player");
            int enemyLayer = LayerMask.NameToLayer("Enemies");
            int layerMask = ~(1 << playerLayer | 1 << enemyLayer);
            Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.layer == playerLayer || collider.gameObject.layer == enemyLayer)
                {
                    collider.enabled = true;
                }
            }
        }
    }

    private void Update()
    {
        if (Forma_Ducha == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                // Disable collisions and set transparency for 5 seconds
                StartCoroutine(DisableCollidersForSeconds(5f));
                isFormaDuchaActive = true;
                rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            if (isFormaDuchaActive)
            {
                // Allow movement while in "Forma Ducha" mode
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                Vector2 movement = new Vector2(horizontalInput, verticalInput);
                transform.Translate(movement * moveSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Laser = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Odepchniecie = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Boski_Ogien = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Bariera = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Atak_z_gory = true;
        }

  
}