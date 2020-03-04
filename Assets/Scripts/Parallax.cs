using UnityEngine;
using System.Collections;
public class Parallax : MonoBehaviour {
  public Transform[] layers;  // Array of layers
  private float[] parallaxMovement;  // How much each layer will be moved by parallax
  public float smoothness = 1f;  // Smoothness of movement
  private Transform camera;  // main camera
  private Vector3 previousCamPos;  // the position of the camera in the previous frame

  void Start () {
    camera = Camera.main.transform;
    previousCamPos = camera.position;
    parallaxMovement = new float[layers.Length];
    for (int i = 0; i < layers.Length; i++) {
      parallaxMovement[i] = i * 5; // 5 is the multiplier at which each layer will move. The further away the layer, the further it moves
    }
  }
  void Update () {
    // for each background
    for (int i = 0; i < layers.Length; i++) {
      // the parallax is the opposite of the camera movement, and is multiplied by which layer it is
      float parallax = (previousCamPos.x - camera.position.x) * parallaxMovement[i];
      // set a target x position for lerping
      float backgroundTargetPosX = layers[i].position.x + parallax;
      // create a target position which is the background's current position with it's target x position
      Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, layers[i].position.y, layers[i].position.z);
      // fade between current position and the target position using lerp
      layers[i].position = Vector3.Lerp (layers[i].position, backgroundTargetPos, smoothness * Time.deltaTime);
    }
      // set the previousCamPos to the camera's position at the end of the frame
      previousCamPos = camera.position;
  }
}
