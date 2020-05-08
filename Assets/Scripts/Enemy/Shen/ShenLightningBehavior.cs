using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShenLightningBehavior : StateMachineBehaviour
{
    public GameObject shenObject;
    public Shen shenActor;
    public GameObject player;
    public Rigidbody body;
    private float lightningTimer;
    private float maxLightningTimer = 1.0f;

    public GameObject lightningProjectile;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Player");
        shenObject = animator.transform.parent.gameObject;
        body = shenObject.GetComponent<Rigidbody>();
        shenActor = shenObject.GetComponent<Shen>();

        lightningTimer = maxLightningTimer;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        lightningTimer -= Time.deltaTime;
        if(lightningTimer <= 0)
        {
            Instantiate(lightningProjectile, player.transform.position, player.transform.rotation);
            Debug.Log("did we drop the lightning?");
            lightningTimer = maxLightningTimer;
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body.constraints = RigidbodyConstraints.FreezeRotation;
        shenActor.lightningTimer = shenActor.lightningTimeLimit;
    }
}
