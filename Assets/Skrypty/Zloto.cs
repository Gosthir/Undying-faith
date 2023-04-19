using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zloto : MonoBehaviour
{
    private HeroKnight heroKnight;
    public Text goldText;

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve the HeroKnight script from the player sprite's GameObject
        heroKnight = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroKnight>();

        // Set the initial text value to display the starting gold value
        goldText.text = heroKnight.Gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the text value every frame to display the current gold value
        goldText.text = heroKnight.Gold.ToString();
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zloto : MonoBehaviour
{
    int Gold = 0;
    public Text GoldText;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial text value to display the starting gold value
        GoldText.text = Gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the text value every frame to display the current gold value
        GoldText.text = Gold.ToString();
    }
} */