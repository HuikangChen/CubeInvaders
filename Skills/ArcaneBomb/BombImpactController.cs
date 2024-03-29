﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombImpactController : MonoBehaviour {

    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().TakeHealth(damage);
        }
    }
}
