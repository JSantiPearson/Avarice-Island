using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSceneObject : MonoBehaviour
{
	public string nextScene;
	public SceneTransition sceneTransition;
    public GameObject player;
    private int enemiesLeft;

    // Start is called before the first frame update
    void Start()
    {
        sceneTransition = GameObject.Find("SceneManager").GetComponent(typeof(SceneTransition)) as SceneTransition;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update(){
        enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }


    //change scenes when player reaches this object
    void OnCollisionEnter(Collision collision){
    	if(collision.gameObject.name == "Player" && enemiesLeft<=0){
            //reset player for next scene
            //player.GetComponent<Hero>().ResetCoords();
    		//change scenes
    		sceneTransition.Load(nextScene);
    	}
    }


}
