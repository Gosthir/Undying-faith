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
        // Disable colliders for all layers except the "Floor", "Layer3", and "Layer7" layers
        int floorLayer = LayerMask.NameToLayer("Floor");
        int layer3 = LayerMask.NameToLayer("Enemies");
        int layer7 = LayerMask.NameToLayer("Player");
        int layerMask = ~(1 << floorLayer | 1 << layer3 | 1 << layer7);
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer != floorLayer && collider.gameObject.layer != layer3 && collider.gameObject.layer != layer7)
            {
                collider.enabled = false;
                collider.gameObject.layer = floorLayer;
            }
        }

        // Set transparency of hero to 25%
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color spriteColor = spriteRenderer.color;
        spriteColor.a = 0.25f;
        spriteRenderer.color = spriteColor;

        // Wait for specified seconds
        yield return new WaitForSeconds(2f + Forma_Ducha_Points);

        // Reset transparency of hero to 100%
        spriteColor.a = 1f;
        spriteRenderer.color = spriteColor;

        // Enable colliders for all layers
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = true;
            collider.gameObject.layer = 0;
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
            }


        }
    }
}