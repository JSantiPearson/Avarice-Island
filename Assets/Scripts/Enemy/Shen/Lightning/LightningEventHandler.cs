﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEventHandler : MonoBehaviour
{
    public void scaleLightning()
    {
        Vector3 pos = gameObject.transform.position;
        pos.y = pos.y + 3.8f;
        gameObject.transform.localScale = new Vector3(1.7f, 1.7f, 1);
        gameObject.transform.position = pos;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 5;
    }

    public void activateHitBox(int activate)
    {
        transform.GetChild(0).GetComponent<Hitbox>().SetActive(activate != 0);
    }
}
