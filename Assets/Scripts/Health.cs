using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
  public int health, numOfPips; //pip behavior will be automatically scaled

  public GameObject[] fullPips;

  public Sprite fullPip, emptyPip;

  public GameObject fullPipPrefab, emptyPipPrefab;
  public GameObject healthBar, emptyHealthSet, fullHealthSet;

  public GameObject playerObject;
  public Hero player;

  //reusable anim variable used in update 
  private Animator currPipAnimator;


  void Start(){

      //player references
      playerObject = GameObject.Find("Player");
      player = playerObject.GetComponent<Hero>();


      //find health bar objects
      healthBar = GameObject.Find("HealthBar"); //RENAME WHEN HEALTH BAR NAME CHANGES
      fullHealthSet = healthBar.transform.GetChild(1).gameObject; //first child
      emptyHealthSet = healthBar.transform.GetChild(0).gameObject; //second child
      fullPips = new GameObject[numOfPips];

      //health based on hero script health
      health = (int)Hero.maxHealth; //cast shouldn't matter, should probably be const int in Hero's definition anyway

    //FILL THE BAR 
    for(int i=numOfPips-1;i>=0;i--){
      //fill empty slot
      Instantiate(emptyPipPrefab,emptyHealthSet.transform);
      //fill full slot
      GameObject currPip = Instantiate(fullPipPrefab,fullHealthSet.transform);
      fullPips[i] = currPip;
    }
  }

  void Update(){
    

    int numPipsActive = (int) Mathf.Ceil(player.currentHealth / (Hero.maxHealth / numOfPips)); //(player.currentHealth/Hero.maxHealth)*numOfPips;


    for(int i=0; i<numOfPips;i++){

      currPipAnimator = fullPips[i].GetComponent<Animator>(); 

      if(i<numPipsActive){
        //currPipAnimator.SetTrigger("Regain"); //in case we implement regaining life points
      } else {
        currPipAnimator.SetTrigger("Flash"); //disables pip after flash
      }
    }    


  }
}
