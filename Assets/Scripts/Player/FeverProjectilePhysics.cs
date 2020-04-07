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

    public void updateLeft(bool isFacingLeft)
    {
        isFacingLeft = this.isFacingLeft;
    }
    void Start()
    {

            rb.velocity = new Vector3(BulletSpeed, 0, 0);


    }

    void OnTriggerEnter(Collider hitInfo)
    {
        //EnemyGrunt enemy = hitInfo.GetComponent<EnemyGrunt>();
        HanLao enemy = hitInfo.transform.parent.parent.gameObject.GetComponent<HanLao>();

        if (enemy != null)
        {
            enemy.Hurt(15);
            //Launch and Hitbox

            //enemy.TakeDamage(damage);
        }

       Debug.Log(hitInfo.name);
        Debug.Log(enemy.name);


        Destroy(gameObject);

    }

    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
