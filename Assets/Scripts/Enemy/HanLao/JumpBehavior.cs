using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : StateMachineBehaviour
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

        float randX = Random.Range(-1f,1f);
        //float randY = Random.Range(0,1);
        float randZ = Random.Range(-1f,1f);


        //Vector3 horizontalVector = new Vector3(direction.x, 0, direction.z) * speed * 40;
        //Vector3 verticalVector = Vector3.up * jumpForce * Time.deltaTime;
        if (!animator.GetBool("knifethrow")){
            //trigger a jump
            //Vector3 horizontalVector = new Vector3(0, 0, 0);
            Vector3 randDirVector = new Vector3(randX, 0, randZ);
            randDirVector.Normalize();
            Vector3 horizontalVector = randDirVector * jumpForce;
            body.AddForce(horizontalVector);
        }
        Vector3 verticalVector = Vector3.up * jumpForce * 2;
        body.AddForce(verticalVector);
        //animator.ResetTrigger("jump");
        //timer = Random.Range(minTime,maxTime);   

        //start walking after jump
        animator.SetTrigger("walk");
    }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(body.velocity.y <= 0)
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
