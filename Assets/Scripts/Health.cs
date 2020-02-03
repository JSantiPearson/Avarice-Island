using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
  public int health, numOfPips;

  public Image[] pips;

  public Sprite fullPip, emptyPip;

  void Update(){

    if (health > numOfPips)
      health = numOfPips;

    for (int i = 0; i < pips.Length; i++) {

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
    }
  }
}
