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
    public LayerMask enemyLayers;
    public Collider[] attackHitboxes;
    //[0] is standing attack 1 and 3
    //[1] is standing attack 2
    //[2] is dash attack
    int bufferAttackCount = 0;
    public bool inAttackAnim;

    // Queue<bool> attackBuffer = new Queue<bool>();

    //hit effects
    public GameObject hitEffectPrefab;


    void start()
    {
        isAttacking(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (!inAttackAnim)
        {
            //Debug.Log("Time:"  + Time.time);
            // Debug.Log("Next Attack Time:"  + nextAttackTime);
            if (Input.GetButtonDown("Attack"))
            {
                if (hero.speed > 3)
                {
                    dashAttack(attackHitboxes[2]);
                    bufferAttackCount = 0;
                }
                else
                {
                    Attack(attackHitboxes[0]); //0 is first standing attack
                    nextAttackTime = Time.time + attackRate; //lockout attack by a second
                    bufferAttackCount = 0;
                }
            }
        }
        if (Input.GetButtonDown("Attack"))
        {
            bufferAttackCount++;
        }
    }
    void dashAttack(Collider col)
    {
        // hero.body.velocity = rigidbody.velocity * 0.9;
        animator.SetTrigger("DashAttack");
        Collider[] hitEnemies = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyGrunt>().Launch(GameObject.Find("Player").transform.position);
            enemy.GetComponent<EnemyGrunt>().Hurt(30);
            Debug.Log("We Dash Attacked " + enemy.name);

            enemy.GetComponent<HanLao>().Hurt(15);

        }
        bufferAttackCount = 0;
    }

    void Attack(Collider col)
    {

        animator.SetTrigger("Attack");

        Collider[] hitEnemies = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log(enemy.name);
            enemy.GetComponent<EnemyGrunt>().Hit(15);
            Debug.Log("We Hit " + enemy.name);
            if (bufferAttackCount > 1)
            {
                animator.SetTrigger("Attack2");
                Instantiate(hitEffectPrefab,enemy.transform.position,enemy.transform.rotation);
                // Attack2(attackHitboxes[1]);
            }
        }

        if (bufferAttackCount > 1)
        {
            animator.SetTrigger("Attack2");
            Attack2(attackHitboxes[1]);
        }
        else
        {
            bufferAttackCount = 0;
        }

    }
    void Attack2(Collider col)
    {

        Collider[] hitEnemies = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log(enemy.name);
            enemy.GetComponent<EnemyGrunt>().Hit(15);
            Debug.Log("We Hit " + enemy.name);
            if (bufferAttackCount > 2)
            {
                //  Debug.Log("Buffer Into Attack 3 " + bufferAttackCount);
                animator.SetTrigger("Attack3");
                // Attack3(attackHitboxes[0]); //standing attack 1 and 3
            }
        }
        if (bufferAttackCount > 2)
        {
            //  Debug.Log("Buffer Into Attack 3 " + bufferAttackCount);
            animator.SetTrigger("Attack3");
            Attack3(attackHitboxes[0]); //standing attack 1 and 3
        }
        bufferAttackCount = 0;


    }
    void Attack3(Collider col)
    {

        // bufferAttack = false;

        Collider[] hitEnemies = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            hitEnemies = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, enemyLayers);

            enemy.GetComponent<EnemyGrunt>().Launch(GameObject.Find("Player").transform.position);
            enemy.GetComponent<EnemyGrunt>().Hurt(30);


            Debug.Log("We Hit " + enemy.name);

        }
        // Debug.Log("Buffer after Attack 3 " + bufferAttackCount);

        bufferAttackCount = 0;



    }
    public int isAttacking(bool isAttacking)
    {
        inAttackAnim = isAttacking;
        return 220;
    }


}
