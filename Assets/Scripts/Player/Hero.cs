using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Actor
{

    public List<Actor> engaged;
    private readonly object balanceLock = new object();
    public int numEngagements;

    public const float maxHealth = 150;
    public float currentHealth;

    public float walkSpeed = 2;
    public float runSpeed = 5;

    public bool isRunning;
    bool isMoving;
    float lastWalk;
    bool attack;

    public bool canRun = true;
    float tapAgainToRunTime = 0.4f;
    Vector3 lastWalkVector;

    Vector3 currentDir;
    bool isFacingLeft;

    bool isJumpLandAnim;
    bool isJumpingAnim;

    public InputHandler input;

    public float jumpForce = 1700;
    private float jumpDuration = 0.2f;
    private float lastJumpTime;

    bool isAttackingAnim;
    float lastAttackTime;
    float attackLimit = 0.14f;

    public void Start()
    {
        engaged = new List<Actor>(numEngagements);
        currentHealth = maxHealth;
    }

    public override void Update()
    {
        base.Update();

        isAttackingAnim = baseAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        isJumpLandAnim = baseAnim.GetCurrentAnimatorStateInfo(0).IsName("JumpLand");
        isJumpingAnim = baseAnim.GetCurrentAnimatorStateInfo(0).IsName("JumpRise") ||
          baseAnim.GetCurrentAnimatorStateInfo(0).IsName("JumpFall");


        float h = input.GetHorizontalAxis();
        float v = input.GetVerticalAxis();
        bool jump = input.GetJumpButtonDown();
        attack = input.GetAttackButtonDown();
        bool FeverAttack = input.GetFeverAttackButtonDown();

        currentDir = new Vector3(h, 0, v);
        currentDir.Normalize();

        if (!isAttackingAnim)
        {
            if ((v == 0 && h == 0))
            {
                Stop();
                isMoving = false;
            }
            else if (!isMoving && (v != 0 || h != 0))
            {
                isMoving = true;
                float dotProduct = Vector3.Dot(currentDir, lastWalkVector);
                if (canRun && Time.time < lastWalk + tapAgainToRunTime && dotProduct > 0)
                {
                    Run();
                }
                else
                {
                    Walk();
                    if (h != 0)
                    {
                        lastWalkVector = currentDir;
                        lastWalk = Time.time;
                    }
                }
            }
        }


        if (jump && !isJumpLandAnim && !isAttackingAnim &&
        (onGround || (isJumpingAnim && Time.time < lastJumpTime +
        jumpDuration)))
        {
            Jump(currentDir);
        }

        if (Input.GetButtonDown("Attack"))
        {
            attack = true;
            baseAnim.SetBool("attack", attack);
        }
        else if (Input.GetButtonUp("Attack"))
        {
            attack = false;
            baseAnim.SetBool("attack", attack);

        }


    }

    public void Stop()
    {
        speed = 0;
        baseAnim.SetFloat("Speed", speed);
        isRunning = false;
        baseAnim.SetBool("IsRunning", isRunning);
    }

    public void Walk()
    {
        speed = walkSpeed;
        baseAnim.SetFloat("Speed", speed);
        isRunning = false;
        baseAnim.SetBool("IsRunning", isRunning);
    }


    void FixedUpdate()
    {
        Vector3 moveVector = currentDir * speed;
        if (onGround && !isAttackingAnim)
        {
            body.MovePosition(transform.position + moveVector * Time.fixedDeltaTime);
            baseAnim.SetFloat("Speed", moveVector.magnitude);
        }

        if (moveVector != Vector3.zero)
        {
            if (moveVector.x != 0)
            {
                //transform.Rotate(0f, 180f, 0f);

                isFacingLeft = moveVector.x < 0;
            }

            FlipSprite(isFacingLeft);
        }
    }

    public void Run()
    {
        speed = runSpeed;
        isRunning = true;
        baseAnim.SetBool("IsRunning", isRunning);
        baseAnim.SetFloat("Speed", speed);
    }

    void Jump(Vector3 direction)
    {
        if (!isJumpingAnim)
        {
            baseAnim.SetTrigger("Jump");
            lastJumpTime = Time.time;

            Vector3 horizontalVector = new Vector3(direction.x, 0, direction.z) * speed * 40;
            body.AddForce(horizontalVector, ForceMode.Force);
        }

        Vector3 verticalVector = Vector3.up * jumpForce * Time.deltaTime;
        body.AddForce(verticalVector, ForceMode.Force);
    }


    protected override void Landed()
    {
        base.Landed();
        Walk();
    }

    public bool getRunning()
    {
        return isRunning;
    }

    //Not sure how Hunter wanted to do these but here is a quick and dirty version -Ethan
    public void hurt(float damage)
    {
        takeDamage(damage);
        baseAnim.SetTrigger("Hurt");
    }

    public void takeDamage(float damage)
    {
        currentHealth = (currentHealth - damage);
        gameObject.GetComponent<Health>().health = (int) (currentHealth / (maxHealth / 5)); //Update the Health script and pips in the UI
    }

    //public override void Attack()
    //{


        //baseAnim.SetBool("attack", attack);
        //lastAttackTime = Time.time;
       // attack = false;
     //   baseAnim.SetBool("atatck", attack);
   // }

    public bool Engage(Actor enemy)
    {
        lock (balanceLock)
        {
            if (engaged.Count < numEngagements)
            { 
                engaged.Add(enemy);
                return true;
            }
            return false;
        }
    }
}
