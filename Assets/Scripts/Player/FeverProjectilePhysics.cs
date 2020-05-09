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
    public void updateLeft(bool isFacingLeft)
    {
        isFacingLeft = this.isFacingLeft;
    }
    void Start()
    {

         //   rb.velocity = new Vector3(BulletSpeed, 0, 0);


    }
    void update()
    {



        if (Time.time >= dieTime)
        {

            //baseAnim.SetTrigger("Hit");
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter(Collider hitInfo)
    {


    }

    void OnCollisionEnter(Collision col)
    {

        Collider hitInfo = col.collider;
        //if (hitInfo.GetComponent<Collider>().tag == "Floor")


        Debug.Log("ElectroWave hit: " + hitInfo.tag);

        if (hitInfo.tag == "camBounds")
        {
            Destroy(gameObject);

        }
        else if (hitInfo.tag == "Enemy")
        {

            //EnemyGrunt enemy = hitInfo.GetComponent<EnemyGrunt>();
            Enemy baddie = hitInfo.gameObject.GetComponent<Enemy>();

            if (baddie != null)
            {
                baddie.Hurt(damage, false);
                //Launch and Hitbox

                //enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }

    }
}
