using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Przyciski : MonoBehaviour
{

    public ProgressBar xp;
    public Abilities ability;
    private Button button;
    private bool hasClicked1 = false;
    private bool hasClicked2 = false;
    private bool hasClicked3 = false;
    private bool hasClicked4 = false;
    private bool hasClicked5 = false;
    private bool hasClicked6 = false;

    void Start()
    {
        // Get the button component
        button = GetComponent<Button>();

        // Set the normal color to 110E0E
        ColorBlock colors = button.colors;
        colors.normalColor = new Color(0.066f, 0.055f, 0.055f);
        colors.colorMultiplier = 1;
        button.colors = colors;
    }

    public void OnClickLaser()
    {
        if (xp.SkillPoints >= 1)
        {
            if (hasClicked1) return;
            ability.laser = true;
            ability.laserPoints++;
            xp.SkillPoints = xp.SkillPoints - 1;

            // Set the button's colors to white
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = Color.white;
            button.colors = colors;
            hasClicked1 = true;
            button.interactable = false;
        }
    }

    public void OnClickOdepchniecie()
    {
        if (xp.SkillPoints >= 1)
        {
            if (hasClicked2) return;
            ability.odepchniecie = true;
            ability.odepchnieciePoints++;
            xp.SkillPoints = xp.SkillPoints - 1;

            // Set the button's colors to white
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = Color.white;
            button.colors = colors;
            hasClicked2 = true;
            button.interactable = false;
        }
    }

    public void OnClickOgien()
    {
        if (xp.SkillPoints >= 1)
        {
            if (hasClicked3) return;
            ability.boskiOgien = true;
            ability.boskiOgienPoints++;
            xp.SkillPoints = xp.SkillPoints - 1;
            // Set the button's colors to white
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = Color.white;
            button.colors = colors;
            hasClicked3 = true;
            button.interactable = false;
        }
    }

    public void OnClickDuch()
    {
        if (xp.SkillPoints >= 1)
        {
            if (hasClicked4) return;
            ability.formaDucha = true;
            ability.formaDuchaPoints++;
            xp.SkillPoints = xp.SkillPoints - 1;
            // Set the button's colors to white
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = Color.white;
            button.colors = colors;
            hasClicked4 = true;
            button.interactable = false;
        }
    }

    public void OnClickDash()
    {
        if (xp.SkillPoints >= 1)
        {
            if (hasClicked5) return;
            ability.Dash = true;
            xp.SkillPoints = xp.SkillPoints - 1;
            // Set the button's colors to white
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = Color.white;
            button.colors = colors;
            hasClicked5 = true;
            button.interactable = false;
        }
    }

    public void OnClickMeteor()
    {
            if (xp.SkillPoints >= 1)
            {
                if (hasClicked6) return;
                ability.meteoryt = true;
                ability.meteorytPoints++;
                xp.SkillPoints = xp.SkillPoints - 1;
                // Set the button's colors to white
                ColorBlock colors = button.colors;
                colors.normalColor = Color.white;
                colors.highlightedColor = Color.white;
                button.colors = colors;
                hasClicked6 = true;
                button.interactable = false;

            }
    }

    public void OnClickBonusDMG()
    {
            if (xp.SkillPoints >= 1)
            {
                ability.BonusAttackDmgON = true;
                ability.BonusAttackDamagePoints++;
                xp.SkillPoints = xp.SkillPoints - 1;
                // Set the button's colors to white
                ColorBlock colors = button.colors;
                colors.normalColor = Color.white;
                colors.highlightedColor = Color.white;
                button.colors = colors;
            }
    }

    public void OnClickBonusHP()
    {
            if (xp.SkillPoints >= 1)
            {
                ability.BonusHealthOn = true;
                ability.BonusHealthPoints++;
                xp.SkillPoints = xp.SkillPoints - 1;
                // Set the button's colors to white
                ColorBlock colors = button.colors;
                colors.normalColor = Color.white;
                colors.highlightedColor = Color.white;
                button.colors = colors;


            }
    }
}