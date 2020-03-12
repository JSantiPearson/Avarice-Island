using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBehavior : StateMachineBehaviour
{

    public float timer;
    public float minTime;
    public float maxTime;

    public GameObject hanLaoObject; //might need to clean this up
    public Actor hanLaoActor;
    public GameObject player;
    private Transform playerPos;

    public Rigidbody body; 
    public float speed;
    private Vector3 direction;

    private bool isFacingLeft;
    const float groundAttackDist = 0.8f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //playerPos = GameObject.Find("Player").GetComponent<Transform>();
        player = GameObject.Find("Player");
        hanLaoObject = GameObject.Find("HanLao");
        body = hanLaoObject.GetComponent<Rigidbody>();
        hanLaoActor = hanLaoObject.GetComponent<HanLao>();

        timer = Random.Range(minTime,maxTime);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = player.transform;
        //Debug.Log("Player position: " + player.transform.position);
        //Debug.Log("lao position: " + body.position);

        //timed jumps
        if(timer<=0){
            animator.SetTrigger("jump");
        } else {
            timer -= Time.deltaTime;
        }

        //walk either away or towards player
        //Vector3 target = new Vector3(playerPos.position.x, playerPos.position.y, playerPos.position.z);
        //Vector3 moveVector = Vector3.MoveTowards(animator.transform.position, target, speed*Time.deltaTime);
        Vector3 moveVector = playerPos.position - body.position;
        moveVector.Normalize();
        hanLaoActor.FlipSprite(moveVector.x < 0);
        //Debug.Log("MOVEVECTOR: "+ moveVector.x + "  " +   moveVector.y + "  " + moveVector.z + "  ");
        //Debug.Log("MOVEVECTOR: "+ moveVector);
        if(Actor.IsCloseTo(body.position,playerPos.position, groundAttackDist)){
            animator.SetTrigger("groundattack");
        }

        //moveVector.Normalize();
        //check for sprite flip and move
        //isFacingLeft = moveVector.x < 0;
       //hanLaoActor.FlipSprite(isFacingLeft);
        body.MovePosition(body.position+moveVector*Time.deltaTime);


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    //NEED TO MAKE THIS INTO A STATIC METHOD IN A HIGHER LEVEL CLASS
    /*private bool IsCloseTo(Vector3 target, Vector3 position)
    {
        float diffX = System.Math.Abs(target.x - position.x);
        float diffY = System.Math.Abs(target.y - position.y);
        float diffZ = System.Math.Abs(target.z - position.z);

        //return diffX <= 0.1 && diffY <= 0.1 && diffZ <= 0.1;
        return diffX <= 0.8 && diffY <= 0.8 && diffZ <= 0.8;
    }*/

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
