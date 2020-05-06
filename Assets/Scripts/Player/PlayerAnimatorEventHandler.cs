using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEventHandler : MonoBehaviour
{
    public GameObject ElectroBall;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Fever Attacks
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public void activateFeverAoe(int activate)
    {
        transform.GetChild(5).GetComponent<feverHitboxes>().SetActive(activate != 0, true, 30);
    }

    public void activateFeverLightning(int activate)
    {
        transform.GetChild(4).GetComponent<feverHitboxes>().SetActive(activate != 0, false, 15);
    }

    public void activateFeverProjectile(int activate)
    {
        transform.GetChild(3).GetComponent<feverHitboxes>().SetActive(activate != 0, false, 15);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Physical Attacks
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public void activatePlayerDashAttack(int activate)
    {
        transform.GetChild(3).GetComponent<playerHitboxes>().SetActive(activate != 0, true, 20);
        transform.parent.GetComponent<PlayerCombat>().isAttacking(activate != 0);
    }
    public void activatePlayerPunch(int activate)
    {
        transform.GetChild(1).GetComponent<playerHitboxes>().SetActive(activate != 0, false, 10);
        transform.parent.GetComponent<PlayerCombat>().isAttacking(activate != 0);
    }
    public void activatePlayerKick(int activate)
    {
        transform.GetChild(2).GetComponent<playerHitboxes>().SetActive(activate != 0, false, 10);
        transform.parent.GetComponent<PlayerCombat>().isAttacking(activate != 0);
    }
    public void activatePlayerFinisher(int activate)
    {
        transform.GetChild(1).GetComponent<playerHitboxes>().SetActive(activate != 0, true, 15);
        transform.parent.GetComponent<PlayerCombat>().isAttacking(activate != 0);
    }
    
}
