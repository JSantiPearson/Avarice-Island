using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverProjectilePhysics : MonoBehaviour
{
    public float BulletSpeed = 8f;
    public Collider collider;
    public Rigidbody rb;
    bool isFacingLeft;
    public int damage = 15;
    public float dieTime;

    void Start()
    {


    }
    void update()
    {


        //if projectile is out longer than it takes to go from the two ends of the map destroy it
        if (Time.time >= dieTime)  
        {
            Destroy(gameObject);
        }

    }


    //When the projectile collides with an object
    void OnCollisionEnter(Collision col)
    {
        //get information
        Collider hitInfo = col.collider;

        //if camera bounds destroy it so it cant hit enemys further down the level
        if (hitInfo.tag == "camBounds")
        {
            Destroy(gameObject);

        }
        else if (hitInfo.tag == "Enemy")
        {

            //get baddie information
            Enemy baddie = hitInfo.gameObject.GetComponent<Enemy>();

            if (baddie != null)
            {
                //deal damage and projectile has no knockback
                baddie.Hurt(damage, false);
            }
            //destroy object as we have hit something
            Destroy(gameObject);
        }

    }
}
