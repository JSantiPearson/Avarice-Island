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
    public Transform firePoint;
    public GameObject feverProjectilePreFab;

    void Start(){
      feverBar.fillAmount = 50f / 100;
      tempColor.a = 0f;
      alert.color = tempColor;
    }

    // Update is called once per frame
    void Update()
    {
      if (feverBar.fillAmount == 100f / 100)
        cooldown = true;
      else if (feverBar.fillAmount == 0)
        cooldown = false;
      feverBar.fillAmount -= 0.1f / 100;
      if (Input.GetButtonDown("FeverAttack") && !cooldown){
            FireProjectile();
            feverBar.fillAmount += 15f / 100;
      }

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

    void FireProjectile()
    {
        Instantiate(feverProjectilePreFab, firePoint.position, firePoint.rotation);
    }
}
