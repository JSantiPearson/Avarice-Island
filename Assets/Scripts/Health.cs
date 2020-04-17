using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
  public int health, numOfPips;

  public GameObject[] fullPips;

  public Sprite fullPip, emptyPip;

  public GameObject fullPipPrefab, emptyPipPrefab;
  public GameObject healthBar, emptyHealthSet, fullHealthSet;

  void Start(){

      //find health bar objects
      healthBar = GameObject.Find("HealthBar2"); //RENAME WHEN HEALTH BAR NAME CHANGES
      fullHealthSet = healthBar.transform.GetChild(1).gameObject; //first child
      emptyHealthSet = healthBar.transform.GetChild(0).gameObject; //second child
      fullPips = new GameObject[numOfPips];

    for(int i=0;i<numOfPips;i++){
      //fill empty slot
      Instantiate(emptyPipPrefab,emptyHealthSet.transform);
      //fill full slot
      GameObject currPip = Instantiate(fullPipPrefab,fullHealthSet.transform);
      fullPips[i] = currPip;
    }
  }

  void Update(){
     //OLD VERSION, INTERACTS WITH ORIGINAL HEALTH BAR
    
    if (health > numOfPips){
      health = numOfPips;
    }

    


    /*for (int i = 0; i < pips.Length; i++) {

      if (i < health){
        pips[i].sprite = fullPip;
      }
      else {
        pips[i].sprite = emptyPip;
      }

      if (i < numOfPips) {
        pips[i].enabled = true;
      }
      else {
        pips[i].enabled = false;
      }
    }*/
    


  }
}
