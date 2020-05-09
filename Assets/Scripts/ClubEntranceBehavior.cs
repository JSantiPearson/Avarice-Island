using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubEntranceBehavior : MonoBehaviour
{
  public GameObject player;
  Vector3 playerPosition;
  SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.Find("Player");
      sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      playerPosition = player.transform.position;
      if (playerPosition.z > -1.05){
        sprite.sortingOrder = 6;
      }
      else {
        sprite.sortingOrder = 5;
      }
    }
}
