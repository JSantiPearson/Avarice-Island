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

	void Start(){
		//find the pauseGame component 
		pauseGame =  GameObject.Find("MyGameManager").GetComponent(typeof(PauseGame)) as PauseGame;
		dialogueBar = GameObject.Find("DialogueBar");

	}

    // Start is called before the first frame update
    public void PlayDialogue()
    {
        StartCoroutine(Type());
    }

    // Update is called once per frame
    IEnumerator Type()
    {
    	dialogueBar.SetActive(true);
    	pauseGame.PauseForDialogue();
    	foreach(char letter in sentences[index].ToCharArray()){
    		textDisplay.text += letter;
    		yield return new WaitForSeconds(speed);
    	}
    	pauseGame.UnpauseForDialogue();
    	dialogueBar.SetActive(false);    
    }
}
