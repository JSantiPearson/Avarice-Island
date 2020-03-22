using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingDoor : MonoBehaviour
{

	public Animator doorAnim;
	public SpawnPoint spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnPoint.spawnTriggered){
        	doorAnim.SetTrigger("Spawn");
        	Destroy(gameObject);
        }
    }
}
