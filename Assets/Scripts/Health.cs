using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
  public int numOfPips, prevHealth; //pip behavior will be automatically scaled
  public int health; 
  public int lives, maxLives;
  public bool regain;

  public GameObject[] fullPips,fullLives;

  //public Sprite fullPip, emptyPip;

  public GameObject fullPipPrefab, emptyPipPrefab, fullHeartPrefab;
  public GameObject healthBar, emptyHealthSet, fullHealthSet, livesBar, fullHeartSet;

  public GameObject playerObject;
  public Hero player;

  //reusable anim variable used in update 
  private Animator currPipAnimator;

  //private static Health instance = null;
    /*
  void Awake()
      {
    //Health Persists across scenes

    if (instance != null)
    {
      Destroy(gameObject);
    }
    else
    {
      instance = this;
      //DontDestroyOnLoad(gameObject);
    }
  }*/


  void Start(){

      //player references
      playerObject = GameObject.Find("Player");
      player = playerObject.GetComponent<Hero>();


      //find health bar objects
      healthBar = GameObject.Find("HealthBar");
      livesBar = GameObject.Find("LivesBar");
      //grids for health bar
      fullHealthSet = healthBar.transform.GetChild(1).gameObject; 
      emptyHealthSet = healthBar.transform.GetChild(0).gameObject;
      //grids for lives bar
      fullHeartSet = livesBar.transform.GetChild(1).gameObject;

      //health based on hero script health
      health = (int)Hero.maxHealth;
      prevHealth = (int)Hero.maxHealth+1; //need to make sure health<prevHealth at start
      maxLives = (int)Hero.maxLives;
      lives = maxLives;

      fullPips = new GameObject[numOfPips];
      fullLives = new GameObject[maxLives];



    //FILL THE HEALTH BAR 
    for(int i=numOfPips-1;i>=0;i--){
      //fill empty slot
      Instantiate(emptyPipPrefab,emptyHealthSet.transform);
      //fill full slot
      GameObject currPip = Instantiate(fullPipPrefab,fullHealthSet.transform);
      fullPips[i] = currPip;
    }

    Debug.Log("max lives: " + maxLives);
    for(int i=maxLives-1;i>=0;i--){
      //fill empty slot
      //Instantiate(__________,emptyHealthSet.transform);
      //fill full slot
      GameObject currLife = Instantiate(fullHeartPrefab,fullHeartSet.transform);
      fullLives[i] = currLife;
    }


  }

  void Update(){
    
    health = (int)player.currentHealth;
    int numPipsActive = (int) Mathf.Ceil(health / (Hero.maxHealth / numOfPips)); //(player.currentHealth/Hero.maxHealth)*numOfPips;

    //should trigger a regain when player loses a life
    if(health>prevHealth){
      regain = true;
    }

    //remove and/or fill pips based on health///////////////////////////////////////////////////
    for(int i=0; i<numOfPips;i++){

      currPipAnimator = fullPips[i].GetComponent<Animator>(); 

      if(i<numPipsActive){
        if(regain){
          currPipAnimator.ResetTrigger("Flash");
         currPipAnimator.SetTrigger("Regain"); 
        }
        //currPipAnimator.SetTrigger("Regain"); //in case we implement regaining life points
      } else {
        currPipAnimator.SetTrigger("Flash"); //disables pip after flash
      }
    }  

    if(regain){
      regain = false;
    }

    //remove lives /////////////////////////////////////////////////////////////////////
    int numLivesActive = (int)player.currentLives;

    for(int i=0; i<maxLives; i++){
      if(i<numLivesActive){
        fullLives[i].SetActive(true);
      } else {
        fullLives[i].SetActive(false);
      }
    }


    prevHealth = health;



  }
}
