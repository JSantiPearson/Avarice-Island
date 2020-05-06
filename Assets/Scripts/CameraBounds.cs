using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class CameraBounds : MonoBehaviour {

  public float minVisibleX; //static left screen buffer, currently 5
  public float maxVisibleX; //static right screen buffer, currently 96
  public float minXValue, minYValue; //left camera position value for camera clamp
  public float maxXValue, maxYValue; //right camera position value for camera clamp
  public float cameraHalfWidth; //the halfway position of camera, between min and max
  private static float HALF = 0.5f;

  private Camera activeCamera;

  Vector3 position;
  public Transform cameraRoot;
  public Transform leftBounds;
  public Transform rightBounds;

  public float leftScreenBound;
  public float rightScreenBound;

  void Start() {

    activeCamera = Camera.main;

    leftScreenBound = activeCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
    rightScreenBound = activeCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

    cameraHalfWidth = Mathf.Abs( leftScreenBound - rightScreenBound ) * HALF;

    position = leftBounds.transform.localPosition;
    position.x = transform.localPosition.x - cameraHalfWidth;
    leftBounds.transform.localPosition = position;
    position = rightBounds.transform.localPosition;
    position.x = transform.localPosition.x + cameraHalfWidth;
    rightBounds.transform.localPosition = position;

}

void Update(){
  if(SceneManager.GetActiveScene().name.Equals("Docks")){
    maxXValue = 27.4f;
  }
}

  /**
  * Sets the camera position every frame
  **/
  public void SetXPosition(float x) {
    Vector3 trans = cameraRoot.position;
    trans.x = Mathf.Clamp(x, minXValue, maxXValue);
    cameraRoot.position = trans;
  }

  public void SetYPosition(float y) {
    Vector3 trans = cameraRoot.position;
    trans.y = Mathf.Clamp(y, minYValue, maxYValue);
    cameraRoot.position = trans;
  }


}
