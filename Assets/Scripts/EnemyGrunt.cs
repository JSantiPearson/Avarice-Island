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

    Vector3 startingPosition = new Vector3(12.0f, 2.5f, 1.5f);


    void Start()
    {
        body.position = startingPosition;
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

    public void Wander()
    {
        var wanderBoundsX = (left: startingPosition.x - 4, right: startingPosition.x + 4);
        var wanderBoundsY = (bottom: startingPosition.y - 1.8, top: 4.4f);

        Random random = new Random();

        Vector3 currPosition = body.position;
        float targetX = random.Next(wanderBoundsX.right - wanderBoundsX.left) + wanderBoundsX.left;
        float targetZ = random.Next((wanderBoundsY.top - wanderBoundsX.bottom)*10)/10 + wanderBoundsX.left;
        float targetY = currPosition.z;

        Vector3 focalPoint = Vecotr3(targetX, targetY, targetZ);

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
