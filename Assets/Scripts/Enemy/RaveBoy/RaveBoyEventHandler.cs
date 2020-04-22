using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaveBoyEventHandler : MonoBehaviour
{

    public void activatePunch1(int activate)
    {
        transform.parent.GetChild(2).GetComponent<Hitbox>().SetActive(activate != 0);
    }

    public void activatePunch2(int activate)
    {
        transform.parent.GetChild(3).GetComponent<Hitbox>().SetActive(activate != 0);
    }

    public void activateKick1(int activate)
    {
        transform.parent.GetChild(3).GetComponent<Hitbox>().SetActive(activate != 0);
    }

    public void activateFlash(int activate)
    {
        transform.parent.GetChild(4).GetComponent<Hitbox>().SetActive(activate != 0);
    }

}
