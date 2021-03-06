using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHitboxes : MonoBehaviour
{
    bool m_Started;
    public LayerMask m_LayerMask;
    public GameObject thisBox;
    public int damage;
    public bool knockback;
    public HashSet<GameObject> beenHit = new HashSet<GameObject>();

    private const string GRUNT = "EnemyGrunt(Clone)";
    private const string RAVE_BOY = "RaveBoy(Clone)";
    private const string RAVE_GIRL = "RaveGirl(Clone)";
    private const string BOUNCER_REX = "BouncerRex(Clone)";
    private const string BOUNCER_BRAD = "BouncerBrad(Clone)";
    private const string BLASTER = "Blaster(Clone)";
    private const string BRAWLER = "Brawler(Clone)";
    private const string HAN_LAO = "HanLao(Clone)";
    private const string SHEN = "Shen(Clone)";
    private const string SHEN2 = "Shen";

    void Start()
    {
        //Use this to ensure that the Gizmos are being drawn when in Play Mode.
        m_Started = true;
        SetActive(false, false, 0);
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
                //switch statement of all enemy types
                switch (enemy.name)
                {
                    case GRUNT:
                        if (knockback)
                        {
                            enemy.GetComponent<Enemy>().Launch(GameObject.Find("Player").transform.position);

                        }
                        enemy.GetComponent<EnemyGrunt>().Hit(damage, knockback);
                        break;
                    case BRAWLER:
                        if (knockback)
                        {
                            enemy.GetComponent<Enemy>().Launch(GameObject.Find("Player").transform.position);
                        }
                        enemy.GetComponent<Brawler>().Hit(damage, knockback);
                        break;
                    case BLASTER:
                        if (knockback)
                        {
                            enemy.GetComponent<Enemy>().Launch(GameObject.Find("Player").transform.position);

                        }
                        enemy.GetComponent<Blaster>().Hit(damage, knockback);
                        break;
                    case RAVE_BOY:
                        if (knockback)
                        {
                            enemy.GetComponent<Enemy>().Launch(GameObject.Find("Player").transform.position);

                        }
                        enemy.GetComponent<RaveBoy>().Hit(damage, knockback);
                        break;
                    case RAVE_GIRL:
                        if (knockback)
                        {
                            enemy.GetComponent<Enemy>().Launch(GameObject.Find("Player").transform.position);

                        }
                        enemy.GetComponent<RaveGirl>().Hit(damage, knockback);
                        break;
                    case BOUNCER_BRAD:
                        if (knockback)
                        {
                            enemy.GetComponent<Enemy>().Launch(GameObject.Find("Player").transform.position);

                        }
                        enemy.GetComponent<Bouncer>().Hit(damage, knockback);
                        break;
                    case BOUNCER_REX:
                        if (knockback)
                        {
                            enemy.GetComponent<Enemy>().Launch(GameObject.Find("Player").transform.position);

                        }
                        enemy.GetComponent<Bouncer>().Hit(damage, knockback);
                        break;
                    case HAN_LAO:
                        enemy.GetComponent<HanLao>().Hit(damage, knockback);
                        break;
                    case SHEN:
                        enemy.GetComponent<Shen>().Hit(damage, knockback);
                        break;
                    case SHEN2:
                        enemy.GetComponent<Shen>().Hit(damage, knockback);
                        break;
                    default:
                        break;
                }
                //add enemy to beenHit hashmap
                beenHit.Add(enemy);
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

    //activate hitboxes from the animator
    public void SetActive(bool isActive, bool launch, int damage)
    {
        this.knockback = launch;
        this.damage = damage;
        thisBox.SetActive(isActive);
        beenHit.Clear();
    }
}
