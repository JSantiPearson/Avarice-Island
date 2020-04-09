using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    Hero script;
    Vector3 transform;
    GameObject player;
    GameObject manager;
    Animator animator;
    float cameraHalfWidth;
    Vector3 camera;
    float leftCamBound;
    float rightCamBound;

    IEnumerator Start(){
      yield return new WaitForSeconds(0.2f);
      player = GameObject.Find("Player");
      manager = GameObject.Find("MyGameManager");
      script = player.GetComponent<Hero>();
      animator = gameObject.GetComponent<Animator>();
      cameraHalfWidth = Camera.main.GetComponent<CameraBounds>().cameraHalfWidth;
      Debug.Log(cameraHalfWidth);
    }
    // Update is called once per frame
    void Update()
    {
      bool locked = !manager.GetComponent<GameManager>().cameraFollows;
      camera = Camera.main.transform.position;
      leftCamBound = camera.x - cameraHalfWidth;
      rightCamBound = camera.x + cameraHalfWidth;

      transform = gameObject.transform.position;
      float playerX = player.transform.position.x;
      if (script.engaged.Count > 0 && locked){
        if (transform.x < camera.x && transform.x > leftCamBound-1){
          Debug.Log("Running away");
          animator.SetBool("Running", true);
          transform.x -= 0.06f;
          if (transform.x <= leftCamBound-1){
            Destroy(gameObject);
          }
        }
        else if(transform.x >= camera.x && transform.x < rightCamBound+1){
          animator.SetBool("Running", true);
          transform.x += 0.06f;
          if (transform.x >= rightCamBound+1){
            Destroy(gameObject);
          }
        }
        gameObject.transform.position = transform;
      }
    }
}
