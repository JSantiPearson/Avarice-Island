using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
	public bool gamePaused=false;
	public GameObject pauseMenu;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    	pauseMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.P)){
        	if(gamePaused==false){
        		Time.timeScale=0f;
        		gamePaused=true;
                player.GetComponent<Hero>().enabled=false;
        		pauseMenu.SetActive(true);
        	} else if (gamePaused==true){
        		Time.timeScale=1f;
        		gamePaused=false;
                player.GetComponent<Hero>().enabled=true;
        		pauseMenu.SetActive(false);
        	}
        }

    }
}
