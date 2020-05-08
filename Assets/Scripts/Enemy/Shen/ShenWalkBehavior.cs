using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShenWalkBehavior : StateMachineBehaviour
{
    public GameObject shenObject;
    public Actor shenActor;
    public GameObject player;
    public Rigidbody body;
    
    public float walkSpeedMultiplier;
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

        Vector3 playerPos = player.transform.position;
        Vector3 moveVector = playerPos - body.position;
        moveVector.Normalize();

        shenActor.FlipSprite(moveVector.x < 0);

        if (Actor.IsCloseTo(body.position, playerPos.position, groundAttackDist))
        {
            animator.SetFloat("Soeed", 0.0f);
        }

        body.MovePosition(body.position + moveVector * Time.deltaTime);


    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
