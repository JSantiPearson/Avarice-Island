using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverAttacks : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firePoint;
    public GameObject feverProjectilePreFab;
    public GameObject feverLightningPreFab;

    public Animator animator;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FeverAttack"))
        {
            FireProjectile();

        }
        if (Input.GetButtonDown("FeverLightning"))
        {
            ShootLightning();
        }
    }
    void FireProjectile()
    {
        Instantiate(feverProjectilePreFab, firePoint.position, firePoint.rotation);
        //
    }
    void ShootLightning()
    {
        animator.SetTrigger("FeverLightning");
        //play attack animation
        //detect enemies
        //apply damage
    }
}
