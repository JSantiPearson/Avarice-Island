using UnityEngine;
public class InputHandler : MonoBehaviour {
  float horizontal;
  float vertical;
  bool jump;
  float lastJumpTime;
  float lastAttackTime;
  bool isAttackingAnim;
  bool isJumping;
  bool attack;
  bool feverAttack;

public float maxAttackDuration = 0.2f;
  public float maxJumpDuration = 0.2f;

  public float GetVerticalAxis() {
    return vertical;
  }
  public float GetHorizontalAxis() {
    return horizontal;
  }
  public bool GetJumpButtonDown() {
    return jump;
  }
  public bool GetAttackButtonDown() {
    return attack;
  }
  //public bool GetFeverLightningButtonDown()
    //{  holding for later
      //  return feverLightning;
    //}
  public bool GetFeverAttackButtonDown(){
    return feverAttack;
  }

  void Update() {
    horizontal = Input.GetAxisRaw("Horizontal");
    vertical = Input.GetAxisRaw("Vertical");
    attack = Input.GetButtonDown("Attack");
  //  feverAttack = Input.GetButtonDown("FeverAttack");
//  attack = Input.GetButtonDown("Attack");


    if(!jump && !isJumping && Input.GetButton("Jump")) {
      jump = true;
      lastJumpTime = Time.time;
      isJumping = true;
    } else if(!Input.GetButton("Jump")) {
      jump = false;
      isJumping = false;
    }
    if(jump && Time.time > lastJumpTime + maxJumpDuration) {
      jump = false;
    }

  }
}
