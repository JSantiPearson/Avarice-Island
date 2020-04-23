using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanLao : Actor
{
	public float walkSpeed;
	public float runSpeed;
	public float direction;
    public float maxHealth = 150;
    public float currentHealth;
    public int currentPhase;
    public GameObject hitEffectPrefab;

    public bool killTest;

    protected float launchForce = 250f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentPhase = 1;
}

    // Update is called once per frame
    void Update()
    {
        /*if (currentHealth <= 0)
        {
            baseAnim.SetTrigger("defeated");
        }*/
        if(killTest){
            Hurt(150);
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

    public void Hurt(float damage)
    {
        TakeDamage(damage);
        Instantiate(hitEffectPrefab,this.transform.position,this.transform.rotation);
        baseAnim.SetTrigger("hurt");
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
        Debug.Log("About to destroy han");
        Destroy(gameObject);
    }
}
