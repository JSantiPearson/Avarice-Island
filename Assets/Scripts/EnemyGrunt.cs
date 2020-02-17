using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrunt : Actor
{

    public float walkSpeed = 2;
    int rightBound = 15;
    int leftBound = 7;

    bool isMoving;
    float lastWalk;
    Vector3 lastWalkVector;

    Vector3 currentDir;
    bool isFacingLeft;

    void Start()
    {
        body.position = new Vector3(12.0f, 2.5f, 1.5f);
        //want them to spawn facing left
    }

    public override void Update()
    {
        base.Update();

        float positionX = body.position.x;
        float positionY = body.position.y;

        if (positionX <= leftBound)
        {
            currentDir = Vector3.right;
            isFacingLeft = true;
            FlipSprite(isFacingLeft);
        }
        else if (positionX >= rightBound)
        {
            currentDir = Vector3.left;
            isFacingLeft = false;
            FlipSprite(isFacingLeft);
        }
        Walk();

    }

    public void Stop()
    {
        speed = 0;
        isMoving = false;
        baseAnim.SetFloat("Speed", speed);
    }

    public void Walk()
    {
        isMoving = true;
        speed = walkSpeed;
        baseAnim.SetFloat("Speed", speed);
    }


    void FixedUpdate()
    {
        Vector3 moveVector = currentDir * speed;
        body.MovePosition(transform.position + moveVector * Time.fixedDeltaTime);
        baseAnim.SetFloat("Speed", moveVector.magnitude);

        if (moveVector != Vector3.zero)
        {
            if (moveVector.x != 0)
            {
                isFacingLeft = moveVector.x < 0;
            }
            FlipSprite(isFacingLeft);
        }
    }
}
