using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventHandler : MonoBehaviour
{
    public GameObject knifeProjectile;
    public float knifeThrowLift;

    public GameObject spitProjectile;

    public void activateHitBox1(int activate)
    {
        transform.parent.GetChild(2).GetComponent<Hitbox>().SetActive(activate != 0);
    }

    public void activateHitBox2(int activate)
    {
        transform.parent.GetChild(3).GetComponent<Hitbox>().SetActive(activate != 0);
    }

    public void activateHitBox3(int activate)
    {
        transform.parent.GetChild(4).GetComponent<Hitbox>().SetActive(activate != 0);
    }

    public void activateHitBox4(int activate)
    {
        transform.parent.GetChild(5).GetComponent<Hitbox>().SetActive(activate != 0);
    }

    public void throwKnife()
    {
        Instantiate(knifeProjectile, transform.position, transform.rotation);
        transform.parent.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * knifeThrowLift);
    }

    public void spitAttack()
    {
        Instantiate(spitProjectile, transform.position - new Vector3(0, -1, 0), transform.rotation);
    }

}
