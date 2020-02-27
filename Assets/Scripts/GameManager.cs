using UnityEngine;
using System.Collections;
public class GameManager : MonoBehaviour {
  //1
  public Hero actor;
  public bool cameraFollows = true;
  public CameraBounds cameraBounds;
  public GameObject enemyPrefab;
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
    dialogueBar = GameObject.Find("DialogueBar");
    leftScreenCollider = GameObject.Find("LeftCamBounds");
    rightScreenCollider = GameObject.Find("RightCamBounds");

    //initialize objects
    cameraBounds.SetXPosition(cameraBounds.minVisibleX);
    dialogueBar.SetActive(false);
    SetScreenColliders(false);    
  }
//3
  void Update() {
    if (cameraFollows) {
      cameraBounds.SetXPosition(actor.transform.position.x);
    }

    //we can check how many enemies are left by finding all objects tagged enemy
    //MAKE SURE ALL ENEMIES ARE INSTANTIATED WITH 'Enemy' TAGS
    Debug.Log("Enemies: " + (GameObject.FindGameObjectsWithTag("Enemy")).Length);

    //test for interval spawns

    /*

    timeElapsed+=Time.deltaTime;
    if(okToSpawn()){
      Instantiate(enemyPrefab);
    }

    */

  }
  
  public void LockCamera(){
   cameraFollows=false;
  }


  public void UnlockCamera(){
    cameraFollows=true;
  }

  public void SetScreenColliders(bool active){
    leftScreenCollider.SetActive(active);
    rightScreenCollider.SetActive(active);
  }

  //NEED TO MOVE FOLLOWING METHODS EVENTUALLY. NEED A SMART SPAWNING SCHEME
  /*

  public bool okToSpawn(){
    if(timeElapsed>spawnInterval && GameObject.FindGameObjectsWithTag("Enemy").Length<maxEnemies){
      timeElapsed=0;
      return true;
    } else {
      return false;
    }
  } 
  /**
  IEnumerator SpawnAndWait(){
    Instantiate(enemyPrefab);
    yield return new WaitForSeconds(2);
  }

  IEnumerator SpawnEnemies(int numEnemies){
    for(int i=0;i<numEnemies;i++){
      StartCoroutine("SpawnAndWait");
    }
  */
}
