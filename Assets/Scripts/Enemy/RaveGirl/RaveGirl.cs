using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaveGirl : Enemy
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// CLASS VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    float teleportDistance = 2;
    float teleportThreshold = .08f;
    protected string TELEPORT_IN_ANIM;
    protected string TELEPORT_OUT_ANIM;

    public void Start()
    {
        PUNCH_ANIM = "RaveGirlPunchAnim";
        BREATHING_ANIM = "RaveGirlBreathAnim";
        EXTRA_ATTACK1_ANIM = "RaveGirlAttack2";
        EXTRA_ATTACK2_ANIM = "RaveGirlAttack3";
        EXTRA_ATTACK3_ANIM = "RaveGirlAttack4";
        LAUNCH_RISE_ANIM = "LaunchRiseAnim";
        LAUNCH_FALL_ANIM = "LaunchFallAnim";
        LAUNCH_LAND_ANIM = "LaunchLandAnim";
        GROUNDED_ANIM = "RaveGirlGroundedAnim";
        STAND_ANIM = "RaveGirlGetUpAnim";
        HURT_GROUNDED_ANIM = "RaveGirlHurtGroundedAnim";
        HURT_STANDING_ANIM = "RaveGirlHurtAnim";
        TELEPORT_OUT_ANIM = "RaveGirlTeleportOutAnim";
        TELEPORT_IN_ANIM = "RaveGirlTeleportInAnim";

        targetPosition = new Vector3(body.position.x, startingPosition.y, startingPosition.z);
        startingPosition = targetPosition;
        playerReference = GameObject.Find("Player");
        currentState = EnemyState.idle;
        currentHealth = maxHealth;
        isWaiting = true;
        fleeHealth = 30;
        teleportThreshold = .08f;
    }

    public override void FreezeEnemy()
    {
      // Freeze enemy in place during nonmoving anims
      if (isGrounded || isLaunching || isBreathing || isStanding || isTeleporting){
        walkSpeed = 0f;
        runSpeed = 0f;
      }
      else {
        walkSpeed = 1.5f;
        runSpeed = 4f;
      }
    }

    public override void DetermineState(float currentDistance)
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
      else if (currentDistance > lastDistance && currentDistance > 3 && Random.value <= teleportThreshold){
        currentState = EnemyState.teleporting;
      }
      else if (currentDistance <= attackDistance) currentState = EnemyState.attacking;
      else if (currentDistance <= noticeDistance) currentState = EnemyState.approaching;
      else currentState = EnemyState.idle;
    }

    public override void ChangeState(EnemyState currentState)
    {
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
          case EnemyState.teleporting:
              StartCoroutine(Teleport());
              break;
          default:
              break;
      }
    }

    public virtual void Attack()
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

        if (Random.value <= attackThreshold)
        {
            if (lastAttack == LastAttack.punch3)
            {
                Kick();
            }
            else if (lastAttack == LastAttack.punch2)
            {
                Punch3();
            }
            else if (lastAttack == LastAttack.punch1)
            {
                Punch2();
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

    public void Punch2()
    {
        //seperate methods for each possible attack and select randomly? How many attacks will grunts have?
        Stop();
        //face the player
        FlipSprite(body.position.x > playerPosition.x);
        baseAnim.SetTrigger("Punch2");
        lastAttack = LastAttack.punch2;
    }

    public void Punch3()
    {
        //seperate methods for each possible attack and select randomly? How many attacks will grunts have?
        Stop();
        //face the player
        FlipSprite(body.position.x > playerPosition.x);
        baseAnim.SetTrigger("Punch3");
        lastAttack = LastAttack.punch3;
    }

    IEnumerator Teleport()
    {
      Stop();
      baseAnim.SetTrigger("TeleportOut");
      yield return new WaitForSeconds(0.5f);
      float teleportLocation;
      if (playerReference.GetComponent<Hero>().isFacingLeft){
        if (playerPosition.x < leftCamBound + 2){
          teleportLocation = playerPosition.x + 1;
        }
        else{
          teleportLocation = playerPosition.x - 2;
        }
      }
      else {
        if (playerPosition.x > rightCamBound - 2){
          teleportLocation = playerPosition.x - 1;
        }
        else{
          teleportLocation = playerPosition.x + 2;
        }
      }
      Vector3 temp = new Vector3(teleportLocation, playerReference.transform.position.y, playerReference.transform.position.z);
      transform.position = temp;
      baseAnim.SetTrigger("TeleportIn");
    }

    public override void CheckAnims()
    {
        isTeleporting = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(TELEPORT_OUT_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(TELEPORT_IN_ANIM);
        isAttacking = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(PUNCH_ANIM);
        isLaunching = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(LAUNCH_ANIM);
        isGrounded = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(GROUNDED_ANIM);
        isStanding = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(STAND_ANIM);
        isHurting = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(HURT_GROUNDED_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(HURT_STANDING_ANIM);
        isBreathing = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(BREATHING_ANIM);
    }
}
