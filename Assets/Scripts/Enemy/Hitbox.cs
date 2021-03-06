﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

  public enum HitboxType
  {
    light,
    heavy,
    flash,
    lightning_light,
    lightning_heavy
  }

    bool m_Started;
    public HitboxType type;
    public LayerMask m_LayerMask;
    public GameObject thisBox;
    public int damage;
    public HashSet<GameObject> beenHit = new HashSet<GameObject>();

    void Start()
    {
        //Use this to ensure that the Gizmos are being drawn when in Play Mode.
        m_Started = true;
        SetActive(false);
    }

    void FixedUpdate()
    {
        MyCollisions();
    }

    void MyCollisions()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask);
        //Collider[] hitEnemies = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, m_layerMask);
        int i = 0;
        //Check when there is a new collider coming into contact with the box
        foreach (Collider collider in hitColliders)
        {
            GameObject enemy = collider.gameObject;
            if (!beenHit.Contains(enemy))
            {
              if (type == HitboxType.light){
                enemy.GetComponent<Hero>().Hurt(damage);
                beenHit.Add(enemy);
              }
              else if (type == HitboxType.flash){
                enemy.GetComponent<Hero>().Stunned();
                beenHit.Add(enemy);
              }
              else if (type == HitboxType.heavy){ //TODO: Add i-frames while player is getting up?
                enemy.GetComponent<Hero>().Launch(damage);
                beenHit.Add(enemy);
              }
              else if (type == HitboxType.lightning_light){ //TODO: Add i-frames while player is getting up?
                enemy.GetComponent<Hero>().Zap(damage);
                beenHit.Add(enemy);
              }
              else if (type == HitboxType.lightning_heavy){ //TODO: Add i-frames while player is getting up?
                enemy.GetComponent<Hero>().Zap(damage);
                enemy.GetComponent<Hero>().Launch(0);
                beenHit.Add(enemy);
              }
            }
        }
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    public void SetActive(bool isActive)
    {
        thisBox.SetActive(isActive);
        beenHit.Clear();
    }
}
