﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : StateMachineBehaviour
{

    //public float timer;
    //public float minTime;
    //public float maxTime;
    public float jumpForce;
    public GameObject hanLao;
    public Rigidbody body; 
    private float speed;
    private Vector3 direction;


    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hanLao = GameObject.Find("HanLao");
        body = hanLao.GetComponent<Rigidbody>(); 

        //Vector3 horizontalVector = new Vector3(direction.x, 0, direction.z) * speed * 40;
        //Vector3 verticalVector = Vector3.up * jumpForce * Time.deltaTime;

        //trigger a jump
        Vector3 horizontalVector = new Vector3(0, 0, 0);
        Vector3 verticalVector = Vector3.up * jumpForce;
        body.AddForce(horizontalVector);
        body.AddForce(verticalVector);
        //animator.ResetTrigger("jump");
        //timer = Random.Range(minTime,maxTime);   
    }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //if(timer<=0){
            //animator.SetTrigger("idle");
        //} else {
       //      timer -= Time.deltaTime;
       // }
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
