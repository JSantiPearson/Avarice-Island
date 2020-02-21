using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPoint : MonoBehaviour
{
	private GameObject player;
	private Vector3 lockLocation, playerLocation;
	private bool locked;

	private GameManager gameManager;
	private PauseGame pauseGame;

	private int enemiesLeft;
	private float distanceFromPlayer;
	public Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
    	//get dialogue if there is a dialogue component associated with this lockpoint
    	dialogue = gameObject.GetComponent(typeof(Dialogue)) as Dialogue;
    	gameManager = GameObject.Find("MyGameManager").GetComponent(typeof(GameManager)) as GameManager;
    	pauseGame = gameManager.GetComponent(typeof(PauseGame)) as PauseGame;
    	locked = false;
    	//set up position vectors

        player = GameObject.Find("Player");
        playerLocation = player.transform.position; 
        lockLocation = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    	//optimize: call this every 5 frames or so
    	enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
    	playerLocation = player.transform.position;
    	distanceFromPlayer = lockLocation.x - playerLocation.x;


    	//3 conditions:
    	//killed all enemies - unlock
    	//just reached the lock point - lock
    	//still fighting enemies - do nothing

        if(locked && enemiesLeft==0){
        	//unlock
        	gameManager.cameraFollows = true;
        	locked = false;
        	//dont want lockpoint to slow things down after it's done its job
        	Destroy(gameObject);
        } else if (!locked && distanceFromPlayer < 0.1) {
        	//lock
        	if(dialogue!=null){
        		TriggerDialogue();
        	}
        	gameManager.cameraFollows = false;
        	locked = true;
        }
    }

    void TriggerDialogue(){
    	//pauseGame.PauseForDialogue();
    	dialogue.PlayDialogue();
    	//pauseGame.UnpauseForDialogue();
    }
}
