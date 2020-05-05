using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatedBehavior : StateMachineBehaviour
{
    public Dialogue dialogue;
    public GameObject hanLaoObject; //might need to clean this up
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("enter defeated state");
        hanLaoObject = animator.transform.parent.gameObject;
        dialogue = hanLaoObject.GetComponent(typeof(Dialogue)) as Dialogue;
        dialogue.PlayDialogue();
        animator.ResetTrigger("defeated");

        //hanLaoObject.GetComponent<HanLao>().Die();
        //animator.SetTrigger("exit");


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!dialogue.pausedForDialogue){
            GameManager.bossFightInProgress=false;
            dialogue.dialogueAnim.ResetTrigger("popup");
            hanLaoObject.GetComponent<HanLao>().Die();
        }
        //animator.ResetTrigger("defeated");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Destroy(hanLaoObject);
        //hanLaoObject.GetComponent<HanLao>().Die();

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
