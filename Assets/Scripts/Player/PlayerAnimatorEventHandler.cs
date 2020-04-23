using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEventHandler : MonoBehaviour
{
    public GameObject ElectroBall;

    public void activateFeverAoe(int activate)
    {
        transform.GetChild(5).GetComponent<feverHitboxes>().SetActive(activate != 0);
    }

    public void activateFeverLightning(int activate)
    {
        transform.GetChild(4).GetComponent<feverHitboxes>().SetActive(activate != 0);

    }
    public void activateIsAttacking(int activate)
    {
        transform.parent.GetComponent<PlayerCombat>().isAttacking(activate != 0);
    }
    public void activatePlayerDashAttack(int activate)
    {
        transform.GetChild(3).GetComponent<feverHitboxes>().SetActive(activate != 0);

    }
    public void activateFeverProjectile(int activate)
    {
        transform.GetChild(3).GetComponent<feverHitboxes>().SetActive(activate != 0);

    }
}
