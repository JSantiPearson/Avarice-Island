using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitProjectilePhysics : MonoBehaviour
{
    public float Speed = 10f;
    //public Collider myCollider;
    public Animator baseAnim; //deals with animations
    //public Rigidbody rb;
    public int damage;
    bool onGround;

    public float size = 1.0f;
    protected Vector3 frontVector; //for determining direction the actor is facing

    void Start()
    {
        baseAnim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider hitInfo)
    {
        Debug.Log("spit hit" + hitInfo.name);
        //if (hitInfo.GetComponent<Collider>().tag == "Floor")
        if (hitInfo.tag == "Floor")
        {
            onGround = true;
            baseAnim.SetBool("onGround", onGround);
            //myCollider.SetActive(false);
            //rb.setActive(false);
            Destroy(gameObject);
        }
        else if (hitInfo.tag == "Player")
        {

            //EnemyGrunt enemy = hitInfo.GetComponent<EnemyGrunt>();
            Hero player = hitInfo.transform.parent.parent.gameObject.GetComponent<Hero>();

            if (player != null)
            {
                player.hurt(damage);
                //Launch and Hitbox

                //enemy.TakeDamage(damage);
            }

            Debug.Log("spit hit" + player.name);
            Destroy(gameObject);
        }
        //myCollider.SetActive(false);
        //rb.setActive(false);
    }

    void OnCollisionEnter()
    {

    }

    /**
 * Flips the sprite horizontally for when the actor changes direction.
 **/
    public void FlipSprite(bool isFacingLeft)
    {

        if (isFacingLeft)
        {

            //   transform.Rotate(0f, 180f, 0f);
            frontVector = new Vector3(-1, 0, 0);
            transform.localScale = new Vector3(-size, size, 1);
        }
        else
        {
            //transform.Rotate(0f, 180f, 0);
            frontVector = new Vector3(1, 0, 0);
            transform.localScale = new Vector3(size, size, 1);
        }
    }
}