using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaveBoy : Enemy
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// CLASS VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////////

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
}
