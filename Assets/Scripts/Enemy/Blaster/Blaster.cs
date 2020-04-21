using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : Enemy
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// CLASS VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    float minShootDist = 2f;
    float maxShootDist = 4f;
    float shootThreshold = 0.2f;
    int directive; //to decide whether the blaster wants to position for shooting or punching

    public void Start()
    {
        PUNCH_ANIM = "Blaster_Punch1";
        LAUNCH_RISE_ANIM = "Blaster_Launch_Rise";
        LAUNCH_FALL_ANIM = "Blaster_Launch_Fall";
        LAUNCH_LAND_ANIM = "Blaster_Launch_Land";
        GROUNDED_ANIM = "Blaster_Idle_Grounded";
        STAND_ANIM = "Blaster_Standing";
        HURT_GROUNDED_ANIM = "Blaster_Hurt_Grounded";
        HURT_STANDING_ANIM = "Blaster_Hurt";


        targetPosition = new Vector3(body.position.x, startingPosition.y, startingPosition.z);
        startingPosition = targetPosition;
        playerReference = GameObject.Find("Player");
        currentState = EnemyState.idle;
        currentHealth = maxHealth;
        isWaiting = true;
        fleeHealth = 30;
    }

    public virtual void Update()
    {
        base.Update();

        CheckAnims();

        Vector3 playerPosition = playerReference.transform.position;
        float currentDistance = Vector3.Distance(body.position, playerPosition);
        float currentXDistance = Mathf.Abs(body.position.x - playerPosition.x); //need this for preDialogue checks

        //remains fully idle until player reaches a certain point (should be equivalent to a dialogue point)
        if (currentState == EnemyState.preDialogueIdle)
        {
            Stop();
            if (currentXDistance < preDialogueBufferDist)
            {
                currentState = EnemyState.idle;
            }
            return;
        }

        if (isWaiting)
        {
            isWaiting = !playerReference.GetComponent<Hero>().Engage(this);
        }

        if (paused >= 0)
        {
            paused--;
        }
        else if (isWaiting)
        {
            currentState = EnemyState.waiting;
        }
        //Universal state change logic
        else if (!(isLaunching || isHurting || isAttacking || isWaiting || isGrounded || isStanding))
        {
            //might have to rework this to flee to a determined point rather than a standing distance
            if (currentDistance <= 3 && (currentHealth <= fleeHealth || currentState == EnemyState.fleeing) && !isHurting)
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
        }

        //Act on the current state
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
            case EnemyState.waiting:
                Wait();
                break;
            case EnemyState.shooting:
                Shoot();
                break;
            default:
                break;
        }

    }

    public override void Approach()
    {
        currentDir = targetPosition - body.position;
        float currentDistance = currentDir.magnitude;
        currentDir.Normalize();

        if (IsCloseTo(targetPosition, body.position, 0.1f))
        {
            currentState = EnemyState.shooting;
        }
        else if (currentDistance <= walkDistance)
        {
            Walk();
        }
        else
        {
            Run();
        }
    }

    public override void Attack()
    {
        Stop();
        float attackThreshold;

        switch (currentLevel)
        {
            case DifficultyLevel.easy:
                attackThreshold = .6f;
                break;
            case DifficultyLevel.medium:
                attackThreshold = .8f;
                break;
            case DifficultyLevel.hard:
                attackThreshold = .95f;
                break;
            default:
                attackThreshold = .6f;
                break;
        }

        float rand = Random.value;
        if (rand <= attackThreshold)
        {
            Punch();
        }
        else
        {
            StopAndPause(attackingPauseTime);
        }
    }

    public void Shoot()
    {
        Stop();
        if (!isAttacking)
        {
            Vector3 playerPosition = playerReference.transform.position;
            if (InShootingRange(playerPosition, body.position) && Random.value <= shootThreshold)
            {
                if (System.Math.Abs(playerPosition.x - body.position.x) >= minShootDist)
                {
                    if (Random.value <= shootThreshold)
                    {
                        baseAnim.SetTrigger("Shoot");
                    }
                }
                else
                {
                    currentState = EnemyState.attacking;
                }
            }
            else
            {
                currentState = EnemyState.approaching;
            }
        }
    }

    public bool InShootingRange(Vector3 target, Vector3 position)
    {
        float diffX = System.Math.Abs(target.x - position.x);
        float diffZ = System.Math.Abs(target.z - position.z);

        return (diffX <= maxShootDist && diffZ <= 0.1);
    }
}
