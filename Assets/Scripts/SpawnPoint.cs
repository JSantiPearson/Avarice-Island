using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

	public GameObject spawnPrefab;
	public int spawnLimit;
	public float spawnInterval;
	public int spawnDistFromPlayer;
  
  private float timeElapsed;
  private int numSpawned;
  private Vector3 spawnLocation, playerLocation;
  private bool spawnTriggered;
  private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        spawnTriggered=false;
        timeElapsed = 0;
        numSpawned=0;
        player = GameObject.Find("Player");
        spawnLocation = gameObject.transform.position;
        playerLocation = player.transform.position; 

    }

    // Update is called once per frame
    void Update()
    {


       //wait until player is in range to start spawning
        playerLocation = player.transform.position;
    	if(spawnLocation.x-playerLocation.x < spawnDistFromPlayer && spawnTriggered == false){
        	spawnTriggered = true;
        	timeElapsed = spawnInterval; //enemy will spawn immediately after player enters range
        }

        //start spawning enemies incrementally
        timeElapsed+=Time.deltaTime;
        if(spawnTriggered && okToSpawn()){
         //if(okToSpawn()){
        	Debug.Log("About to spawn for" + gameObject.name);
        	Instantiate(spawnPrefab,spawnLocation,Quaternion.identity);
        }

    }

    public bool okToSpawn(){
    	if(numSpawned == spawnLimit){
    		return false;
    	}

   		if(timeElapsed>spawnInterval){
      		timeElapsed=0;
      		numSpawned++;
      		return true;
    	}
      	
      	return false;
  	}

}
