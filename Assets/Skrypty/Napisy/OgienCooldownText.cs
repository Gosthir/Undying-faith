using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OgienCooldownText : MonoBehaviour
{
    public Abilities ability;

    public TMP_Text OgienCooldown;
    public GameObject OgienButton;


    // Update is called once per frame
    void Update()
    {
        //Ogien
        int roundedTimer = Mathf.CeilToInt(ability.ogienTimer);
        OgienCooldown.text = roundedTimer.ToString();
        OgienCooldown.gameObject.SetActive(roundedTimer != 0);

        Image buttonImage = OgienButton.GetComponent<Image>();
        if (ability.ogienTimer > 0)
        {
            buttonImage.color = Color.red;
        }
        else
        {
            buttonImage.color = Color.white;
        }

    }
}
