using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaveGirl : Enemy
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// CLASS VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public void Start()
    {
        PUNCH_ANIM = "RaveGirlPunchAnim";
        LAUNCH_ANIM = "RaveGirlLaunchAnim";
        GROUNDED_ANIM = "RaveGirlGroundedAnim";
        STAND_ANIM = "RaveGirlGetUpAnim";
        HURT_GROUNDED_ANIM = "RaveGirlHurtGroundedAnim";
        HURT_STANDING_ANIM = "RaveGirlHurtAnim";


        targetPosition = new Vector3(body.position.x, startingPosition.y, startingPosition.z);
        startingPosition = targetPosition;
        playerReference = GameObject.Find("Player");
        currentState = EnemyState.idle;
        currentHealth = maxHealth;
        isWaiting = true;
        fleeHealth = 30;
    }
}
