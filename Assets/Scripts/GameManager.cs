using UnityEngine;
using System.Collections;
public class GameManager : MonoBehaviour {
  //1
  public Hero actor;
  public bool cameraFollows = true;
  public bool cameraPanning = false;
  public CameraBounds cameraBounds;
  public GameObject enemyPrefab;
  public GameObject dialogueBar;
  public GameObject leftScreenCollider;
  public GameObject rightScreenCollider;
  private Transform currentCameraTrans;
  private float lastDistCamToPlayer;
  private float currDistCamToPlayer;

  //MOVE THESE ELSEWHERE WHEN SPAWNING IS REFACTORED
  /*
  int maxEnemies=5;
  public float spawnInterval=2;
  public float timeElapsed=0;
  */

//2
  void Start() {

    //find game objects we need to reference
    dialogueBar = GameObject.Find("DialogueBar");
    leftScreenCollider = GameObject.Find("LeftCamBounds");
    rightScreenCollider = GameObject.Find("RightCamBounds");

    //initialize objects
    currDistCamToPlayer = 0;
    cameraBounds.SetXPosition(cameraBounds.minVisibleX);
    currentCameraTrans = cameraBounds.cameraRoot;
    dialogueBar.SetActive(false);
    SetScreenColliders(false);    
  }
//3
  void Update() {

    currDistCamToPlayer = (currentCameraTrans.position.x - actor.transform.position.x);

    //on an unlock, we pan back to the player
    if (cameraFollows) {
      cameraBounds.SetXPosition(actor.transform.position.x);
    } else if (cameraPanning){
      cameraBounds.SetXPosition(currentCameraTrans.position.x - (lastDistCamToPlayer/50));
    } else {
        //only update this value when camera is locked and not panning.
        lastDistCamToPlayer = (currentCameraTrans.position.x - actor.transform.position.x);
    }

    //cameraFollows reset when camera lands on player
    if(Mathf.Abs(currDistCamToPlayer) < 0.1 && cameraPanning){
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
