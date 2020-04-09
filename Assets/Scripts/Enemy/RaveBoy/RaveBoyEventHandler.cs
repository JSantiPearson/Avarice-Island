using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaveBoyEventHandler : MonoBehaviour
{

    public void activateFlashHitBox(int activate)
    {
        transform.parent.GetChild(2).GetComponent<Hitbox>().SetActive(activate != 0);
    }

}
