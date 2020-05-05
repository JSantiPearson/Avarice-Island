using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectilePhysics : MonoBehaviour
{
    public float Speed = 10f;
    //public Collider myCollider;
    public Animator baseAnim; //deals with animations
    public Rigidbody body;
    public int damage;
    bool onGround;
    public float dieTime;

    public float size = 1.0f;
    protected Vector3 frontVector; //for determining direction the actor is facing

    private bool fireballPaused;


    void Start()
    {
        fireballPaused = false;
        baseAnim = gameObject.GetComponent<Animator>();
        body = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //destroy if paused, FOR NOW
        if(!GameManager.enemiesOn){
            fireballPaused = true;
            Destroy(gameObject);
        }

        /*
        if(GameManager.enemiesOn && fireballPaused){
            fireballPaused = false;
            body.WakeUp();
        }*/

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
        if (hitInfo.tag == "Floor")
        {
            onGround = true;
            baseAnim.SetBool("onGround", onGround);
            //myCollider.SetActive(false);
            //rb.setActive(false);
        }
        else if (hitInfo.tag == "Player")
        {

            //EnemyGrunt enemy = hitInfo.GetComponent<EnemyGrunt>();
            Hero player = hitInfo.gameObject.GetComponent<Hero>();

            if (player != null)
            {
                player.Hurt(damage);
                //Launch and Hitbox

                //enemy.TakeDamage(damage);
            }
        }
        //myCollider.SetActive(false);
        //rb.setActive(false);
        baseAnim.SetTrigger("Hit");
        //Destroy(gameObject);
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
