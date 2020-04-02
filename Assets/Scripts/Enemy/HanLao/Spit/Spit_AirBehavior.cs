using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit_AirBehavior : StateMachineBehaviour
{
    public Dialogue dialogue;
    public Rigidbody body;
    public GameObject spitObject; //might need to clean this up
    public SpitProjectilePhysics spitBehavior;
    public float upForce = 100f;
    public float overForce = 200f;
    public Vector3 playerDir;

    public GameObject player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spitObject = animator.gameObject;
        body = spitObject.GetComponent<Rigidbody>();
        spitBehavior = spitObject.GetComponent<SpitProjectilePhysics>();
        //speed = knifeBehavior.speed;

        player = GameObject.Find("Player");

        Vector3 playerPosition = player.transform.position;
        playerDir = playerPosition - body.position;
        playerDir.Normalize();

        spitBehavior.FlipSprite(playerDir.x <= 0);
        body.AddForce(playerDir * overForce);
        body.AddForce(Vector3.up * upForce);
        
        /*
        Vector3 velocity = playerDir * speed;
        body.velocity = velocity;
        */

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
