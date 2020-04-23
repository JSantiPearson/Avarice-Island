using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerEventHandler : MonoBehaviour
{

    public void activateAttack1(int activate)
    {
        transform.parent.GetChild(2).GetComponent<Hitbox>().SetActive(activate != 0);
    }

    public void activateAttack2(int activate)
    {
        transform.parent.GetChild(3).GetComponent<Hitbox>().SetActive(activate != 0);
    }

    public void activateAttack3(int activate)
    {
        transform.parent.GetChild(4).GetComponent<Hitbox>().SetActive(activate != 0);
    }

    public void activateAttack4(int activate)
    {
        transform.parent.GetChild(5).GetComponent<Hitbox>().SetActive(activate != 0);
    }

}
