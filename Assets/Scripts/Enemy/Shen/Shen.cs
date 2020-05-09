using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shen : Actor

{
    public float maxHealth = 150f;
    public float currentHealth;

    public int comboCounter;
    public float comboTimeLimit;
    private float comboTimer;

    public float lightningTimer;
    public float lightningTimeLimit = 30.0f;

    public GameObject hitEffectPrefab;
    public bool paused;

    public bool killTest;

    protected float launchForce = 250f;

    public AudioManager audioManager;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        comboCounter = 0;
        comboTimer = comboTimeLimit;

        lightningTimer = lightningTimeLimit;

        GameManager.bossFightInProgress = true; //tells audio manager to switch songs
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pause logic
        if(GameManager.enemiesOn && paused){ //unpause
            paused = false;
            //body.Sleep();
            baseAnim.enabled = true;
        }

        if(!GameManager.enemiesOn && !paused){ //disable animator on first pass after pause
            paused=true;
            baseAnim.enabled=false;
            //body.WakeUp();
            return;
        } else if (paused){ //just skip update
            return;
        }

        // Manage the lightning timer
        lightningTimer -= Time.deltaTime;
        if(lightningTimer <= 0)
        {
            lightningTimer = lightningTimeLimit;
            baseAnim.SetTrigger("Lightning");
        }

        // Manage the combo counter
        comboTimer -= Time.deltaTime;
        if(comboTimer <= 0)
        {
            comboTimer = comboTimeLimit;
            comboCounter = 0;
        }


         if(killTest){
            Hit(maxHealth, false);
            killTest = false;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        if (currentHealth <= 0)
        {
            baseAnim.SetTrigger("Defeated");
        }
    }

    public void Hit(float damage, bool knockback)
    {
        TakeDamage(damage);
        if (!knockback){
          baseAnim.SetTrigger("Hit");
        }
        comboCounter = 0;

        audioManager.PlayOneShot("hitSound", 0.2f);
        Instantiate(hitEffectPrefab, this.transform.position, this.transform.rotation);
    }

    public void Launch(Vector3 attackerLocation)
    {

        baseAnim.SetTrigger("Launch");

        Vector3 horizontalLaunchInfluence = body.position - attackerLocation;
        horizontalLaunchInfluence.Normalize();
        horizontalLaunchInfluence = horizontalLaunchInfluence * launchForce / 2;
        body.AddForce(horizontalLaunchInfluence, ForceMode.Force);

        Vector3 verticalLaunchInfluence = Vector3.up * launchForce;
        body.AddForce(verticalLaunchInfluence, ForceMode.Force);
    }

    public void Zap(float damage)
    {

        TakeDamage(damage);

        audioManager.PlayOneShot("hitSound", 0.2f);
        Instantiate(hitEffectPrefab, this.transform.position, this.transform.rotation);
        baseAnim.SetTrigger("Zap");
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
        if(collision.collider.tag == "Floor")
        {
            baseAnim.SetBool("onGround", false);
        }
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            baseAnim.SetBool("onGround", true);
        }
    }


    public void Die()
    {
        StartCoroutine(WaitAndDie(1.5f));
    }

    IEnumerator WaitAndDie(float time)
    {
        yield return new WaitForSeconds(time);
        //Debug.Log("About to destroy han");
        Destroy(gameObject);
    }
}
