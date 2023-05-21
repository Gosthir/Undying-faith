using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreScript : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject TutorialAndStory;
    public GameObject Story;
    public GameObject Tutorial;

    public void TutorialAndStoryOn()
    {
        MainMenu.SetActive(false);
        TutorialAndStory.SetActive(true);

    }

    public void TutorialAndStoryBack()
    {
        MainMenu.SetActive(true);
        TutorialAndStory.SetActive(false);
    }

    public void TutorialOn()
    {
        Tutorial.SetActive(true);
        TutorialAndStory.SetActive(false);
    }

    public void TutorialBack()
    {
        Tutorial.SetActive(false);
        TutorialAndStory.SetActive(true);
    }


    public void StoryOn()
    {
        Story.SetActive(true);
        TutorialAndStory.SetActive(false);
    }

    public void StoryBack()
    {
        Story.SetActive(false);
        TutorialAndStory.SetActive(true);
    }

    

    


}
