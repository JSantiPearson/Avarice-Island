using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverAttackCollision : MonoBehaviour
{

    public Collider collider;
    public LayerMask enemyLayers;

    public int damage = 50;



    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider hitInfo)
    {
        //EnemyGrunt enemy = hitInfo.GetComponent<EnemyGrunt>();


        Collider[] hitEnemies = Physics.OverlapBox(hitInfo.bounds.center, hitInfo.bounds.extents, hitInfo.transform.rotation, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyGrunt>().Launch(GameObject.Find("Player").transform.position);
            enemy.GetComponent<EnemyGrunt>().Hurt(30);
            Debug.Log("We Dash Attacked " + enemy.name);

        }

        Debug.Log(hitInfo.name);


        Destroy(gameObject);

    }

    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }

}
