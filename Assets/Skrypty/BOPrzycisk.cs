using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOPrzycisk : MonoBehaviour
{
    public ProgressBar xp;
    public Abilities ability;

    public void OnClick()
    {
        if (xp.SkillPoints >= 1)
        {
            ability.boskiOgien = true;
            ability.boskiOgienPoints++;
            xp.SkillPoints = xp.SkillPoints - 1;
        }
    }
}