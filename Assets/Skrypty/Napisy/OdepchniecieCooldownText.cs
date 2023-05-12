using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OdepchniecieCooldownText : MonoBehaviour
{
    public Abilities ability;

    public TMP_Text OdepchniecieCooldown;
    public GameObject OdepchniecieButton;


    void Update()
    {


        //Odepchniecie
        int roundedTimer = Mathf.CeilToInt(ability.odepchniecieTimer);
        OdepchniecieCooldown.text = roundedTimer.ToString();
        OdepchniecieCooldown.gameObject.SetActive(roundedTimer != 0);

        Image buttonImage = OdepchniecieButton.GetComponent<Image>();
        if (ability.odepchniecieTimer > 0)
        {
            buttonImage.color = Color.red;
        }
        else
        {
            buttonImage.color = Color.white;
        }


    }
}