using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : Enemy
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// CLASS VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    float minShootDist = 1f;
    float maxShootDist = 4f;
    float shootThreshold = 0.2f;
    int directive; //to decide whether the blaster wants to position for shooting or punching

    protected string SHOOT_ANIM = "Blaster_Blast";

    public void Start()
    {

        PUNCH_ANIM = "Blaster_Punch1";
        BREATHING_ANIM = "Blaster_Breath";
        EXTRA_ATTACK1_ANIM = "Blaster_Kick1";
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
        currentState = EnemyState.approaching;
        currentHealth = maxHealth;
        isWaiting = true;
        fleeHealth = 30;
    }

    public virtual void Update()
    {
        base.Update();

        CheckAnims();

        FreezeEnemy();

        Vector3 playerPosition = playerReference.transform.position;
        float currentDistance = Vector3.Distance(body.position, playerPosition);
        float currentXDistance = Mathf.Abs(body.position.x - playerPosition.x); //need this for preDialogue checks

        //remains fully idle until player reaches a certain point (should be equivalent to a dialogue point)
        if (currentState == EnemyState.preDialogueIdle)
        {
            Stop();
            if (currentXDistance < preDialogueBufferDist)
            {
                currentState = EnemyState.approaching;
            }
            return;
        }

        if (isWaiting)
        {
            isWaiting = !playerReference.GetComponent<Hero>().Engage(this);
        }

        if (paused > 0)
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
            else
            {
                //currentState = EnemyState.approaching;
            }
        }
        else
        {
            currentState = EnemyState.idle;
        }

        //reset the combo indicator if we interrupted attacking for any reason.
        if (currentState != EnemyState.attacking)
        {
            lastAttack = LastAttack.none;
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

    public override void Idle()
    {
        Stop();
    }

    public override void Approach()
    {
        Vector3 playerPosition = playerReference.transform.position;
        float targetX;
        if(body.position.x > playerPosition.x)
        {
            targetX = playerPosition.x + 3;
        }
        else
        {
            targetX = playerPosition.x - 3;
        }
        targetPosition = new Vector3(targetX, playerPosition.y, playerPosition.z);
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
            if (lastAttack == LastAttack.punch2)
            {
                Kick();
            }
            else
            {
                Punch();
            }
        }
        else
        {
            StopAndPause(attackingPauseTime);
        }
    }

    public void Shoot()
    {
        Stop();
        //face the player
        Vector3 playerPosition = playerReference.transform.position;
        FlipSprite(body.position.x > playerPosition.x);

        if (!isAttacking)
        {
            if (InShootingRange(playerPosition, body.position))
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

        return (diffX <= maxShootDist && diffZ <= 0.3);
    }

    public override void CheckAnims()
    {
        isAttacking = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(PUNCH_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(SHOOT_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(EXTRA_ATTACK1_ANIM);
        isLaunching = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(LAUNCH_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(LAUNCH_RISE_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(LAUNCH_FALL_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(LAUNCH_LAND_ANIM);
        isGrounded = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(GROUNDED_ANIM);
        isStanding = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(STAND_ANIM);
        isHurting = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(HURT_GROUNDED_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(HURT_STANDING_ANIM);
        isBreathing = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(BREATHING_ANIM);
    }
}
