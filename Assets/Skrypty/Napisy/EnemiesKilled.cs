using TMPro;
using UnityEngine;

public class EnemiesKilled : MonoBehaviour
{
    public ProgressBar ability;
    public TMP_Text EnemiesKilledText;

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
        EnemiesKilledText.text = ability.DeadEnemiesDisplay.ToString();
    }
}