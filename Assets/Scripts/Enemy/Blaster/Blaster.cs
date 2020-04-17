using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : Enemy
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// CLASS VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////////

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
}
