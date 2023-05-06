using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class Bossfight : MonoBehaviour
{
    public Animator B_animator;
    private int NumberOfAttack = 4;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            NumberOfAttack = Random.Range(0, 3);
        }
        switch (NumberOfAttack)
        {
            case 0:
                B_animator.SetTrigger("Attack1");
                NumberOfAttack = 4;
                return;
            case 1:
                B_animator.SetTrigger("Attack2");
                NumberOfAttack = 4;
                return;
            case 2:
                B_animator.SetTrigger("Attack3");
                NumberOfAttack = 4;
                return;
            case 3:
                B_animator.SetTrigger("run");
                NumberOfAttack = 4;
                return;
        }
    }
}
