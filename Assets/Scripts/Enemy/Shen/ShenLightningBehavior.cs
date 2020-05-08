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

    float cameraHalfWidth;
    Vector3 camera;
    public float leftCamBound;
    public float rightCamBound;

    public GameObject lightningProjectile;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Player");
        shenObject = animator.transform.parent.gameObject;
        body = shenObject.GetComponent<Rigidbody>();
        shenActor = shenObject.GetComponent<Shen>();

        cameraHalfWidth = Camera.main.GetComponent<CameraBounds>().cameraHalfWidth;

        lightningTimer = maxLightningTimer;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        lightningTimer -= Time.deltaTime;
        camera = Camera.main.transform.position;
        leftCamBound = camera.x - cameraHalfWidth;
        rightCamBound = camera.x + cameraHalfWidth;

        if(lightningTimer <= 0)
        {
            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;
            float playerZ = player.transform.position.z;

            // if player is running and is not too close to the left collider, spawn the lightning in front of them.
            if (player.GetComponent<Hero>().isFacingLeft && player.GetComponent<Hero>().speed > 0 && (playerX-3) < leftCamBound){ //If player
              Vector3 lightningPos = new Vector3((playerX - 3), playerY, playerZ);
              Instantiate(lightningProjectile, lightningPos, player.transform.rotation);
            }
            else if (!player.GetComponent<Hero>().isFacingLeft && player.GetComponent<Hero>().speed > 0 && (playerX+3) > rightCamBound){
              Vector3 lightningPos = new Vector3((playerX + 3), playerY, playerZ);
              Instantiate(lightningProjectile, lightningPos, player.transform.rotation);
            }
            // if the player is not moving or is running against a wall, spawn the lightning on top of them.
            else {
              Instantiate(lightningProjectile, player.transform.position, player.transform.rotation);
            }
            lightningTimer = maxLightningTimer;
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body.constraints = RigidbodyConstraints.FreezeRotation;
        shenActor.lightningTimer = shenActor.lightningTimeLimit;
    }
}
