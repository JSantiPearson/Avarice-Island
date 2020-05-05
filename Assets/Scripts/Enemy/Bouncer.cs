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
    		case "BouncerBrad":
                PUNCH_ANIM = "BradPunchAnim";
                EXTRA_ATTACK1_ANIM = "BradKickAnim";
                EXTRA_ATTACK2_ANIM = "BradBigPunchAnim";
                EXTRA_ATTACK3_ANIM = "BradGroundedKickAnim";
                LAUNCH_ANIM = "BradLaunchAnim";
        		GROUNDED_ANIM = "BradGroundedAnim"; //no regular grounded anim?
        		STAND_ANIM = "BradGetUpAnim";
        		HURT_GROUNDED_ANIM = "BradHurtGroundedAnim";
        		HURT_STANDING_ANIM = "BradHurtAnim";
        		break;

        	case "BouncerRex":
        		PUNCH_ANIM = "RexPunchAnim";
                EXTRA_ATTACK1_ANIM = "RexKickAnim";
                EXTRA_ATTACK2_ANIM = "RexBigPunchAnim";
                EXTRA_ATTACK3_ANIM = "RexGroundedKickAnim";
                LAUNCH_ANIM = "RexLaunchAnim";
        		GROUNDED_ANIM = "RexGroundedAnim"; //no regular grounded anim?
        		STAND_ANIM = "RexGetUpAnim";
        		HURT_GROUNDED_ANIM = "RexHurtGroundedAnim";
        		HURT_STANDING_ANIM = "RexHurtAnim";
        		break;
    	}


        targetPosition = new Vector3(body.position.x, startingPosition.y, startingPosition.z);
        startingPosition = targetPosition;
        playerReference = GameObject.Find("Player");
        currentState = EnemyState.preDialogueIdle;
        currentHealth = maxHealth;
        isWaiting = true;
        fleeHealth = 30;
        transform.localScale = new Vector3((-1)*this.size, this.size, 1);
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
