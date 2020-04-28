using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawler : Enemy
{

		public void Start()
		{
			targetPosition = new Vector3(body.position.x, startingPosition.y, startingPosition.z);
			startingPosition = targetPosition;
			playerReference = GameObject.Find("Player");
			currentState = EnemyState.idle;
			currentHealth = maxHealth;
			isWaiting = true;
			fleeHealth = 30;
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
