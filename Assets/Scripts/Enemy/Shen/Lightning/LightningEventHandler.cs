using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEventHandler : MonoBehaviour
{
    public void activateHitBox(int activate)
    {
        transform.GetChild(0).GetComponent<Hitbox>().SetActive(activate != 0);
    }
}
