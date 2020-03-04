using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSceneObject : MonoBehaviour
{
	public string nextScene;
	public SceneTransition sceneTransition;
    // Start is called before the first frame update
    void Start()
    {
        sceneTransition = GameObject.Find("SceneManager").GetComponent(typeof(SceneTransition)) as SceneTransition;
    }

    // Update is called once per frame


    //change scenes when player reaches this object
    void OnCollisionEnter(Collision collision){
    	if(collision.gameObject.name == "Player"){
    		//change scene
    		sceneTransition.Load(nextScene);
    	}
    }


}
