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
  private float distCameraToPlayer;

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
    distCameraToPlayer = 0;
    cameraBounds.SetXPosition(cameraBounds.minVisibleX);
    currentCameraTrans = cameraBounds.cameraRoot;
    dialogueBar.SetActive(false);
    SetScreenColliders(false);    
  }
//3
  void Update() {

    distCameraToPlayer = (currentCameraTrans.position.x - actor.transform.position.x);
    Debug.Log("distCameraToPlayer" + distCameraToPlayer);


    if (cameraFollows) {
      cameraBounds.SetXPosition(actor.transform.position.x);
    } else if (cameraPanning){
      Debug.Log("panning by " + (distCameraToPlayer/50));
      cameraBounds.SetXPosition(currentCameraTrans.position.x - (distCameraToPlayer/100));
    }

    //cameraFollows reset when camera lands on player
    if(Mathf.Abs(distCameraToPlayer) < 0.1 && cameraPanning){
      Debug.Log("setting follows to true");
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
