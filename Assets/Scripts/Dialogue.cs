using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

	public TextMeshProUGUI textDisplay;
	public string[] sentences;
	public int index; //which sentence to type
	public float speed;
	public PauseGame pauseGame;
	public GameObject dialogueBar;
	private bool pausedForDialogue;

	void Start(){
		//find the pauseGame component
		pausedForDialogue = false; 
		pauseGame =  GameObject.Find("MyGameManager").GetComponent(typeof(PauseGame)) as PauseGame;
		dialogueBar = GameObject.Find("DialogueBar");
        textDisplay = GameObject.Find("DialogueText").GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;

	}

	void Update(){
		//keep track of whether or not we are paused for dialogue, allow player to continue
		if(pausedForDialogue && Input.GetKeyDown(KeyCode.Z)){
			pauseGame.UnpauseForDialogue();
			pausedForDialogue = false;
			dialogueBar.SetActive(false);    

		}

	}

    public void PlayDialogue()
    {
    	//TEMPORARY SOLUTION FOR DIALOGUE HANDLING. Only works for 1 line
    	dialogueBar.SetActive(true);
    	pauseGame.PauseForDialogue();
    	textDisplay.text = sentences[index];
    	pausedForDialogue = true;
    	//CONTINUE HAPPENS IN UPDATE METHOD

    }

    //UNUSED at the moment
    IEnumerator Type()
    {
    	dialogueBar.SetActive(true);
    	pauseGame.PauseForDialogue();
    	/*
    	foreach(char letter in sentences[index].ToCharArray()){
    		textDisplay.text += letter;

    	}
    	*/
    	textDisplay.text = sentences[index];
    	yield return new WaitForSeconds(speed);
    	//Debug.Log("yielded...");
    	pauseGame.UnpauseForDialogue();
    	dialogueBar.SetActive(false);    
    }
}
