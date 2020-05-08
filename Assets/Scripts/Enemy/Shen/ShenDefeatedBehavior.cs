using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShenDefeatedBehavior : StateMachineBehaviour
{
    public Dialogue dialogue;
    public GameObject shenObject; //might need to clean this up
    public static bool hasBeenDefeated = false; //PART OF A QUICK FIX
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shenObject = animator.transform.parent.gameObject;
        dialogue = shenObject.GetComponent(typeof(Dialogue)) as Dialogue;
        if(!hasBeenDefeated){ //quick fix for han lao double defeated bug
            dialogue.PlayDialogue();
            hasBeenDefeated = true;
        }
        animator.ResetTrigger("Defeated");

        //hanLaoObject.GetComponent<HanLao>().Die();
        //animator.SetTrigger("exit");


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!dialogue.pausedForDialogue){
            GameManager.bossFightInProgress=false;
            dialogue.dialogueAnim.ResetTrigger("popup");
            shenObject.GetComponent<Shen>().Die();
        }
        //animator.ResetTrigger("defeated");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Destroy(hanLaoObject);
        //hanLaoObject.GetComponent<HanLao>().Die();

    }
}
