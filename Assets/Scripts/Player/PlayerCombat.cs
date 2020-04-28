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

    private const string GRUNT = "EnemyGrunt(Clone)";
    private const string RAVE_BOY = "RaveBoy(Clone)";
    private const string RAVE_GIRL = "RaveGirl(Clone)";
    private const string BOUNCER_REX = "BouncerRex(Clone)";
    private const string BOUNCER_BRAD = "BouncerBrad(Clone)";
    private const string BLASTER = "Blaster(Clone)";
    private const string HAN_LAO = "HanLao(Clone)";
    private const string SHEN = "Shen(Clone)";


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
        { //I suspect that "Attack Colliders" are useless because enemies already have rigid bodies.
            switch (enemy.name)
            {
                case GRUNT:
                    enemy.GetComponent<EnemyGrunt>().Launch(GameObject.Find("Player").transform.position);
                    enemy.GetComponent<EnemyGrunt>().Hurt(30);
                    break;
                case BLASTER:
                    enemy.GetComponent<Blaster>().Launch(GameObject.Find("Player").transform.position);
                    enemy.GetComponent<Blaster>().Hurt(30);
                    break;
                case RAVE_BOY:
                    enemy.GetComponent<RaveBoy>().Launch(GameObject.Find("Player").transform.position);
                    enemy.GetComponent<RaveBoy>().Hurt(30);
                    break;
                case RAVE_GIRL:
                    enemy.GetComponent<RaveGirl>().Launch(GameObject.Find("Player").transform.position);
                    enemy.GetComponent<RaveGirl>().Hurt(30);
                    break;
                case BOUNCER_BRAD:
                    enemy.GetComponent<Bouncer>().Launch(GameObject.Find("Player").transform.position);
                    enemy.GetComponent<Bouncer>().Hurt(30);
                    break;
                case BOUNCER_REX:
                    enemy.GetComponent<Bouncer>().Launch(GameObject.Find("Player").transform.position);
                    enemy.GetComponent<Bouncer>().Hurt(30);
                    break;
                case HAN_LAO:
                    enemy.GetComponent<HanLao>().Launch(GameObject.Find("Player").transform.position);
                    enemy.GetComponent<HanLao>().Hurt(15);
                    break;
                case SHEN:
                    enemy.GetComponent<Shen>().Hurt(15);
                    break;
                default:
                    break;
            }

        }
        bufferAttackCount = 0;
    }

    void Attack(Collider col)
    {

        animator.SetTrigger("Attack");

        Collider[] hitEnemies = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            switch (enemy.name)
            {
                case GRUNT:
                    enemy.GetComponent<EnemyGrunt>().Hurt(15);
                    break;
                case BLASTER:
                    enemy.GetComponent<Blaster>().Hurt(15);
                    break;
                case RAVE_BOY:
                    enemy.GetComponent<RaveBoy>().Hurt(15);
                    break;
                case RAVE_GIRL:
                    enemy.GetComponent<RaveGirl>().Hurt(15);
                    break;
                case BOUNCER_BRAD:
                    enemy.GetComponent<Bouncer>().Hurt(15);
                    break;
                case BOUNCER_REX:
                    enemy.GetComponent<Bouncer>().Hurt(15);
                    break;
                case HAN_LAO:
                    enemy.GetComponent<HanLao>().Hurt(15);
                    break;
                case SHEN:
                    enemy.GetComponent<Shen>().Hurt(15);
                    break;
                default:
                    break;
            }
            if (bufferAttackCount > 1)
            {
                animator.SetTrigger("Attack2");
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
            switch (enemy.name)
            {
                case GRUNT:
                    enemy.GetComponent<EnemyGrunt>().Hurt(15);
                    break;
                case BLASTER:
                    enemy.GetComponent<Blaster>().Hurt(15);
                    break;
                case RAVE_BOY:
                    enemy.GetComponent<RaveBoy>().Hurt(15);
                    break;
                case RAVE_GIRL:
                    enemy.GetComponent<RaveGirl>().Hurt(15);
                    break;
                case BOUNCER_BRAD:
                    enemy.GetComponent<Bouncer>().Hurt(15);
                    break;
                case BOUNCER_REX:
                    enemy.GetComponent<Bouncer>().Hurt(15);
                    break;
                case HAN_LAO:
                    enemy.GetComponent<HanLao>().Hurt(15);
                    break;
                case SHEN:
                    enemy.GetComponent<Shen>().Hurt(15);
                    break;
                default:
                    break;
            }
            if (bufferAttackCount > 2)
            {
                animator.SetTrigger("Attack3");
                // Attack3(attackHitboxes[0]); //standing attack 1 and 3
            }
        }
        if (bufferAttackCount > 2)
        {
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
            //hitEnemies = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, enemyLayers);

            switch (enemy.name)
            {
                case GRUNT:
                    enemy.GetComponent<EnemyGrunt>().Launch(GameObject.Find("Player").transform.position);
                    enemy.GetComponent<EnemyGrunt>().Hurt(30);
                    break;
                case BLASTER:
                    enemy.GetComponent<Blaster>().Launch(GameObject.Find("Player").transform.position);
                    enemy.GetComponent<Blaster>().Hurt(30);
                    break;
                case RAVE_BOY:
                    enemy.GetComponent<RaveBoy>().Launch(GameObject.Find("Player").transform.position);
                    enemy.GetComponent<RaveBoy>().Hurt(30);
                    break;
                case RAVE_GIRL:
                    enemy.GetComponent<RaveGirl>().Launch(GameObject.Find("Player").transform.position);
                    enemy.GetComponent<RaveGirl>().Hurt(30);
                    break;
                case BOUNCER_BRAD:
                    enemy.GetComponent<Bouncer>().Launch(GameObject.Find("Player").transform.position);
                    enemy.GetComponent<Bouncer>().Hurt(30);
                    break;
                case BOUNCER_REX:
                    enemy.GetComponent<Bouncer>().Launch(GameObject.Find("Player").transform.position);
                    enemy.GetComponent<Bouncer>().Hurt(30);
                    break;
                case HAN_LAO:
                    enemy.GetComponent<HanLao>().Hurt(15);
                    break;
                case SHEN:
                    enemy.GetComponent<Shen>().Hurt(15);
                    break;
                default:
                    break;
            }


        }
        bufferAttackCount = 0;



    }
    public int isAttacking(bool isAttacking)
    {
        inAttackAnim = isAttacking;
        return 220;
    }


}
