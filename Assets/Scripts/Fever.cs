using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fever : MonoBehaviour
{
    public Image feverBar;
    private bool cooldown = false;

    void Start(){
      feverBar.fillAmount = 50f / 100;
    }

    // Update is called once per frame
    void Update()
    {
      if (feverBar.fillAmount == 100f / 100)
        cooldown = true;
      else if (feverBar.fillAmount == 0)
        cooldown = false;
      feverBar.fillAmount -= 0.1f / 100;
      if (Input.GetButtonDown("Fever Attack 1") && !cooldown){
        feverBar.fillAmount += 15f / 100;
      }
    }
}
