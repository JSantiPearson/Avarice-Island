using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

  	public Animator transitionAnimation; 
    private static SceneTransition instance;
	//public string sceneName;

    void Awake() {
    //Hero Script Persists across scenes
        //startingCoords = this.transform.position;
        //SceneManager.sceneLoaded += OnSceneLoaded;

        if (instance != null)
        {
            //instance.gameObject.GetComponent<Rigidbody>().MovePosition(instance.startingCoords); //move the other object to the right place?
            //instance.ResetCoords();
            //Destroy(gameObject);
            //assign health vaalues to new object
           // this.currentHealth = instance.currentHealth;
            //this.currentLives = instance.currentLives;
            //kill old object
            //GameObject oldObject = instance.gameObject;
            Destroy(gameObject);
            //instance = this;
           //DontDestroyOnLoad(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
    //This will load a scene on keypress.
    //   if(Input.GetKeyDown(KeyCode.Tab) && sceneName!=null){
    //   	StartCoroutine(LoadScene());
    //   } 
    }

    public void Load(string sceneName){
    	StartCoroutine(LoadSceneCo(sceneName));
    }

    public void ReLoad(){
      StartCoroutine(LoadSceneCo(SceneManager.GetActiveScene().name));
    }


    IEnumerator LoadSceneCo(string sceneName){
      //check for game pause
    	if(Time.timeScale==0){
			Time.timeScale=1;
		}
		//trigger a fadeout
		transitionAnimation.SetTrigger("fadeout");
    yield return new WaitForSeconds(0.8f);
    transitionAnimation.ResetTrigger("fadeout");
    SceneManager.LoadScene(sceneName);
    }
}
