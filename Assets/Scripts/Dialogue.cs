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
    public Animator dialogueAnim;
    private const float cursorInterval=0.5f;
    private float timeElapsed;
	public bool pausedForDialogue;
    private bool cursorOn;
    private char[] cursorChars = {'(','z',')'};
    private bool indexUpdated;

	void Start(){
		//find the pauseGame component
        index = 0;
        indexUpdated = true; //true so first sentence will load
        cursorOn = false; //used for flashing cursor in dialogue bar.
		pausedForDialogue = false;
        timeElapsed=0; 

        //fill in dialogue-related objects
		pauseGame =  GameObject.Find("MyGameManager").GetComponent(typeof(PauseGame)) as PauseGame;
		dialogueBar = GameObject.Find("DialogueBar");
        dialogueAnim = dialogueBar.GetComponent<Animator>();
        textDisplay = dialogueBar.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //textDisplay = GameObject.Find("DialogueText").GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;

	}

	void Update(){
		//keep track of whether or not we are paused for dialogue, allow player to continue
        timeElapsed+=Time.fixedDeltaTime;
		if(pausedForDialogue){

            //displays a fresh sentence only once
            if(indexUpdated){
                PlaySentence(index);
                indexUpdated=false;
            }

            flashCursor();
			if(Input.GetKeyDown(KeyCode.Z)){
                //pauseGame.UnpauseWithoutMenu();
                //PlaySentence(index);
                index++;
                indexUpdated=true;
			    //pausedForDialogue = false;
			    //dialogueBar.SetActive(false);
            }    
		    if(index>=sentences.Length){
                pausedForDialogue = false;
                //dialogueBar.SetActive(false);
                dialogueAnim.SetTrigger("hide");
                pauseGame.UnpauseWithoutMenu();
            }
        }
	}

    public void PlayDialogue()
    {
    	//dialogueBar.SetActive(true);
        dialogueAnim.SetTrigger("popup");
    	//pauseGame.PauseWithoutMenu();
        StartCoroutine(WaitAndPause(0.5f));
    	pausedForDialogue = true;
        //PlaySentence(0); //start first sentence
    	//CONTINUE HAPPENS IN UPDATE METHOD

    }

    public void PlaySentence(int index){
        textDisplay.text = sentences[index];
    }

    public void flashCursor(){

        string cursorString = new string(cursorChars); 

        if(timeElapsed>=cursorInterval){
            //flash either on or off
            if(cursorOn){
                textDisplay.text+=cursorString;
                cursorOn = false;
            } else {
                textDisplay.text = (textDisplay.text).TrimEnd(cursorChars);
                cursorOn = true;
            }
            //reset
            timeElapsed = 0;
        }
        Debug.Log("timeElapsed wasn't sufficient");
    }

    //UNUSED at the moment
    IEnumerator Type()
    {
    	//dialogueBar.SetActive(true);
        dialogueAnim.SetTrigger("popup");
    	pauseGame.PauseWithoutMenu();
    	/*
    	foreach(char letter in sentences[index].ToCharArray()){
    		textDisplay.text += letter;

    	}
    	*/
    	textDisplay.text = sentences[index];
    	yield return new WaitForSeconds(speed);
    	//Debug.Log("yielded...");
    	pauseGame.UnpauseWithoutMenu();
    	//dialogueBar.SetActive(false); 
        dialogueAnim.SetTrigger("hide");   
    }

    IEnumerator WaitAndPause(float time){
        yield return new WaitForSeconds(time);
        dialogueAnim.ResetTrigger("popup"); //fix for bug after miniboss death
        pauseGame.PauseWithoutMenu();
    }
}
