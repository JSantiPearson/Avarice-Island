using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fever : MonoBehaviour
{
    public Image feverBar;
    public Image alert;
    private float interval = 0.05f;
    Color tempColor = new Color(1f, 0f, 0f, 0f);
    private bool cooldown = false;
    private bool fadeIn = true;
    public Animator animator;



    void Start(){
      feverBar.fillAmount = 0f;
      tempColor.a = 0f;
      alert.color = tempColor;
    }

    // Update is called once per frame
    void Update()
    {
        //if we fill the bar go into cooldown mode
      if (feverBar.fillAmount >= 99.7f / 100)
        cooldown = true;
      //if bar empty stop cooldown
      else if (feverBar.fillAmount == 0)
        cooldown = false;
      feverBar.fillAmount -= 0.075f / 100;

      //player hits fever button
      if (Input.GetKeyDown(KeyCode.E) && !cooldown){

            //projectile
            if((Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.LeftArrow)) && !cooldown)
            {
              if (feverBar.fillAmount <= 86f / 100){
                FireProjectile();
                feverBar.fillAmount += 15f / 100;
              }
            }
            //Lightning
            else if (Input.GetKey(KeyCode.UpArrow) && !cooldown)
            {
              if (feverBar.fillAmount <= 86f / 100){
                feverLightning();
                feverBar.fillAmount += 15f / 100;
              }

            }
            //AOE
            if(!(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && !(Input.GetKey(KeyCode.UpArrow)) && !cooldown)
            {
              if (feverBar.fillAmount <= 51f / 100){
                feverAOE();
                feverBar.fillAmount += 50f / 100;
              }
            }



        }

      //Fade in on UI
      if (cooldown && fadeIn){
        tempColor.a += interval;
        alert.color = tempColor;
        if (tempColor.a >= 1f){
          fadeIn = false;
        }
      }
      else if (cooldown && !fadeIn){
        tempColor.a -= interval;
        alert.color = tempColor;
        if (tempColor.a <= 0f){
          fadeIn = true;
        }
      }
      else {
        tempColor.a = 0f;
        alert.color = tempColor;
      }
    }


    //Set animation triggers for the animator 
    void FireProjectile()
    {
        animator.SetTrigger("FeverProjectile");
    }
    void feverAOE()
    {
        animator.SetTrigger("FeverAOE");
    }
    void feverLightning()
    {
        animator.SetTrigger("FeverLightning");
    }
}
