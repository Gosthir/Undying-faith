using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class Attack3_Script : MonoBehaviour
{
    public HeroKnight playerr;
    public int Dmg_Of_Attack3 = 10;
    private float timer = 0;
    private new float animation = 4;
    AudioManager audioManager;
    // Start is called before the first frame update

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        StartCoroutine(bossattack3());
    }
    IEnumerator bossattack3()
    {
        yield return new WaitForSeconds(0.5f);
        playerr = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroKnight>();
        Collider2D[] hitHero = Physics2D.OverlapBoxAll(new Vector2(0, 0), new Vector2(2.7f, 15), 90); ;
        foreach (Collider2D playerr in hitHero)
        {
            if (playerr.CompareTag("Player"))
            {
                playerr.GetComponent<HeroKnight>().TakeDamageHero(Dmg_Of_Attack3);


    int randomClipNumber = Random.Range(1, 3);
    AudioClip attackClip = null;

                switch (randomClipNumber)
                {
                    case 1:
                        attackClip = audioManager.Atak1;
                        break;
                    case 2:
                        attackClip = audioManager.Atak2;
                        break;
                }
            }
        }
    Destroy(gameObject);
    }

}
