using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterAnimatorEventHandler : MonoBehaviour
{
    public GameObject fireballProjectile;
    public float fireballForce;
    public GameObject fireballSpawnPoint;

    public void activateHitBox1(int activate)
    {
        transform.parent.GetChild(2).GetComponent<Hitbox>().SetActive(activate != 0);
    }

    public void shoot()
    {
        var fireball = Instantiate(fireballProjectile, fireballSpawnPoint.transform.position, transform.rotation);
        fireball.GetComponent<Rigidbody>().AddForce(fireballSpawnPoint.transform.up * fireballForce);
    }
}
