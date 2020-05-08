using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShenRunBehavior : StateMachineBehaviour
{
    public GameObject shenObject;
    public Actor shenActor;
    public GameObject player;
    public Rigidbody body;
    
    public float walkSpeed;
    public float runSpeed;
    public float speed;
    private Vector3 direction;

    private bool isFacingLeft;
    const float groundAttackDist = 1.4f;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Player");
        shenObject = animator.transform.parent.gameObject;
        body = shenObject.GetComponent<Rigidbody>();
        shenActor = shenObject.GetComponent<Shen>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
