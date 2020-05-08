using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShenFallBehavior : StateMachineBehaviour
{
    public GameObject shenObject;
    public Actor shenActor;
    public GameObject player;
    public Rigidbody body;

    public float runSpeedmultiplier = 1.5f;
    private Vector3 direction;

    private bool isFacingLeft;
    const float groundAttackDist = 1.4f;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Player");
        shenObject = animator.transform.parent.gameObject;
        body = shenObject.GetComponent<Rigidbody>();
        shenActor = shenObject.GetComponent<Shen>();
        body.constraints = RigidbodyConstraints.FreezeAll;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
