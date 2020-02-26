using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	private Vector3 spawnLocation, playerLocation;
	private bool okToSpawn;
	private GameObject player;
	public GameObject spawnPrefab;
	public int spawnLimit = 5;
    public int numSpawned;
	private float spawnInterval=2;
    private float timeElapsed=0;
	const int spawnDistFromPlayer = 5;
    // Start is called before the first frame update
    void Start()
    {
        okToSpawn = true;
        player = GameObject.Find("Player");
        spawnLocation = gameObject.transform.position;
        playerLocation = player.transform.position; 

    }

    // Update is called once per frame
    void Update()
    {



        //spawn an enemy when player gets close to spawn point
        /*playerLocation = player.transform.position;
    	if(spawnLocation.x-playerLocation.x < spawnDistFromPlayer && okToSpawn)
        {
       		for(int i=0; i<spawnLimit; i++)
            {
       			Instantiate(spawnPrefab,spawnLocation,Quaternion.identity);
       		}
            okToSpawn = false;
        }*/
        spawnLimit = 5;
        playerLocation = player.transform.position;
        if (spawnLocation.x - playerLocation.x < spawnDistFromPlayer && okToSpawn && numSpawned < spawnLimit)
        {
            Instantiate(spawnPrefab, spawnLocation, Quaternion.identity);
            numSpawned++;
            okToSpawn = false;
        }
    }
}
