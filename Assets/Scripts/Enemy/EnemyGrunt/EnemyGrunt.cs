using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrunt : Enemy
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// CLASS VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public void Start()
    {
        PUNCH_ANIM = "enemy_grunt_punch";
        EXTRA_ATTACK2_ANIM = "GruntAttack2";
        LAUNCH_RISE_ANIM = "GruntLaunchRise";
        LAUNCH_FALL_ANIM = "GruntLaunchFall";
        LAUNCH_LAND_ANIM = "GruntLaunchLand";
        GROUNDED_ANIM = "enemy_grunt_grounded";
        STAND_ANIM = "enemy_grunt_stand";
        HURT_GROUNDED_ANIM = "enemy_grunt_hurt_grounded";
        HURT_STANDING_ANIM = "enemy_grunt_hurt_standing";


        targetPosition = new Vector3(body.position.x, startingPosition.y, startingPosition.z);
        startingPosition = targetPosition;
        playerReference = GameObject.Find("Player");
        currentState = EnemyState.idle;
        currentHealth = maxHealth;
        isWaiting = true;
        fleeHealth = 30;
    }
}
