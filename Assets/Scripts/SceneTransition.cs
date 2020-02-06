using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

  public Animator transitionAnimation;
	public string sceneName;


    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Tab) && sceneName!=null){
       	StartCoroutine(LoadScene());
       } 
    }

    IEnumerator LoadScene(){
      transitionAnimation.SetTrigger("end");
      yield return new WaitForSeconds(1f);
      SceneManager.LoadScene(sceneName);
    }
}
