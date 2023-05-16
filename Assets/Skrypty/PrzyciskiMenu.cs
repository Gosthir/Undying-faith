using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrzyciskiMenu : MonoBehaviour

{

    public GameObject GameOverScreen;


    public void Playgame()
    {
        SceneManager.LoadScene(1);
        GameOverScreen.SetActive(false);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}