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

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
    }
}
