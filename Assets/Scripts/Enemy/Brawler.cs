using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawler : Enemy
{

		public void Start()
		{
	    PUNCH_ANIM = "Brawler_Punch";
			BREATHING_ANIM = "BrawlerBreathAnim";
	    EXTRA_ATTACK1_ANIM = "Brawler_Bounce";
	    EXTRA_ATTACK2_ANIM = "Brawler_Bump";
	    LAUNCH_RISE_ANIM = "Brawler_Launch_Rise";
	  	LAUNCH_FALL_ANIM = "Brawler_Launch_Fall";
	    LAUNCH_LAND_ANIM = "Brawler_Launch_Land";
	    GROUNDED_ANIM = "Brawler_Idle_Grounded";
      STAND_ANIM = "Brawler_GetUp";
      HURT_GROUNDED_ANIM = "Brawler_Hurt_Grounded";
      HURT_STANDING_ANIM = "Brawler_Hurt";

      targetPosition = new Vector3(body.position.x, startingPosition.y, startingPosition.z);
			startingPosition = targetPosition;
			playerReference = GameObject.Find("Player");
			currentState = EnemyState.idle;
			currentHealth = maxHealth;
			isWaiting = true;
			fleeHealth = 30;
		}

		public void Punch()
    {
        //seperate methods for each possible attack and select randomly? How many attacks will grunts have?
        Stop();
        //face the player
        Vector3 playerPosition = playerReference.transform.position;
        FlipSprite(body.position.x > playerPosition.x);
        baseAnim.SetTrigger("Punch");
        lastAttack = LastAttack.punch1;
    }

		public void Bump()
		{
			Stop();
			Vector3 playerPosition = playerReference.transform.position;
			FlipSprite(body.position.x > playerPosition.x);
			baseAnim.SetTrigger("Bump");
			lastAttack = LastAttack.kick;
		}

		public void Bounce()
		{
			Stop();
			Vector3 playerPosition = playerReference.transform.position;
			FlipSprite(body.position.x > playerPosition.x);
			baseAnim.SetTrigger("Bounce");
			lastAttack = LastAttack.none;
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

        if (Random.value <= attackThreshold)
        {
						float attackType = Random.value;
						if (attackType <= .75f){
	            if(lastAttack == LastAttack.punch1)
	            {
	                Bump();
	            }
	            else
	            {
	                Punch();
	            }
						}
						else {
							Bounce();
						}
        }
        else
        {
            StopAndPause(attackingPauseTime);
        }
    }
}
