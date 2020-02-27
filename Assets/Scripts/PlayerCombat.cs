using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Hero hero;
    public InputHandler input;
    public bool isRunning;
   // public
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
                if (hero.isRunning)
                {
                   
                    dashAttack();
                    
                }
                else
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate; //lockout attack by a second

                    //attack = true;
                    //baseAnim.SetBool("attack", attack);
                }



            }
      }

        else if (Input.GetButtonUp("Attack"))
        {
           // attack = false;
           // baseAnim.SetBool("attack", attack);

        }
    }
    void dashAttack(){
       // hero.body.velocity = rigidbody.velocity * 0.9;
        animator.SetTrigger("Attack");
      Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        
        foreach (Collider enemy in hitEnemies){
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

            //WARNING This is a temporary jank ass fix because i can't find real hitboxes assocciated with these attacks
            if(hero.speed > 3)
            {
                enemy.GetComponent<EnemyGrunt>().Launch(GameObject.Find("Player").transform.position);
                enemy.GetComponent<EnemyGrunt>().Hurt(30);
            }
            else
            {
                enemy.GetComponent<EnemyGrunt>().Hit(15);
            }
            //This is the end of the jank ass fix
            
            Debug.Log("We Hit " + enemy.name);
        }
    }

    void OnDrawGizmosSelected(){

      Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
