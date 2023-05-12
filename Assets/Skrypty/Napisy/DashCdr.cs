using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DashCdr : MonoBehaviour
{
    public Abilities ability;

    public TMP_Text DashCooldown;
    public GameObject DashButton;


    // Update is called once per frame
    void Update()
    {




        //Dash 
        int roundedTimer = Mathf.CeilToInt(ability.dashTimer);
        DashCooldown.text = roundedTimer.ToString();
        DashCooldown.gameObject.SetActive(roundedTimer != 0);

        Image buttonImage = DashButton.GetComponent<Image>();
        if (ability.dashTimer > 0)
        {
            buttonImage.color = Color.red;
        }
        else
        {
            buttonImage.color = Color.white;
        }


    }
}
