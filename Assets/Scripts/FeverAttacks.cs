using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverAttacks : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firePoint;
    public GameObject feverProjectilePreFab;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FeverAttack"))
        {
            FireProjectile();

        }
    }
    void FireProjectile()
    {
        Instantiate(feverProjectilePreFab, firePoint.position, firePoint.rotation);
        //
    }
}