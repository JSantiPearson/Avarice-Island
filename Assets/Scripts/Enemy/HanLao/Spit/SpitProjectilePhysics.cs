using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitProjectilePhysics : MonoBehaviour
{
    //public Collider myCollider;
    public Animator baseAnim; //deals with animations
    //public Rigidbody rb;
    public int damage;
    bool onGround;
    private float hitResetTimerMax = 1.5f;
    private float hitResetTimer;


    public float size = 1.0f;
    protected Vector3 frontVector; //for determining direction the actor is facing


    bool m_Started;
    public LayerMask m_LayerMask;
    public HashSet<GameObject> beenHit = new HashSet<GameObject>();

    void Start()
    {
        baseAnim = gameObject.GetComponent<Animator>();
        hitResetTimer = hitResetTimerMax;
    }

    void Update()
    {
        MyCollisions();

        hitResetTimer = hitResetTimer - Time.deltaTime;
        if(hitResetTimer <= 0)
        {
            beenHit.Clear();
            hitResetTimer = hitResetTimerMax;
        }
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
                enemy.GetComponent<Hero>().Hurt(damage);
                beenHit.Add(enemy);
            }
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