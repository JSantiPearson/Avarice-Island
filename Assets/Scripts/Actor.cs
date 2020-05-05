using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
  public Animator baseAnim; //deals with animations
  public Rigidbody body; //for physics
  public SpriteRenderer shadowSprite; //renders the actor's shadow

  public float speed = 2; //the default speed at which the actor walks. Currently only in hero class.
  public float size = 1.5f;
  protected Vector3 frontVector; //for determining direction the actor is facing

  public bool onGround; //is the actor on the ground? true or false

    //game difficulty
    public enum DifficultyLevel
  {
    easy,
    medium,
    hard
  }

  public void Start(){
     //transform.localScale = new Vector3(size, size, 1);
  }

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

         //   transform.Rotate(0f, 180f, 0f);
     frontVector = new Vector3(-1, 0, 0);
      transform.localScale = new Vector3(-size, size, 1);
    } else {
            //transform.Rotate(0f, 180f, 0);
      frontVector = new Vector3(1, 0, 0);
      transform.localScale = new Vector3(size, size, 1);
    }
  }

  //STATIC VERSION OF METHOD ORIGINALLY DECLARED IN ENEMYGRUNT CLASS. should migrate to this one
  public static bool IsCloseTo(Vector3 target, Vector3 position, float interval)
    {
        float diffX = System.Math.Abs(target.x - position.x);
        float diffY = System.Math.Abs(target.y - position.y);
        float diffZ = System.Math.Abs(target.z - position.z);

        return diffX <= interval && diffY <= interval && diffZ <= interval;
    }
}
