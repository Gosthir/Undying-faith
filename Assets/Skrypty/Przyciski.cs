using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Przyciski : MonoBehaviour
{
    public ProgressBar xp;
    public Abilities ability;
    private Button odepchniecieButton;

    void Start()
    {
        // Find the "Odepchniecie" button in the scene
        odepchniecieButton = GameObject.Find("Odepchniecie").GetComponent<Button>();
    }


    public void OnClickLaser()
    {
        if (xp.SkillPoints >= 1)
        {
            ability.laser = true;
            ability.laserPoints++;
            xp.SkillPoints = xp.SkillPoints - 1;
            // Get a reference to the button component
            Button thisButton = GetComponent<Button>();

            // Get the original color of the button
            ColorBlock colors = thisButton.colors;
            Color originalColor = colors.normalColor;

            // Set the button's colors to its original colors
            colors.normalColor = originalColor;
            thisButton.colors = colors;
        }
    }

    public void OnClickOdepchniecie()
    {
        if (xp.SkillPoints >= 1)
        {
            ability.odepchniecie = true;
            ability.odepchnieciePoints++;
            xp.SkillPoints = xp.SkillPoints - 1;

            // Set the button's colors to its default colors

            ColorBlock colors = odepchniecieButton.colors;
            colors.normalColor = odepchniecieButton.colors.normalColor;
            colors.highlightedColor = odepchniecieButton.colors.highlightedColor;
            colors.pressedColor = odepchniecieButton.colors.pressedColor;
            colors.disabledColor = odepchniecieButton.colors.disabledColor;
            colors.colorMultiplier = odepchniecieButton.colors.colorMultiplier;

            odepchniecieButton.colors = colors;
        }
    }


    public void OnClickOgien()
    {
        if (xp.SkillPoints >= 1)
        {
            ability.boskiOgien = true;
            ability.boskiOgienPoints++;
            xp.SkillPoints = xp.SkillPoints - 1;
            // Get a reference to the button component
            Button thisButton = GetComponent<Button>();

            // Get the original color of the button
            ColorBlock colors = thisButton.colors;
            Color originalColor = colors.normalColor;

            // Set the button's colors to its original colors
            colors.normalColor = originalColor;
            thisButton.colors = colors;
        }
    }

    public void OnClickDuch()
    {
        if (xp.SkillPoints >= 1)
        {
            ability.formaDucha = true;
            ability.formaDuchaPoints++;
            xp.SkillPoints = xp.SkillPoints - 1;
            // Get a reference to the button component
            Button thisButton = GetComponent<Button>();

            // Get the original color of the button
            ColorBlock colors = thisButton.colors;
            Color originalColor = colors.normalColor;

            // Set the button's colors to its original colors
            colors.normalColor = originalColor;
            thisButton.colors = colors;
        }
    }

    public void OnClickDash()
    {
        if (xp.SkillPoints >= 1)
        {
            ability.Dash = true;
            xp.SkillPoints = xp.SkillPoints - 1;
            // Get a reference to the button component
            Button thisButton = GetComponent<Button>();

            // Get the original color of the button
            ColorBlock colors = thisButton.colors;
            Color originalColor = colors.normalColor;

            // Set the button's colors to its original colors
            colors.normalColor = originalColor;
            thisButton.colors = colors;
        }
    }

    public void OnClickMeteor()
    {
        if (xp.SkillPoints >= 1)
        {
            ability.meteoryt = true;
            ability.meteorytPoints++;
            xp.SkillPoints = xp.SkillPoints - 1;
            // Get a reference to the button component
            Button thisButton = GetComponent<Button>();

            // Get the original color of the button
            ColorBlock colors = thisButton.colors;
            Color originalColor = colors.normalColor;

            // Set the button's colors to its original colors
            colors.normalColor = originalColor;
            thisButton.colors = colors;
        }
    }

    public void OnClickBonusDMG()
    {
        if (xp.SkillPoints >= 1)
        {
            ability.BonusAttackDmgON = true;
            ability.BonusAttackDamagePoints++;
            xp.SkillPoints = xp.SkillPoints - 1;
            // Get a reference to the button component
            Button thisButton = GetComponent<Button>();

            // Get the original color of the button
            ColorBlock colors = thisButton.colors;
            Color originalColor = colors.normalColor;

            // Set the button's colors to its original colors
            colors.normalColor = originalColor;
            thisButton.colors = colors;
        }
    }

    public void OnClickBonusHP()
    {
        if (xp.SkillPoints >= 1)
        {
            ability.BonusHealthOn = true;
            ability.BonusHealthPoints++;
            xp.SkillPoints = xp.SkillPoints - 1;
            // Get a reference to the button component
            Button thisButton = GetComponent<Button>();

            // Get the original color of the button
            ColorBlock colors = thisButton.colors;
            Color originalColor = colors.normalColor;

            // Set the button's colors to its original colors
            colors.normalColor = originalColor;
            thisButton.colors = colors;
        }
    }
}