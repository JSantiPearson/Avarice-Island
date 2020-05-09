using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanLao : Actor
{
	public float walkSpeed;
	public float runSpeed;
	public float direction;
    public float maxHealth = 150f;
    public float currentHealth;
    public int currentPhase;
    public GameObject hitEffectPrefab;
    public bool paused;

    public bool killTest;

    protected float launchForce = 250f;

    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        //body = gameObject.GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        currentPhase = 1;
        paused = false;
        GameManager.bossFightInProgress=true; //tells audio manager to switch songs
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        /*if (currentHealth <= 0)
        {
            baseAnim.SetTrigger("defeated");
        }*/

        //PAUSE LOGIC

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

        if(killTest){
            Hit(maxHealth, false);
            killTest = false;
        }
        if (currentHealth <= (maxHealth / 2) && currentPhase == 1)
        {
            currentPhase = 2;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        if (currentHealth <= 0)
        {
            baseAnim.SetTrigger("defeated");
        }
    }

    void FixedUpdate(){
        //pause logic //STILL GLITCHY DURING A JUMP
        if (!GameManager.enemiesOn) {
         body.velocity = Vector3.zero;
         body.angularVelocity = Vector3.zero;
         return;
        }
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

    public void Hit(float damage, bool knockback)
    {
        audioManager.PlayOneShot("hitSound",0.2f);
        TakeDamage(damage);
        Instantiate(hitEffectPrefab,this.transform.position,this.transform.rotation);
				if (!knockback){
					baseAnim.SetTrigger("hurt");
				}
    }

		public void Zap(float damage)
    {
        audioManager.PlayOneShot("hitSound",0.2f);
        TakeDamage(damage);
        Instantiate(hitEffectPrefab,this.transform.position,this.transform.rotation);
        baseAnim.SetTrigger("Zap");
    }

    /**
  * If actor exits collision with something, check if it's the ground. If it is, onGround is false.
  **/
    protected virtual void OnCollisionExit(Collision collision)
    {
        /*if(collision.collider.tag == "Hit")
        {
            Hurt(15);
            Debug.Log("got hit by electroball");
        }*/
    }

    public void Die(){
        StartCoroutine(WaitAndDie(1.5f));
    }

    IEnumerator WaitAndDie(float time){
        yield return new WaitForSeconds(time);
        //Debug.Log("About to destroy han");
        Destroy(gameObject);
    }
}
