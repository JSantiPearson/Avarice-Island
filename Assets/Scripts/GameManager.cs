using UnityEngine;
public class GameManager : MonoBehaviour {
  //1
  public Hero actor;
  public bool cameraFollows = true;
  public CameraBounds cameraBounds;
  public GameObject enemyPrefab;
//2
  void Start() {
    cameraBounds.SetXPosition(cameraBounds.minVisibleX);
    for(int i=0;i<3;i++){
      Instantiate(enemyPrefab);
    }
  }
//3
void Update() {
    if (cameraFollows) {
      cameraBounds.SetXPosition(actor.transform.position.x);
    }

    //we can check how many enemies are left by finding all objects tagged enemy
    //MAKE SURE ALL ENEMIES ARE INSTANTIATED WITH 'Enemy' TAGS
    Debug.Log("Enemies: " + (GameObject.FindGameObjectsWithTag("Enemy")).Length);
  }
  
public void LockCamera(){
  cameraFollows=false;
}

public void unlockCamera(){
  cameraFollows=true;
}
}
