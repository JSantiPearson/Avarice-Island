using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAttackBehavior : StateMachineBehaviour
{

    public GameObject hanLaoObject; //might need to clean this up
    public Actor hanLaoActor;
    public GameObject player;
    private Transform playerPos;

    public Rigidbody body; 
    public float speed;
    private Vector3 direction;

    private bool isFacingLeft;
    const float groundAttackDist = 1.4f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Player");
        hanLaoObject = animator.transform.parent.gameObject;
        body = hanLaoObject.GetComponent<Rigidbody>();
        hanLaoActor = hanLaoObject.GetComponent<HanLao>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = player.transform;

        if(!Actor.IsCloseTo(body.position,player.transform.position,groundAttackDist)){
            
            /*if(hanLaoActor.phase == 1)
            {
                animator.SetTrigger("walk");
            }
            else if(hanLaoActor.phase == 2)
            {
                animator.SetTrigger("run");
            }*/
            animator.SetTrigger("walk");
        }
        else
        {
            animator.SetTrigger("idle");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
