using UnityEngine;
using System.Collections;
public class GameManager : MonoBehaviour {

  //TODO: Refactor Camera Stuff? or just rename this file and refactor noncamera stuff. 


  //1
  public Hero actor;
  public bool cameraFollows = true;
  public bool cameraPanning = false;
  public CameraBounds cameraBounds;
  public GameObject enemyPrefab;
  private Transform currentCameraTrans;
  private float lastDistCamToPlayer;
  private float currDistCamToPlayer;
  private const float panInterval = 0.15f;

  //noncamera vars
  public GameObject dialogueBar;
  public GameObject leftScreenCollider;
  public GameObject rightScreenCollider;

  //MOVE THESE ELSEWHERE WHEN SPAWNING IS REFACTORED
  /*
  int maxEnemies=5;
  public float spawnInterval=2;
  public float timeElapsed=0;
  */

//2
  void Start() {

    //find game objects we need to reference
    //dialogueBar = GameObject.Find("DialogueBar");
    leftScreenCollider = GameObject.Find("LeftCamBounds");
    rightScreenCollider = GameObject.Find("RightCamBounds");

    //initialize values and objects
    currDistCamToPlayer = 0;
    cameraBounds.SetXPosition(cameraBounds.minVisibleX);
    currentCameraTrans = cameraBounds.cameraRoot;
    //dialogueBar.SetActive(false); //TESTING ANIMATOR VERSION
    SetScreenColliders(false);    
  }
//3
  void Update() {

    currDistCamToPlayer = (currentCameraTrans.position.x - actor.transform.position.x);

    //on an unlock, we pan back to the player
    if (cameraFollows) {
      cameraBounds.SetXPosition(actor.transform.position.x);
      //set y value to player's z because of tilted screen. not an exact correlation but close.
      cameraBounds.SetYPosition(actor.transform.position.z);
    } else if (cameraPanning){
      //cameraBounds.SetXPosition(currentCameraTrans.position.x - (lastDistCamToPlayer/50)); //this version is based on player location
      if(lastDistCamToPlayer > 0){
        cameraBounds.SetXPosition(currentCameraTrans.position.x - panInterval); //pan by 0.5 each frame (NEED TO REMOVE HARDCODE)
      } else if (lastDistCamToPlayer < 0) {
        cameraBounds.SetXPosition(currentCameraTrans.position.x + panInterval);
      }

    } else {
        //locked in x direction only
        lastDistCamToPlayer = (currentCameraTrans.position.x - actor.transform.position.x);
        cameraBounds.SetYPosition(actor.transform.position.z);

    }

    //cameraFollows reset when camera lands on player
    if(Mathf.Abs(currDistCamToPlayer) < 0.2 && cameraPanning){
      cameraFollows = true;
      cameraPanning = false;
    }

  }
  
  public void LockCamera(){
   cameraFollows=false;
  }


  public void UnlockCamera(){
    //cameraFollows=true;
    cameraPanning = true;
  }

  public void SetScreenColliders(bool active){
    leftScreenCollider.SetActive(active);
    rightScreenCollider.SetActive(active);
  }

}
