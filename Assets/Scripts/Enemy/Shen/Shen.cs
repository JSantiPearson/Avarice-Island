using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shen : Actor

{
    public float maxHealth = 150;
    public float currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentPhase = 1;
        paused = false;
        GameManager.bossFightInProgress = true; //tells audio manager to switch songs
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        baseAnim.setFloat("Speed", body.transform.velocity);
    }

    public void Hit(int damage)
    {

    }

    public void Zap(int damage)
    {

    }


    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            baseAnim.setBool("onGround", false);
        }
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            baseAnim.setBool("onGround", true);
        }
    }
}
