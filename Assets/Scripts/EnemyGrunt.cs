using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrunt : Actor
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// CLASS VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public int maxHealth = 100;
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

    Vector3 startingPosition = new Vector3(12.0f, 2.5f, 1.5f);
    Vector3 targetPosition;

    float timeOfLastWander;
    float WanderWaitTime;

    float noticeDistance = 4;
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
        currentHealth = maxHealth;
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
                        fleeHealth -= 5;
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
            if (targetPosition.Equals(body.position) && (Time.time - timeOfLastWander) > WanderWaitTime)
            {
                var wanderBoundsX = (left: startingPosition.x - 4, right: startingPosition.x + 4);
                var wanderBoundsY = (bottom: startingPosition.y - 1.8, top: 4.4f);

                Vector3 currPosition = body.position;
                float targetX = (float)(Random.value * (wanderBoundsX.right - wanderBoundsX.left) + wanderBoundsX.left);
                float targetZ = (float)(Random.value * (wanderBoundsY.top - wanderBoundsY.bottom) + wanderBoundsY.bottom);
                float targetY = currPosition.z;

                targetPosition = new Vector3(targetX, targetY, targetZ);
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

    public void TakeDamage (int damage){

      currentHealth -= damage;

      if(currentHealth <= 0){
        //die
        Debug.Log("Enemy Die");
      }
    }
}
