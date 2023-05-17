using System.Collections;
using UnityEngine;

public class Attack3_Script : MonoBehaviour
{
    public HeroKnight playerr;
    public int Dmg_Of_Attack3 = 10;
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(Bossattack3());
    }
    IEnumerator Bossattack3()
    {
        yield return new WaitForSeconds(0.5f);
        playerr = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroKnight>();
        Collider2D[] hitHero = Physics2D.OverlapBoxAll(new Vector2(0, 0), new Vector2(2.7f, 15), 90); ;
        foreach (Collider2D playerr in hitHero)
        {
            if (playerr.CompareTag("Player"))
            {
                playerr.GetComponent<HeroKnight>().TakeDamageHero(Dmg_Of_Attack3);

            }
        }
    Destroy(gameObject);
    }

}
