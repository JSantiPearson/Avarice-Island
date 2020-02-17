using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	private Transform spawnLocation, playerLocation;
	private bool okToSpawn;
	private GameObject player;
	public GameObject spawnPrefab;
	const int spawnPadding = 5;
    // Start is called before the first frame update
    void Start()
    {
        okToSpawn = true;
        player = GameObject.Find("Player");
        spawnLocation = gameObject.transform;
        playerLocation = player.transform; 

    }

    // Update is called once per frame
    void Update()
    {
    	//spawn an enemy when player gets close to spawn point
       playerLocation = player.transform;
       if(spawnLocation.position.x-playerLocation.position.x < spawnPadding && okToSpawn){
       	Instantiate(spawnPrefab,spawnLocation.position,Quaternion.identity);
       	okToSpawn = false;
       }
    }
}
