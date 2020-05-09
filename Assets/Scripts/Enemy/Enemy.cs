using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// CLASS VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    protected string PUNCH_ANIM;
    protected string BREATHING_ANIM;
    protected string EXTRA_ATTACK1_ANIM;
    protected string EXTRA_ATTACK2_ANIM;
    protected string EXTRA_ATTACK3_ANIM;
    protected string EXTRA_ATTACK4_ANIM;
    protected string LAUNCH_ANIM;
    protected string LAUNCH_RISE_ANIM;
    protected string LAUNCH_FALL_ANIM;
    protected string LAUNCH_LAND_ANIM;
    protected string GROUNDED_ANIM;
    protected string STAND_ANIM;
    protected string HURT_GROUNDED_ANIM;
    protected string HURT_STANDING_ANIM;

    public float preDialogueBufferDist = 0.5f;

    public int maxHealth = 100;
    public float currentHealth;
    public float walkSpeed = 1.5f;
    public float runSpeed = 4f;
    protected int rightBound = 15;
    protected int leftBound = 7;

    public float lastDistance;
    public Vector3 lastPosition;

    protected bool isAttacking;
    protected bool isHurting;
    protected bool isBreathing;
    protected float lastHurtTime;
    protected bool isLaunching;
    protected float lastLaunchTime;
    protected float launchForce = 250f;
    protected bool isStanding;
    protected bool isGrounded;
    protected bool isTeleporting;

    protected float waitDistance = 4;
    protected bool isWaiting;
    protected bool isFleeing;

    protected int paused;
    protected int waitingPauseTime = 40;
    protected int attackingPauseTime = 40;

    protected bool isMoving;
    protected float lastWalk;
    protected Vector3 lastWalkVector;

    protected bool takeBreath = true;

    protected Vector3 currentDir;
    protected bool isFacingLeft;

    public Vector3 startingPosition = new Vector3(12.0f, 2.475173f, 1.0f);
    public Vector3 targetPosition = new Vector3(12.0f, 2.5f, 1.0f);
    public Vector3 playerPosition;

    protected float timeOfLastWander;
    protected float WanderWaitTime = 5;

    protected float noticeDistance = 7;
    protected float walkDistance = 3.5f;
    protected float attackDistance = 1;
    protected int fleeHealth;
    protected bool noticed = false;

    public GameObject hitEffectPrefab, ghostPrefab;

    List<Actor> playerEngagements;

    private bool enemyPaused;

    float cameraHalfWidth;
    Vector3 camera;
    public float leftCamBound;
    public float rightCamBound;

    public AudioManager audioManager;


    public enum EnemyState
    {
        //noticed state? alternatively, "if state not approaching and approachable" case in state logic of update
        preDialogueIdle, //only used when enemy should be idling before player reaches them
        idle,
        pacing,
        approaching,
        attacking,
        fleeing,
        wandering,
        waiting,
        teleporting,
        shooting
    }

    protected enum LastAttack
    {
        none,
        punch1,
        punch2,
        punch3,
        kick
    }

    public DifficultyLevel? currentLevel = null;
    public EnemyState currentState;
    protected LastAttack lastAttack = LastAttack.none;

    public GameObject playerReference;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Game Engine Methods
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public void Start()
    {
        targetPosition = new Vector3(body.position.x, startingPosition.y, startingPosition.z);
        startingPosition = targetPosition;
        playerReference = GameObject.Find("Player");
        playerPosition = playerReference.transform.position;
        lastDistance = Vector3.Distance(body.position, playerPosition);
        currentState = EnemyState.idle; //override this if preDialogueIdle needs to be used
        currentHealth = maxHealth;
        isWaiting = true;
        isTeleporting = false;
        isFleeing = false;
        fleeHealth = 30;
        takeBreath = true;
        transform.localScale = new Vector3(this.size, this.size, 1); //fixes scale bug on spawn
        //audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        //audioObject = GameObject.Find("AudioManager");
    }

    //can't currently find audiomanager with start method because enemies don't call their base start method.
    //this will work for now since all enemies spawn after the audiomanager has been initialized
    public void Awake(){
        //assign audio for hit sound
        if(GameObject.Find("AudioManager")){
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
        cameraHalfWidth = Camera.main.GetComponent<CameraBounds>().cameraHalfWidth;
    }

    /**
    / Update Methods
    **/

    public virtual void FreezeEnemy()
    {
      // Freeze enemy in place during nonmoving anims
      if (isGrounded || isLaunching || isBreathing || isStanding || isHurting){
        walkSpeed = 0f;
        runSpeed = 0f;
      }
      else {
        walkSpeed = 1.5f;
        runSpeed = 4f;
      }
    }

    public virtual void Update()
    {

        FreezeEnemy();

      // camera bounds
        camera = Camera.main.transform.position;
        leftCamBound = camera.x - cameraHalfWidth;
        rightCamBound = camera.x + cameraHalfWidth;

        if(GameManager.enemiesOn && enemyPaused){ //unpause
            enemyPaused = false;
            //body.Sleep();
            baseAnim.enabled = true;
        }

        if(!GameManager.enemiesOn && !enemyPaused){ //disable animator on first pass after pause
            enemyPaused=true;
            baseAnim.enabled=false;
            //body.WakeUp();
            return;
        } else if (enemyPaused){ //just skip update
            return;
        }

        base.Update();

        CheckAnims();

        playerPosition = playerReference.transform.position;
        float currentDistance = Vector3.Distance(body.position, playerPosition);
        float currentXDistance = Mathf.Abs(body.position.x - playerPosition.x); //need this for preDialogue checks

        playerEngagements = playerReference.GetComponent<Hero>().engaged;

        //remains fully idle until player reaches a certain point (should be equivalent to a dialogue point)
        if(currentState == EnemyState.preDialogueIdle){
            Stop();
            Debug.Log("in predialogue");
            if(currentXDistance<preDialogueBufferDist){
                currentState = EnemyState.idle;
                Debug.Log("breaking out of predialogue");
            }
            return;
        }

        if (isWaiting && !isFleeing)
        {
            isWaiting = !playerReference.GetComponent<Hero>().Engage(this);
        }
          if (paused >= 0)
          {
              paused--;
          }
          else if (isWaiting)
          {
              currentState = EnemyState.waiting;
          }
          //Universal state change logic
          else if (!(isLaunching || isHurting || isAttacking || isWaiting || isGrounded || isStanding || isTeleporting))
          {
                DetermineState(currentDistance);
            }
            else
            {
                currentState = EnemyState.idle;
            }

            //reset the combo indicator if we interrupted attacking, unless player is launched from a heavy attack.
            if (currentState != EnemyState.attacking)
            {
                lastAttack = LastAttack.none;
            }
            //Act on the current state
            ChangeState(currentState);
            lastDistance = currentDistance;
            lastPosition = transform.position;
        }

    public virtual void DetermineState(float currentDistance)
    {
      //might have to rework this to flee to a determined point rather than a standing distance
      if (currentDistance <= 3 && (currentHealth <= fleeHealth || currentState == EnemyState.fleeing) && !isHurting)
      {
          if (currentState != EnemyState.fleeing)
          {
              switch (currentLevel)
              {
                  case DifficultyLevel.easy:
                      fleeHealth -= 30;
                      break;
                  case DifficultyLevel.medium:
                      fleeHealth -= 15;
                      break;
                  case DifficultyLevel.hard:
                      fleeHealth -= 10;
                      break;
                  default:
                      fleeHealth -= 30;
                      break;
              }
          }
          currentState = EnemyState.fleeing;
      }
      else if (currentDistance <= attackDistance) currentState = EnemyState.attacking;
      else if (currentDistance <= noticeDistance) currentState = EnemyState.approaching;
      else currentState = EnemyState.idle;
    }

    public virtual void ChangeState(EnemyState currentState)
    {
      switch (currentState)
      {
          case EnemyState.idle:
              Idle();
              break;
          case EnemyState.pacing:
              Pace();
              break;
          case EnemyState.approaching:
              Approach();
              break;
          case EnemyState.attacking:
              Attack();
              break;
          case EnemyState.fleeing:
              Flee();
              break;
          case EnemyState.wandering:
              Wander();
              break;
          case EnemyState.waiting:
              Wait();
              break;
          default:
              break;
      }
    }

    void FixedUpdate()
    {
        //pause logic
        if (!GameManager.enemiesOn) {
         body.velocity = Vector3.zero;
         body.angularVelocity = Vector3.zero;
         return;
        }

        Vector3 moveVector = currentDir * speed;
        body.MovePosition(transform.position + moveVector * Time.fixedDeltaTime);
        baseAnim.SetFloat("Speed", moveVector.magnitude);

        if (moveVector != Vector3.zero)
        {
            if (moveVector.x != 0)
            {
                isFacingLeft = moveVector.x < 0;
            }
            FlipSprite(isFacingLeft);
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "floor":
                onGround = true;
                baseAnim.SetBool("onGround", onGround);
                Landed();
                break;
            case "hit":
                Hit(0, false);
                break;
            case "launch":
                Launch(collision.transform.position);
                break;
            default:
                break;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// State Methods
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public virtual void Idle()
    {
        //Idle anim? leg up on wall/kicking dirt/hand in pocket/smoking?
        Stop();
    }

    public void Pace()
    {
        float positionX = body.position.x;
        float positionY = body.position.y;

        if (positionX <= leftBound)
        {
            currentDir = Vector3.right;
            isFacingLeft = true;
            FlipSprite(isFacingLeft);
        }
        else if (positionX >= rightBound)
        {
            currentDir = Vector3.left;
            isFacingLeft = false;
            FlipSprite(isFacingLeft);
        }
        Walk();
    }

    public virtual void Approach()
    {
        targetPosition = playerReference.transform.position;
        currentDir = targetPosition - body.position;
        float currentDistance = currentDir.magnitude;
        currentDir.Normalize();

        if (currentDistance <= walkDistance)
        {
            Walk();
        }
        else
        {
            Run();
        }
    }

    public void CatchBreath()
    {
      Stop();
      isFleeing = false;
      baseAnim.SetTrigger("Rest");
      takeBreath = false;
    }

    public virtual void Attack()
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
            if(lastAttack == LastAttack.punch2)
            {
                Kick();
            }
            else
            {
                Punch();
            }
        }
        else
        {
            StopAndPause(attackingPauseTime);
        }
    }

    public void Flee()
    {
        isFleeing = true;
        currentDir = body.position - playerPosition;
        currentDir.Normalize();
        isWaiting = playerReference.GetComponent<Hero>().Disengage(this);
        //Run away if player is close or if a wall is hit
        if (body.position.y < 4 && body.position.x - playerPosition.x < 5 && body.position.x <= rightCamBound-0.9 && body.position.x >= leftCamBound+0.9){
          Run();
        }
        else {
          if (takeBreath){
            CatchBreath();
          }
          else{
            Wander();
          }
        }
    }

    public void Wander()
    {
        // If at the new target and it's time to wander again, get a new target position.
        if (IsCloseTo(targetPosition, body.position, 0.1f))
        {
            if ((Time.time - timeOfLastWander) > WanderWaitTime)
            {
                timeOfLastWander = Time.time;
                var wanderBoundsX = (left: startingPosition.x - 2, right: startingPosition.x + 2);
                var wanderBoundsZ = (bottom: -2.5f, top: 1.2f);

                Vector3 currPosition = body.position;
                float targetX = (float)(Random.value * (wanderBoundsX.right - wanderBoundsX.left) + wanderBoundsX.left);
                float targetZ = (float)(Random.value * (wanderBoundsZ.top - wanderBoundsZ.bottom) + wanderBoundsZ.bottom);
                float targetY = currPosition.y;

                targetPosition = new Vector3(targetX, targetY, targetZ);
            }
            else
            {
                Stop();
            }
        }
        else
        {
            currentDir = targetPosition - body.position;
            currentDir.Normalize();
            Walk();
        }
    }

    public void Wait()
    {
      if (!isFleeing){
        if (IsCloseTo(targetPosition, body.position, 0.1f))
        {
            var wanderBoundsX = (left: startingPosition.x - 5, right: startingPosition.x + 5);
            var wanderBoundsZ = (bottom: -2.5f, top: 1.2f);

            if (wanderBoundsX.left < leftCamBound){
              wanderBoundsX.left = leftCamBound + 1;
            }
            if (wanderBoundsX.right > rightCamBound){
              wanderBoundsX.right = rightCamBound - 1;
            }

            //Form a line connecting the player and enemy
            Vector3 desiredLine = body.position - playerPosition;
            desiredLine.Normalize();
            //Find a point on that line that is waitDistance away from player
            targetPosition = playerPosition + desiredLine * waitDistance;

            //Check that it's in bounds
            float targetX = targetPosition.x;
            float targetZ = targetPosition.z;
            if (targetX > wanderBoundsX.right) targetX = wanderBoundsX.right;
            if (targetX < wanderBoundsX.left) targetX = wanderBoundsX.left;
            if (targetZ > wanderBoundsZ.top) targetZ = wanderBoundsZ.top;
            if (targetZ < wanderBoundsZ.bottom) targetZ = wanderBoundsZ.bottom;

            targetPosition = new Vector3(targetX, playerPosition.y, targetZ);
            StopAndPause(waitingPauseTime);
        }
        else
        {
            currentDir = targetPosition - body.position;
            currentDir.Normalize();
            Walk();
        }
      }
      else {
        Flee();
      }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Action Methods
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public void Punch()
    {
        //seperate methods for each possible attack and select randomly? How many attacks will grunts have?
        Stop();
        //face the player
        FlipSprite(body.position.x > playerPosition.x);
        baseAnim.SetTrigger("Punch");
        if(lastAttack == LastAttack.punch1)
        {
            lastAttack = LastAttack.punch2;
        }
        else
        {
            lastAttack = LastAttack.punch1;
        }
    }

    public void Kick()
    {
        Stop();
        FlipSprite(body.position.x > playerPosition.x);
        baseAnim.SetTrigger("Kick");
        lastAttack = LastAttack.kick;
    }

    public void Stop()
    {
        speed = 0;
        isMoving = false;
        baseAnim.SetFloat("Speed", speed);
    }

    public void StopAndPause(int pauseTime)
    {
        Stop();
        paused = pauseTime;
        currentState = EnemyState.idle;
    }

    public void Walk()
    {
        isMoving = true;
        speed = walkSpeed;
        baseAnim.SetFloat("Speed", speed);
    }

    public void Run()
    {
        isMoving = true;
        speed = runSpeed;
        baseAnim.SetFloat("Speed", speed);
    }

    public void Hit(float damage, bool knockback)
    {
        Stop();
        isHurting = true;
        lastHurtTime = Time.time;
        Hurt(damage, knockback);
    }

    public void Zap(float damage)
    {
        Stop();
        isHurting = true;
        lastHurtTime = Time.time;
        Hurt(damage, false);
        baseAnim.SetTrigger("Zap");
        baseAnim.ResetTrigger("Hit"); //fixes an issue where grunts trigger a late hurt anim

    }

    public void Launch(Vector3 attackerLocation)
    {
      if (!isLaunching){
        Stop();
        isLaunching = true;
        lastLaunchTime = Time.time;
        baseAnim.SetTrigger("Launch");

        Vector3 horizontalLaunchInfluence = body.position - attackerLocation;
        horizontalLaunchInfluence.Normalize();
        horizontalLaunchInfluence = horizontalLaunchInfluence * launchForce / 2;
        body.AddForce(horizontalLaunchInfluence, ForceMode.Force);

        Vector3 verticalLaunchInfluence = Vector3.up * launchForce;
        body.AddForce(verticalLaunchInfluence, ForceMode.Force);
      }
    }

    public void Hurt(float damage, bool knockback)
    {
        if(audioManager!=null){ //allows us to deactivate audio manager temporarily
            audioManager.PlayOneShot("hitSound",0.2f);
        }
        Instantiate(hitEffectPrefab,this.transform.position,this.transform.rotation);
        currentHealth -= damage;
        Stop();
        isHurting = true;
        lastHurtTime = Time.time;
        if (!knockback){
          baseAnim.SetTrigger("Hit");
        }
        if (currentHealth <= 0){
            Die();
        }
    }

    private void Die()//Method for dying?
    {
        //currentState = EnemyState.dead;
        List<Actor> playerEngagements = playerReference.GetComponent<Hero>().engaged;
        playerEngagements.Remove(this);
        baseAnim.SetTrigger("Death");


        //ghost
        Instantiate(ghostPrefab,this.transform.position,this.transform.rotation);

        Destroy(body);
        Destroy(this.GetComponent<BoxCollider>());
        StartCoroutine(WaitAndDie(3));

    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Helper Functions
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public virtual void CheckAnims()
    {
        isAttacking = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(PUNCH_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(EXTRA_ATTACK1_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(EXTRA_ATTACK2_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(EXTRA_ATTACK3_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(EXTRA_ATTACK4_ANIM);
        isLaunching = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(LAUNCH_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(LAUNCH_RISE_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(LAUNCH_FALL_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(LAUNCH_LAND_ANIM);
        isGrounded = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(GROUNDED_ANIM);
        isStanding = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(STAND_ANIM);
        isHurting = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(HURT_GROUNDED_ANIM) ||
            baseAnim.GetCurrentAnimatorStateInfo(0).IsName(HURT_STANDING_ANIM);
        isBreathing = baseAnim.GetCurrentAnimatorStateInfo(0).IsName(BREATHING_ANIM);
    }

    IEnumerator WaitAndDie(float time){
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
