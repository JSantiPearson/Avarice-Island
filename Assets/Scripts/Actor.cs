using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
  public Animator baseAnim; //deals with animations
  public Rigidbody body; //for physics
  public SpriteRenderer shadowSprite; //renders the actor's shadow

  public float speed = 2; //the default speed at which the actor walks. Currently only in hero class.
  protected Vector3 frontVector; //for determining direction the actor is facing

  public bool onGround; //is the actor on the ground? true or false

  /**
  * Updates the graphics every frame, moving actors and such. Position is determined through here.
  **/
  public virtual void Update() {
  Vector3 shadowSpritePosition = shadowSprite.transform.position;
  shadowSpritePosition.y = 1.8f; //set shadow starting position
  shadowSprite.transform.position = shadowSpritePosition; //update the shadow position
  }

  /**
  * If actor collides something, check if it's the ground. If it is, onGround is true.
  **/
  protected virtual void OnCollisionEnter(Collision collision) {
  if (collision.collider.tag == "Floor") {
    onGround = true;
    baseAnim.SetBool("onGround", onGround);
    Landed();
    }
  }

  /**
  * If actor exits collision with something, check if it's the ground. If it is, onGround is false.
  **/
  protected virtual void OnCollisionExit(Collision collision) {
  if (collision.collider.tag == "Floor") {
    onGround = false;
    baseAnim.SetBool("onGround", onGround);
    }
  }
  /**
  * Determines actor's state upon landing. In the case of the player, they enter into Walk().
  **/
  protected virtual void Landed(){
    
  }

  /**
  * Triggers the Attack state for the actor.
  **/
  public virtual void Attack() {
    baseAnim.SetTrigger("Attack");
  }

  /**
  * Flips the sprite horizontally for when the actor changes direction.
  **/
  public void FlipSprite(bool isFacingLeft) {
    if (isFacingLeft) {
      frontVector = new Vector3(-1, 0, 0);
      transform.localScale = new Vector3(-1.5f, 1.5f, 1);
    } else {
      frontVector = new Vector3(1, 0, 0);
      transform.localScale = new Vector3(1.5f, 1.5f, 1);
    }
  }
}
