using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeteorCooldownText : MonoBehaviour
{
    public Abilities ability;



    public TMP_Text MeteorCooldown;
    public GameObject MeteorButton;


    // Update is called once per frame
    void Update()
    {

        //Meteor
        int roundedTimer = Mathf.CeilToInt(ability.meteorytTimer);
        MeteorCooldown.text = roundedTimer.ToString();
        MeteorCooldown.gameObject.SetActive(roundedTimer != 0);

        Image buttonImage = MeteorButton.GetComponent<Image>();
        if (ability.meteorytTimer > 0)
        {
            buttonImage.color = Color.red;
        }
        else
        {
            buttonImage.color = Color.white;
        }
    }
}
