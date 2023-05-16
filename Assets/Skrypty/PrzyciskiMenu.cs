using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
<<<<<<< HEAD

=======
>>>>>>> 1a718e983296322e74ef9cc36e46dfa25c0b37b5
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