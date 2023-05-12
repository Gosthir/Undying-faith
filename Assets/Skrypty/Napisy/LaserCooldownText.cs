using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LaserCooldownText : MonoBehaviour
{
    public Abilities ability;

    public TMP_Text LaserCooldown;
    public GameObject LaserButton;

    // Update is called once per frame
    void Update()
    {
        //Laser
        int roundedTimer = Mathf.CeilToInt(ability.laserTimer);
        LaserCooldown.text = roundedTimer.ToString();
        LaserCooldown.gameObject.SetActive(roundedTimer != 0);

        Image buttonImage = LaserButton.GetComponent<Image>();
        if (ability.laserTimer > 0)
        {
            buttonImage.color = Color.red;
        }
        else
        {
            buttonImage.color = Color.white;
        }


    }
}
