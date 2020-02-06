using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverProjectilePhysics : MonoBehaviour
{
    public float BulletSpeed = 8f;

    public Rigidbody rb;
    bool isFacingLeft;

    public void updateLeft(bool isFacingLeft)
    {
        isFacingLeft = this.isFacingLeft;
    }
    void Start()
    {

        if (isFacingLeft)
        {
            rb.velocity = new Vector3(0, BulletSpeed * -1, 0);
        }
        else
        {
            rb.velocity = new Vector3(BulletSpeed, 0, 0);

        }

    }
}