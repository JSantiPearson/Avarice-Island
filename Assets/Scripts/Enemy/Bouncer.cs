using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : Enemy
{
    // Start is called before the first frame update

	public string bouncerName;

    void Start()
    {
    	//the 2 distinct bouncers have distinct anims. this would be cleaner
    	//if we made them clones or if we somehow changed the CheckAnims() scheme in enemy superclass
    	//these animators also have multiple animations that correspond to a single state here. i just used one for each.
    	switch(bouncerName){
    		case "BouncerRex":
        		PUNCH_ANIM = "BradPunchAnim";
        		LAUNCH_ANIM = "BradLaunchedAnim";
        		GROUNDED_ANIM = "BradHurtGroundedAnim"; //no regular grounded anim?
        		STAND_ANIM = "BradGetUpAnim";
        		HURT_GROUNDED_ANIM = "BradHurtGroundedAnim";
        		HURT_STANDING_ANIM = "BradHurtFrontAnim";
        		break;

        	case "BouncerBrad":
        		PUNCH_ANIM = "RexPunchAnim";
        		LAUNCH_ANIM = "RexLaunchedAnim";
        		GROUNDED_ANIM = "RexHurtGroundedAnim"; //no regular grounded anim?
        		STAND_ANIM = "RexGetUpAnim";
        		HURT_GROUNDED_ANIM = "RexHurtGroundedAnim";
        		HURT_STANDING_ANIM = "RexHurtFrontAnim";
        		break;
    	}


        targetPosition = new Vector3(body.position.x, startingPosition.y, startingPosition.z);
        startingPosition = targetPosition;
        playerReference = GameObject.Find("Player");
        currentState = EnemyState.preDialogueIdle;
        currentHealth = maxHealth;
        isWaiting = true;
        fleeHealth = 30;
    }

		public void BigPunch()
    {
        Stop();
        Vector3 playerPosition = playerReference.transform.position;
        FlipSprite(body.position.x > playerPosition.x);
        baseAnim.SetTrigger("BigPunch");
        lastAttack = LastAttack.none;
    }

		public override void Attack()
    {
				Debug.Log(currentHealth);
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
	            if(lastAttack == LastAttack.punch2)
	            {
	                Kick();
	            }
	            else
	            {
	                Punch();
	            }
						}
						else {
							BigPunch();
						}
        }
        else
        {
            StopAndPause(attackingPauseTime);
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //
    //}
}
