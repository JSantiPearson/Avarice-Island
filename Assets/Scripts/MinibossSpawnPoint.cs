using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinibossSpawnPoint : MonoBehaviour
{
	
	public GameObject spawnPrefab; //should be a miniboss
	public int spawnDistFromPlayer;
	public Dialogue entranceDialogue;

    public Vector3 spawnLocation, playerLocation;
    private bool spawnTriggered;
    private bool okToSpawn;
    private GameObject player;
    private GameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        spawnTriggered=false;
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("MyGameManager").GetComponent<GameManager>();
        spawnLocation = gameObject.transform.position;
        Debug.Log("Spawn location: " + gameObject.transform.position);
        playerLocation = player.transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
         //wait until player is in range to start spawning
        playerLocation = player.transform.position;
    	if(spawnLocation.x-playerLocation.x < spawnDistFromPlayer && !spawnTriggered){
        	spawnTriggered = true; //can probably forgo this system by destroying the spawn point
        	okToSpawn = true;
        }

        //start spawning enemies incrementally
        //timeElapsed+=Time.deltaTime;
        if(spawnTriggered && okToSpawn){
        	Debug.Log("about to spawn han");
        	okToSpawn = false;
        	PanToMiniboss();
         //if(okToSpawn()){
        	Instantiate(spawnPrefab,spawnLocation,Quaternion.identity);
        }
    }

    public void PanToMiniboss(){
    	return;
    }


}
