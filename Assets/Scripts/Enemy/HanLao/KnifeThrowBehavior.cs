using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrowBehavior : StateMachineBehaviour
{

    //public float timer;
    //public float minTime;
    //public float maxTime;
    public float jumpForce;
    public GameObject hanLaoObject; //might need to clean this up
    public Actor hanLaoActor;
    public Rigidbody body;
    private float speed;
    private Vector3 direction;


    public GameObject player;
    private Transform playerPos;


    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //get objects and rigidbodies 
        player = GameObject.Find("Player");
        hanLaoObject = animator.transform.parent.gameObject;
        body = hanLaoObject.GetComponent<Rigidbody>();
        hanLaoActor = hanLaoObject.GetComponent<HanLao>();

        animator.SetBool("knifethrow", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (body.velocity.y <= 0)
        {
            animator.SetTrigger("fall");
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("jump");
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
