using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image ProgressBarImage;
    public int AllEnemies;
    public int DeadEnemies;
    public int PlayerLevel = 1;
    public int SkillPoints = 0;

    private void Start()
    {
        // Count the number of enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        AllEnemies = enemies.Length;

        // Call UpdateProgressBar to set the initial progress bar value
        UpdateProgressBar();
    }

    private void Update()
    {
        // Call UpdateProgressBar every frame to update the progress bar value
        UpdateProgressBar();
        if(AllEnemies==DeadEnemies)
        {
            DeadEnemies = 0;
            SkillPoints++;
            PlayerLevel++;
        }
    }

    private void UpdateProgressBar()
    {
        // Calculate the fill amount based on the number of dead enemies and the total number of enemies
        float fillAmount = (float)DeadEnemies / AllEnemies;

        // Set the fill amount of the progress bar image
        ProgressBarImage.fillAmount = fillAmount;
    }
}