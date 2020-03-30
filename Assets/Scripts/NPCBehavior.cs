using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    Hero script;
    Vector3 transform;
    GameObject player;
    Animator animator;
    void Start(){
      player = GameObject.Find("Player");
      script = player.GetComponent<Hero>();
      animator = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
      transform = gameObject.transform.position;
      float playerX = player.transform.position.x;
      if (script.engaged.Count > 0){
        if (transform.x < playerX && transform.x > playerX-10){
          animator.SetBool("Running", true);
          transform.x -= 0.04f;
          if (transform.x < playerX-10){
            Destroy(gameObject);
          }
        }
        else if(transform.x > playerX && transform.x < playerX+10){
          animator.SetBool("Running", true);
          transform.x += 0.04f;
          if (transform.x > playerX+10){
            Destroy(gameObject);
          }
        }
        gameObject.transform.position = transform;
      }
    }
}
