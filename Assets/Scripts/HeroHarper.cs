using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHarper : Actor  {


  public float walkSpeed = 2;
  public float runSpeed = 5;

  bool isRunning;
  bool isMoving;
  float lastWalk;
  public bool canRun = true;
  float tapAgainToRunTime = 0.2f;
  Vector3 lastWalkVector;

  Vector3 currentDir;
  bool isFacingLeft;

  bool isJumpLandAnim;
  bool isJumpingAnim;

  public InputHandler input;

  public float jumpForce = 1700;
  private float jumpDuration = 0.2f;
  private float lastJumpTime;
  private bool jumpSound=true; //true iff jump sound should be triggered

  bool isAttackingAnim;
  float lastAttackTime;
  float attackLimit = 0.14f;

  public override void Update() {
    base.Update();

    isAttackingAnim = baseAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack1");
    isJumpLandAnim = baseAnim.GetCurrentAnimatorStateInfo(0).IsName("JumpLand");
    isJumpingAnim = baseAnim.GetCurrentAnimatorStateInfo(0).IsName("JumpRise") ||
      baseAnim.GetCurrentAnimatorStateInfo(0).IsName("JumpFall");


    float h = input.GetHorizontalAxis ();
    float v = input.GetVerticalAxis ();
    bool jump = input.GetJumpButtonDown();
    bool attack = input.GetAttackButtonDown();

    currentDir = new Vector3(h, 0, v);
    currentDir.Normalize();

    if (!isAttackingAnim) {
      if ((v == 0 && h == 0)) {
        Stop ();
        isMoving = false;
      } else if (!isMoving && (v != 0 || h != 0)) {
        isMoving = true;
        float dotProduct = Vector3.Dot (currentDir, lastWalkVector);
        if (canRun && Time.time < lastWalk + tapAgainToRunTime && dotProduct > 0) {
          Run ();
        } else {
          Walk ();
          if (h != 0) {
            lastWalkVector = currentDir;
            lastWalk = Time.time;
          }
        }
      }
    }


    //Calls jump when:
    //jump is true, we aren't landing, we aren't attacking
    //AND
      //we are on the ground
      //OR 
      //we are mid jump

        if (jump && !isJumpLandAnim && !isAttackingAnim &&
    (onGround || (isJumpingAnim && Time.time < lastJumpTime + //I DONT THINK THE FINAL CONDITION WORKS. SHOULD EVALUATE TO FALSE WHILE MIDAIR?
    jumpDuration)) ) {
      //sound fx
          if(jumpSound){
            AudioManager.instance.Play("Jump");
            jumpSound=false;
          }

      Jump(currentDir);
    }

    if (attack && Time.time >= lastAttackTime + attackLimit) {
      lastAttackTime = Time.time;
      Attack();
    }
  }

  public void Stop() {
    speed = 0;
    baseAnim.SetFloat("Speed", speed);
    isRunning = false;
    baseAnim.SetBool("IsRunning", isRunning);
  }

  public void Walk() {
    speed = walkSpeed;
    baseAnim.SetFloat("Speed", speed);
    isRunning = false;
    baseAnim.SetBool("IsRunning", isRunning);
  }


  void FixedUpdate() {
    Vector3 moveVector = currentDir * speed;
    if(onGround && !isAttackingAnim){
      body.MovePosition (transform.position + moveVector * Time.fixedDeltaTime);
      baseAnim.SetFloat ("Speed", moveVector.magnitude);
    }

    if (moveVector != Vector3.zero) {
      if (moveVector.x != 0) {
        isFacingLeft = moveVector.x < 0;
      }
      FlipSprite (isFacingLeft);
    }
  }

  public void Run() {
    speed = runSpeed;
    isRunning = true;
    baseAnim.SetBool("IsRunning", isRunning);
    baseAnim.SetFloat("Speed", speed);
  }

  void Jump(Vector3 direction) {
    if (!isJumpingAnim) {
      baseAnim.SetTrigger ("Jump");
      lastJumpTime = Time.time;

      Vector3 horizontalVector = new Vector3(direction.x, 0, direction.z) * speed * 40;
      body.AddForce(horizontalVector,ForceMode.Force);
    }

    Vector3 verticalVector = Vector3.up * jumpForce * Time.deltaTime;
    body.AddForce(verticalVector,ForceMode.Force);


  }

  protected override void Landed()
  {
    jumpSound=true;
    base.Landed();
    Walk();
  }

  public override void Attack() {

  }

}
