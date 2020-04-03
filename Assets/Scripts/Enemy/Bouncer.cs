using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        PUNCH_ANIM = "enemy_grunt_punch";
        LAUNCH_ANIM = "enemy_grunt_launch";
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
