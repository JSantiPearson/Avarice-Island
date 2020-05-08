using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShenIdleBehavior : StateMachineBehaviour
{
    public GameObject shenObject;
    public Shen shenActor;
    public GameObject player;
    public Rigidbody body;
    
    private float walkSpeed;
    private float speed;
    private Vector3 direction;

    private bool isFacingLeft;
    const float groundAttackDist = 1.4f;
    private float attackThreshold = 0.6f;
    private float axeKickThreshold = 0.1f;
    private float roarThreshold = 0.05f;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Player");
        shenObject = animator.transform.parent.gameObject;
        body = shenObject.GetComponent<Rigidbody>();
        shenActor = shenObject.GetComponent<Shen>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Check that the player is nearby
        if (!Actor.IsCloseTo(body.position, player.transform.position, groundAttackDist))
        {
            animator.SetFloat("Speed", 2.5f);
        }

        //Select and execute attack
        float rand = Random.value;
        if(rand <= roarThreshold)
        {
            animator.SetTrigger("Roar");
            shenActor.comboCounter = 0;
        }
        else if (rand <= axeKickThreshold)
        {
            animator.SetTrigger("AxeKick");
            shenActor.comboCounter = 0;
        }
        else if (rand <= attackThreshold)
        {
            switch (shenActor.comboCounter)
            {
                case 0:
                    animator.SetTrigger("Punch");
                    break;
                case 1:
                    animator.SetTrigger("Kick");
                    break;
                default:
                    break;
            }

        }
        else
        {
            shenActor.comboCounter = 0;
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
