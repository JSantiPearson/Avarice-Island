using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShenEventHandler : MonoBehaviour
{

    public void activatePunchHitBox(int activate)
    {
        transform.parent.GetChild(2).GetComponent<Hitbox>().SetActive(activate != 0);
    }
    public void activateKickHitBox(int activate)
    {
        transform.parent.GetChild(3).GetComponent<Hitbox>().SetActive(activate != 0);
    }
    public void activateAxeKickHitBox(int activate)
    {
        transform.parent.GetChild(4).GetComponent<Hitbox>().SetActive(activate != 0);
    }
    public void activateRoarHitBox(int activate)
    {
        transform.parent.GetChild(5).GetComponent<Hitbox>().SetActive(activate != 0);
    }
}
