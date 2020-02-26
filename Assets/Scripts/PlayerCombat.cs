using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Hero hero;
    public InputHandler input;
    public bool isRunning;
    public Animator animator;
    public Transform attackPoint;
    public int attackDamage = 40;
    public float attackRange = 0.5f;
    public float attackRate = 1f;
    float nextAttackTime = 0f;
    public LayerMask enemyLayers;



    // Update is called once per frame
    void Update()
    {
      if(Time.time >= nextAttackTime){
        if (Input.GetButtonDown("Attack"))
        {


            Attack();
            nextAttackTime = Time.time + 1f/ attackRate; //lockout attack by a second
            //attack = true;
            //baseAnim.SetBool("attack", attack);

        }
      }

        else if (Input.GetButtonUp("Attack"))
        {
           // attack = false;
           // baseAnim.SetBool("attack", attack);

        }
    }
    void dashAttack(){
      animator.SetTrigger("Attack");
      Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
      foreach(Collider enemy in hitEnemies){
        //enemy.GetComponent<EnemyGrunt>().TakeDamage(attackDamage);
        enemy.GetComponent<EnemyGrunt>().Hit(30);
        Debug.Log("We Dash Attacked " + enemy.name);
      }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies){
          //enemy.GetComponent<EnemyGrunt>().Launch(GameObject.Find("Player").transform.position);
            enemy.GetComponent<EnemyGrunt>().Hit(30);
            //enemy.GetComponent<EnemyGrunt>().Hurt(15);
          Debug.Log("We Hit " + enemy.name);
        }
    }

    void OnDrawGizmosSelected(){

      Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
