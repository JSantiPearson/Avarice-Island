using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

  	public Animator transitionAnimation; //need this when we implement fades
	//public string sceneName;


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


    IEnumerator LoadSceneCo(string sceneName){
      //check for game pause
    	if(Time.timeScale==0){
			Time.timeScale=1;
		}
		//trigger a fadeout
		transitionAnimation.SetTrigger("fadeout");
      	yield return new WaitForSeconds(0.8f);
      	SceneManager.LoadScene(sceneName);
    }
}
