using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Bossfight : MonoBehaviour
{
    [Header("Other")]
    public Animator B_animator;
    private int NumberOfAttack = 4;
    public HeroKnight player;
    public GameObject Laser;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            B_animator.SetTrigger("Attack3");
            Boss_Attack();
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
                Boss_Attack();
                NumberOfAttack = 4;
                return;
            case 3:
                B_animator.SetTrigger("run");
                NumberOfAttack = 4;
                return;
        }
    }
    private void Boss_Attack()
    {
        Instantiate(Laser, new Vector3(0, 0, 0), quaternion.EulerXYZ(90,90,90));

    }
}
