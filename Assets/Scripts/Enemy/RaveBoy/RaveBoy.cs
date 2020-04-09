using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaveBoy : Enemy
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// CLASS VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    protected string FLASH_ANIM = "RaveBoyFlashAnim";

    public void Start()
    {
        PUNCH_ANIM = "RaveBoyPunchAnim";
        LAUNCH_ANIM = "RaveBoyLaunchAnim";
        GROUNDED_ANIM = "RaveBoyGroundedAnim";
        STAND_ANIM = "RaveBoyGetUpAnim";
        HURT_GROUNDED_ANIM = "RaveBoyHurtGroundedAnim";
        HURT_STANDING_ANIM = "RaveBoyHurtAnim";


        targetPosition = new Vector3(body.position.x, startingPosition.y, startingPosition.z);
        startingPosition = targetPosition;
        playerReference = GameObject.Find("Player");
        currentState = EnemyState.idle;
        currentHealth = maxHealth;
        isWaiting = true;
        fleeHealth = 30;
    }

    public override void Attack()
    {
        Stop();
        float attackThreshold;
        float flashThreshold = .05f;

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
        if (rand <= flashThreshold)
        {
          Flash();
        }
        else if (rand <= attackThreshold)
        {
            Punch();
        }
        else
        {
            StopAndPause(attackingPauseTime);
        }
    }

    public void Flash()
    {
        //seperate methods for each possible attack and select randomly? How many attacks will grunts have?
        baseAnim.SetTrigger("Flash");
    }
}
