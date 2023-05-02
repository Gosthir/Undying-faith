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

    public bool AtakZGory = false;
    public int atakZGoryPoints = 0;

    public bool oslepienie = false;
    public int oslepieniePoints = 0;

    public bool meteoryt = false;
    public int meteorytPoints = 0;


    private void Start()
    {
        
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

    private void Update()
    {
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
            boskiOgien = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            formaDucha = true;
            StartCoroutine(DisableEnemyAttacksForSeconds(2f + formaDuchaPoints));
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