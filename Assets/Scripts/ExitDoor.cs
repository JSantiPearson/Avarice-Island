using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
	public Animator doorAnim;
	public GameObject player;
	public Transform playerTrans;

	private Vector3 doorPos;
	private Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.Find("Player");
       playerTrans = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
    	playerPos =  playerTrans.position;
    	doorPos = gameObject.transform.position;
    	//Debug.Log("Door x: " + doorPos.x + "\n Player x: " + playerPos.x );
        if(System.Math.Abs(playerPos.x-doorPos.x)<1.5){
        	doorAnim.SetBool("Nearby", true);
        }
				else {
					doorAnim.SetBool("Nearby", false);
				}
    }
}
