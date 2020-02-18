using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrunt : Actor
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// CLASS VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public float currentHealth;
    public float walkSpeed = 2;
    int rightBound = 15;
    int leftBound = 7;

    bool isAttacking;
    bool isHurt;

    bool isMoving;
    float lastWalk;
    Vector3 lastWalkVector;

    Vector3 currentDir;
    bool isFacingLeft;

    Vector3 startingPosition = new Vector3(12.0f, 2.475173f, 1.0f);
    Vector3 targetPosition = new Vector3(12.0f, 2.5f, 1.0f);

    float timeOfLastWander;
    float WanderWaitTime = 5;

    float noticeDistance = 2;
    float attackDistance = 1;
    int fleeHealth;

    //game difficulty
    public enum DifficultyLevel
    {
        easy,
        medium,
        hard
    }

    public enum EnemyState
    {
        //noticed state? alternatively, "if state not approaching and approachable" case in state logic of update
        idle,
        pacing,
        approaching,
        attacking,
        fleeing,
        wandering
    }

    public DifficultyLevel? currentLevel = null;
    public EnemyState currentState;

    public GameObject playerReference;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        body.position = startingPosition;
        targetPosition = startingPosition;
        playerReference = GameObject.Find("Player");
        currentState = EnemyState.idle;
        currentHealth = 100;
        fleeHealth = 30;
    }

    public override void Update()
    {
        base.Update();

        Vector3 playerPosition = playerReference.transform.position;
        float currentDistance = Vector3.Distance(body.position, playerPosition);

        //might have to rework this to flee to a determined point rather than a standing distance
        if (currentDistance <= 3 && (currentHealth <= fleeHealth || currentState == EnemyState.fleeing))
        {
            if (currentState != EnemyState.fleeing)
            {
                switch (currentLevel)
                {
                    case DifficultyLevel.easy:
                        fleeHealth -= 30;
                        break;
                    case DifficultyLevel.medium:
                        fleeHealth -= 15;
                        break;
                    case DifficultyLevel.hard:
                        fleeHealth -= 10;
                        break;
                    default:
                        fleeHealth -= 30;
                        break;
                }
            }
            currentState = EnemyState.fleeing;
        }
        else if (currentDistance <= attackDistance) currentState = EnemyState.attacking;
        else if (currentDistance <= noticeDistance) currentState = EnemyState.approaching;
        else currentState = EnemyState.idle;

        switch (currentState)
        {
            case EnemyState.idle:
                Idle();
                break;
            case EnemyState.pacing:
                Pace();
                break;
            case EnemyState.approaching:
                Approach();
                break;
            case EnemyState.attacking:
                Attack();
                break;
            case EnemyState.fleeing:
                Flee();
                break;
            case EnemyState.wandering:
                Wander();
                break;
            default:
                break;
        }

    }

    public void Idle()
    {
        //Idle anim? leg up on wall/kicking dirt/hand in pocket/smoking?
        Stop();
    }

    public void Pace()
    {
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

    public void Approach()
    {
        Vector3 playerPosition = playerReference.transform.position;
        currentDir = playerPosition - body.position;
        currentDir.Normalize();
        Walk();
    }

    public void Attack()
    {
        Stop();
        float attackChance;

        switch (currentLevel)
        {
            case DifficultyLevel.easy:
                attackChance = .03f;
                break;
            case DifficultyLevel.medium:
                attackChance = .8f;
                break;
            case DifficultyLevel.hard:
                attackChance = .2f;
                break;
            default:
                attackChance = .03f;
                break;
        }

        if (Random.value <= attackChance)
        {
            Punch();
        }
    }

    //might have to rework this to flee to a determined point rather than a standing distance
    public void Flee()
    {
        Vector3 playerPosition = playerReference.transform.position;
        currentDir = body.position - playerPosition;
        currentDir.Normalize();
        Walk();
    }

    public void Wander()
    {
        // If at the new target and it's time to wander again, get a new target position.
        if (IsCloseTo(targetPosition, body.position)) 
        {
            if ((Time.time - timeOfLastWander) > WanderWaitTime)
            {
                timeOfLastWander = Time.time;
                var wanderBoundsX = (left: startingPosition.x - 2, right: startingPosition.x + 2);
                var wanderBoundsZ = (bottom: -2.5f, top: 1.2f);

                Vector3 currPosition = body.position;
                float targetX = (float)(Random.value * (wanderBoundsX.right - wanderBoundsX.left) + wanderBoundsX.left);
                float targetZ = (float)(Random.value * (wanderBoundsZ.top - wanderBoundsZ.bottom) + wanderBoundsZ.bottom);
                float targetY = currPosition.y;

                targetPosition = new Vector3(targetX, targetY, targetZ);
            }
            else
            {
                Stop();
            }
        }
        else
        {
            currentDir = targetPosition - body.position;
            currentDir.Normalize();
            Walk();
        }
    }

    public void Punch()
    {
        //seperate methods for each possible attack and select randomly? How many attacks will grunts have?
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

    private bool IsCloseTo(Vector3 target, Vector3 position)
    {
        float diffX = System.Math.Abs(target.x - position.x);
        float diffY = System.Math.Abs(target.y - position.y);
        float diffZ = System.Math.Abs(target.z - position.z);

        return diffX <= 0.1 && diffY <= 0.1 && diffZ <= 0.1;
    }
}
