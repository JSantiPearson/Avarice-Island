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
	public Animator goArrowAnim;

    // Start is called before the first frame update
    void Start()
    {
    	//get dialogue if there is a dialogue component associated with this lockpoint
    	dialogue = gameObject.GetComponent(typeof(Dialogue)) as Dialogue;

    	//find other gameobjects we need
    	gameManager = GameObject.Find("MyGameManager").GetComponent(typeof(GameManager)) as GameManager;
    	pauseGame = gameManager.GetComponent(typeof(PauseGame)) as PauseGame;
    	goArrowAnim = GameObject.Find("GoArrow").GetComponent(typeof(Animator)) as Animator;
    	//goArrowAnim = gameObject.GetComponent(typeof(Animator)) as Animator;

    	//set up position vectors + other bookkeeping
        locked = false;
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
        	//UNLOCK
        	gameManager.UnlockCamera();
            gameManager.SetScreenColliders(false);
        	locked = false;
        	goArrowAnim.SetTrigger("ScreenUnlocked");
        	//dont want lockpoint to slow things down after it's done its job
            Destroy(gameObject);
        	//StartCoroutine(Expire());
        } else if (!locked && distanceFromPlayer < 0.1) {

        	//LOCK
            gameManager.SetScreenColliders(true);
        	if(dialogue!=null){
        		//TriggerDialogue();
                dialogue.PlayDialogue();

        	}
            Debug.Log("about to lock camera");
        	gameManager.LockCamera();
        	locked = true;
        }
    }

    /*
    void TriggerDialogue(){
    	//pauseGame.PauseForDialogue();
    	dialogue.PlayDialogue();
    	//pauseGame.UnpauseForDialogue();
    }*/

    
    IEnumerator Expire(){
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    /**
    IEnumerator FlashArrow(){
    	float endTime = 3f;
    	float totalTime = 0f;

    	while(executionTime<endTime){
    		totalTime+=Time.deltaTime;

    		Color arrowColor = goArrow.color;
    		arrowColor.a = Mathf.PingPong(1+totalTime*2f, 1f);
    		goArrow.color = c;
    	}
    	yield return null;
    	**/
}
