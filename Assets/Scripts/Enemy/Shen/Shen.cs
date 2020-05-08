using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shen : Actor

{
    public float maxHealth = 150;
    public float currentHealth;

    public int comboCounter;
    public float comboTimeLimit;
    private float comboTimer;

    private float lightningTimer;
    public float lightningTimeLimit;
    


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        comboCounter = 0;
        comboTimer = comboTimeLimit;

        lightningTimer = lightningTimeLimit;

        paused = false;
        GameManager.bossFightInProgress = true; //tells audio manager to switch songs
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        baseAnim.setFloat("Speed", body.transform.velocity);

        // Manage the lightning timer
        lightningTimer -= Time.deltaTime;
        if(lightningTimer <= 0)
        {
            lightningTimer = lightningTimeLimit;
            baseAnim.setTrigger("Lightning");
        }

        // Manage the combo counter
        comboTimer -= Time.deltaTime;
        if(comboTimer <= 0)
        {
            comboTimer = comboTimeLimit;
            comboCounter = 0;
        }
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;
        baseAnim.setTrigger("Hit");
        comboCounter = 0;
    }

    public void Launch(int damage)
    {

    }

    public void Zap(int damage)
    {

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Unity Game Physics
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    void FixedUpdate()
    {
        //pause logic //STILL GLITCHY DURING A JUMP
        if (!GameManager.enemiesOn)
        {
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;

            //body.constraints = RigidbodyConstraints.FreezeAll; possible better fix
            return;
        }
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
