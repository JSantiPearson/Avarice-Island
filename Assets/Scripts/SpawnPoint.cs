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
  private GameObject[] spawnedEnemies;

  public Vector3 spawnLocation, playerLocation;
  public bool spawnTriggered;
  private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        spawnTriggered=false;
        timeElapsed = 0;
        numSpawned=0;
        spawnedEnemies = new GameObject[spawnLimit];
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
        if(spawnTriggered && OkToSpawn()){
         //if(okToSpawn()){
        	spawnedEnemies[numSpawned] = Instantiate(spawnPrefab,spawnLocation,spawnPrefab.transform.rotation);
          numSpawned++;
        }

    }

    public bool OkToSpawn(){
    	if(numSpawned == spawnLimit){
    		return false;
    	}

   		if(timeElapsed>spawnInterval && AreaClear()){
      		timeElapsed=0;
      		return true;
    	}
      	
      	return false;
  	}

    public bool AreaClear(){

      //check if any enemies are standing in spawn area
        foreach(GameObject enemy in spawnedEnemies){
          if(enemy!=null && Actor.IsCloseTo(enemy.transform.position,spawnLocation,1f)){ //cant check array length properly, needed null check
            return false;
          }
        }

      //check is player is standing in spawn area
      return !Actor.IsCloseTo(playerLocation,spawnLocation,1f);
    }

}
