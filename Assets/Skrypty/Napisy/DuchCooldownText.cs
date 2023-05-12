using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuchCooldownText : MonoBehaviour
{
    public Abilities ability;



    public TMP_Text DuchCooldown;
    public GameObject DuchButton;


    // Update is called once per frame
    void Update()
    {
        //duch
        int roundedTimer = Mathf.CeilToInt(ability.duchTimer);
        DuchCooldown.text = roundedTimer.ToString();
        DuchCooldown.gameObject.SetActive(roundedTimer != 0);

        Image buttonImage = DuchButton.GetComponent<Image>();
        if (ability.duchTimer > 0)
        {
            buttonImage.color = Color.red;
        }
        else
        {
            buttonImage.color = Color.white;
        }
    }
}
