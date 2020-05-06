using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerCombat : MonoBehaviour
{
    public Hero hero;
    public InputHandler input;
    public bool isRunning;
    // public
    public Animator animator;
    public float attackRate = .01f;
    float nextAttackTime = 0f;
    int bufferAttackCount = 0;
    public bool inAttackAnim;

    // Queue<bool> attackBuffer = new Queue<bool>();

    //hit effects

    void start()
    {
        isAttacking(false);
    }
    // Update is called once per frame
    void Update()
    {

        if (!inAttackAnim)
        {
            if (Input.GetButtonDown("Attack"))
            {
                if (hero.speed > 3)
                {
                    dashAttack();
                    bufferAttackCount = 0;
                }
                else
                {
                    Attack(); //0 is first standing attack
                    bufferAttackCount = 0;
                }
            }
        }
        if (Input.GetButtonDown("Attack"))
        {
            bufferAttackCount++;
        }
    }
    void dashAttack()
    {
        // hero.body.velocity = rigidbody.velocity * 0.9;
        animator.SetTrigger("DashAttack");
        bufferAttackCount = 0;
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        if (bufferAttackCount > 1)
        {
            animator.SetTrigger("Attack2");
            Attack2();
        }
        else
        {
            bufferAttackCount = 0;
        }

    }
    void Attack2()
    {
        if (bufferAttackCount > 2)
        {
            animator.SetTrigger("Attack3");
            Attack3(); //standing attack 1 and 3
        }
        bufferAttackCount = 0;


    }
    void Attack3()
    {
        // bufferAttack = false;
        bufferAttackCount = 0;
    }
    public int isAttacking(bool isAttacking)
    {
        inAttackAnim = isAttacking;
        return 220;
    }


}
