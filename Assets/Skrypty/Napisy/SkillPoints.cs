using UnityEngine;
using TMPro;

public class SkillPoints : MonoBehaviour
{
    public ProgressBar ability;
    public TMP_Text SkillPointsText;

    void Start()
    {
        UpdateNumberText();
    }

    void Update()
    {
        UpdateNumberText();
    }


    public void UpdateNumberText()
    {
        SkillPointsText.text = ability.SkillPoints.ToString();
    }
}