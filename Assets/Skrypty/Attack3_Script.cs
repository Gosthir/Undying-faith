using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Attack3_Script : MonoBehaviour
{
    public HeroKnight player;
    public Collider2D[] Boss_Attack3_Radius;
    public int Dmg_Of_Attack3;
    // Start is called before the first frame update
    void Start()
    {
        Collider2D[] hitHero = Physics2D.OverlapBoxAll(new Vector2(0,0), new Vector2(10,10), 90); ;
        foreach (Collider2D player in hitHero)
        {
            if (player.CompareTag("Player"))
            {
                player.GetComponent<HeroKnight>().TakeDamageHero(Dmg_Of_Attack3);
            }
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
