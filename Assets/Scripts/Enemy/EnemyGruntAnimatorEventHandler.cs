using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGruntAnimatorEventHandler : MonoBehaviour
{

    public void activateHitBox1(int activate)
    {
        transform.parent.GetChild(2).GetComponent<Hitbox>().SetActive(activate != 0);
    }

}
