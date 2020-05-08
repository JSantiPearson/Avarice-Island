using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShenJumpBehavior : StateMachineBehaviour
{
    public GameObject shenObject;
    public Actor shenActor;
    public GameObject player;
    public Rigidbody body;

    public float runSpeedmultiplier = 1.5f;
    private Vector3 direction;

    private bool isFacingLeft;
    const float groundAttackDist = 1.4f;

    private float jumpForce = 1000f;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Player");
        shenObject = animator.transform.parent.gameObject;
        body = shenObject.GetComponent<Rigidbody>();
        shenActor = shenObject.GetComponent<Shen>();

        Vector3 verticalVector = Vector3.up * jumpForce;
        body.AddForce(verticalVector);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body.constraints = RigidbodyConstraints.FreezeAll;
    }
}
