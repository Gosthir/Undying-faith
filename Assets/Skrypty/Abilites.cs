using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Abilites : MonoBehaviour
{
    public bool Laser = false;
    public int LaserPoints = 0;

    public bool Odepchniecie = false;
    public int Odepchniecie_Points = 0;

    public bool Boski_Ogien = false;
    public int Boski_Ogien_Points = 0;

    public bool Forma_Ducha = false;
    public int Forma_Ducha_Points = 0;

    public bool Bariera = false;
    public int Bariera_Points = 0;

    public bool Atak_z_gory = false;
    public int Atak_z_gory_Points = 0;

    public bool Oslepienie = false;
    public int Oslepienie_Points = 0;

    public bool Meteoryt = false;
    public int Meteoryt_Points = 0;


    //FORMA DUCHA
    private IEnumerator DisableCollidersForSeconds(float seconds)
    {
        // Disable collisions between the "Player" layer and the "Enemies" layer
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
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == playerLayer || collider.gameObject.layer == enemyLayer)
            {
                collider.enabled = true;
            }
        }
    }



    private void Update()
    {
        if (Forma_Ducha == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                // Call coroutine to disable colliders for 5 seconds
                StartCoroutine(DisableCollidersForSeconds(5f));
                Physics2D.IgnoreLayerCollision(3, 10, false);
            }


        }
    }
}