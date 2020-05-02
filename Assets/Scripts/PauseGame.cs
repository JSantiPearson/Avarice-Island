using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
	public bool gamePaused=false;
	public bool pausedWithoutMenu=false;
	public GameObject pauseMenu;
	public GameObject optionsMenu;
    public GameObject player;
    public Selectable firstSelectedPauseButton;
    public Selectable firstSelectedOptionsButton;

    // Start is called before the first frame update
    void Start()
    {
    	//find game objects we need
    	pauseMenu = GameObject.Find("PauseMenu");
    	optionsMenu = GameObject.Find("OptionsMenu");
    	pauseMenu.SetActive(false);
    	optionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.P)){
        	if(gamePaused==false){
        		Pause();
        	} else if (gamePaused==true){
        		Unpause();
        	}
        }

    }

    public void Pause(){
    	Time.timeScale=0f;
        //GameManager.enemiesOn = false;
        gamePaused=true;
        player.GetComponent<Hero>().enabled=false;
        pauseMenu.SetActive(true);
        firstSelectedPauseButton.Select();
    }

    public void Unpause(){
    	Time.timeScale=1f;
        //GameManager.enemiesOn = true;
        gamePaused=false;
        player.GetComponent<Hero>().enabled=true;
        pauseMenu.SetActive(false);
    }

    //separate pause method for dialogue handling

    public void PauseWithoutMenu(){
		//Time.timeScale=0f;
        pausedWithoutMenu=true;
        player.GetComponent<Hero>().enabled=false;
    }

    public void UnpauseWithoutMenu(){
    	//Time.timeScale=1f;
        pausedWithoutMenu=false;
        player.GetComponent<Hero>().enabled=true;
    }

    public void RevealOptions(){
    	pauseMenu.SetActive(false);
    	optionsMenu.SetActive(true);
        firstSelectedOptionsButton.Select();
    }

    public void UnRevealOptions(){
    	pauseMenu.SetActive(true);
    	optionsMenu.SetActive(false);
        firstSelectedPauseButton.Select();

    }
}
