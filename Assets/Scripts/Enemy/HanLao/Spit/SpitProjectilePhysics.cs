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
        MyCollisions();
    }

    void MyCollisions()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask);
        //Collider[] hitEnemies = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, m_layerMask);
        int i = 0;
        //Check when there is a new collider coming into contact with the box
        foreach (Collider collider in hitColliders)
        {
            GameObject enemy = collider.gameObject;
            if (!beenHit.Contains(enemy))
            {
                if (type == HitboxType.light)
                {
                    enemy.GetComponent<Hero>().Hurt(damage);
                    beenHit.Add(enemy);
                }
                else if (type == HitboxType.flash)
                {
                    enemy.GetComponent<Hero>().Stunned();
                    beenHit.Add(enemy);
                }
                else if (type == HitboxType.heavy)
                { //TODO: Add i-frames while player is getting up?
                    enemy.GetComponent<Hero>().Launch(damage);
                    beenHit.Add(enemy);
                }
                else if (type == HitboxType.lightning_light)
                { //TODO: Add i-frames while player is getting up?
                    enemy.GetComponent<Hero>().Zap(damage);
                    beenHit.Add(enemy);
                }
                else if (type == HitboxType.lightning_heavy)
                { //TODO: Add i-frames while player is getting up?
                    enemy.GetComponent<Hero>().Zap(damage);
                    enemy.GetComponent<Hero>().Launch(0);
                    beenHit.Add(enemy);
                }
            }
        }
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        else if (hitInfo.tag == "Player")
        {

            //EnemyGrunt enemy = hitInfo.GetComponent<EnemyGrunt>();
            Hero player = hitInfo.transform.parent.parent.gameObject.GetComponent<Hero>();

            if (player != null)
            {
                player.Hurt(damage);
                //Launch and Hitbox

                //enemy.TakeDamage(damage);
            }

            Debug.Log("spit hit" + player.name);
            Destroy(gameObject);
        }
        //myCollider.SetActive(false);
        //rb.setActive(false);
    }

    void OnCollisionEnter(Collision hitInfo)
    {
        if (hitInfo.collider.tag == "Floor")
        {
            onGround = true;
            baseAnim.SetBool("onGround", onGround);
            //myCollider.SetActive(false);
            //rb.setActive(false);
            Destroy(gameObject);
        }
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