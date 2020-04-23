using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    Hero script;
    Vector3 position;
    Vector3 localScale;
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
    }
    // Update is called once per frame
    void Update()
    {
      //this line throws an error because the manager has not been initialized yet.
      // TODO: Should we implement a loading screen?
      bool locked = !manager.GetComponent<GameManager>().cameraFollows;
      camera = Camera.main.transform.position;
      leftCamBound = camera.x - cameraHalfWidth;
      rightCamBound = camera.x + cameraHalfWidth;

      position = gameObject.transform.position;
      localScale = gameObject.transform.localScale;
      float playerX = player.transform.position.x;
      if (script.engaged.Count > 0 && locked){
        if (position.x < camera.x && position.x > leftCamBound-1){
          if (localScale.x < 0){
            localScale.x *= -1;
            gameObject.transform.localScale = localScale;
          }
          animator.SetBool("Running", true);
          position.x -= 0.06f;
          if (position.x <= leftCamBound-1){
            Destroy(gameObject);
          }
        }
        else if(position.x >= camera.x && position.x < rightCamBound+1){
          if (localScale.x > 0){
            localScale.x *= -1;
            gameObject.transform.localScale = localScale;
          }
          animator.SetBool("Running", true);
          position.x += 0.06f;
          if (position.x >= rightCamBound+1){
            Destroy(gameObject);
          }
        }
        gameObject.transform.position = position;
      }
    }
}
