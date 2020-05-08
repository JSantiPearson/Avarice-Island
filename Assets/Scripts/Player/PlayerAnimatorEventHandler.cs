using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEventHandler : MonoBehaviour
{
    public GameObject ElectroBall;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Fever Attacks
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public void activateFeverLightning(int activate)
    {
        transform.GetChild(6).GetComponent<feverHitboxes>().SetActive(activate != 0, false, 15);
    }
    public void activateFeverAoe(int activate)
    {
        transform.GetChild(7).GetComponent<feverHitboxes>().SetActive(activate != 0, true, 30);
    }
    public void activateFeverProjectile(int activate)
    {
        transform.GetChild(8).GetComponent<feverHitboxes>().SetActive(activate != 0, false, 15);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Physical Attacks
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public void activatePlayerPunch(int activate)
    {
        transform.GetChild(1).GetComponent<playerHitboxes>().SetActive(activate != 0, false, 10);
        transform.parent.GetComponent<PlayerCombat>().isAttacking(activate != 0);
    }
    public void activatePlayerKick(int activate)
    {
        transform.GetChild(2).GetComponent<playerHitboxes>().SetActive(activate != 0, false, 10);
    }
    public void activatePlayerRoundhouse(int activate)
    {
        transform.GetChild(3).GetComponent<playerHitboxes>().SetActive(activate != 0, false, 10);
    }
    public void activatePlayerUppercut(int activate)
    {
        transform.GetChild(4).GetComponent<playerHitboxes>().SetActive(activate != 0, true, 15);
    }
    public void activatePlayerDashAttack(int activate)
    {
        transform.GetChild(5).GetComponent<playerHitboxes>().SetActive(activate != 0, true, 20);
        transform.parent.GetComponent<PlayerCombat>().isAttacking(activate != 0);
    }
    public void noLongerAttacking(int activate)
    {
        transform.parent.GetComponent<PlayerCombat>().isAttacking(activate != 0);

    }


    public void activateHitboxReset()
    {
        transform.GetChild(1).GetComponent<playerHitboxes>().SetActive(false, false, 10);
        transform.GetChild(2).GetComponent<playerHitboxes>().SetActive(false, false, 10);
        transform.GetChild(3).GetComponent<playerHitboxes>().SetActive(false, false, 10);
        transform.GetChild(4).GetComponent<playerHitboxes>().SetActive(false, true, 15);
        transform.GetChild(5).GetComponent<playerHitboxes>().SetActive(false, true, 20);
        transform.GetChild(6).GetComponent<playerHitboxes>().SetActive(false, false, 15);
        transform.GetChild(7).GetComponent<playerHitboxes>().SetActive(false, true, 30);
        transform.parent.GetComponent<PlayerCombat>().isAttacking(false);
    }

}
