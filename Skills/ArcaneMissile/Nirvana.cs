using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nirvana : MonoBehaviour {
    
    public void OnEnemyHit(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Arcane.singleton.Add(1);
        }
    }

    void OnDisable()
    {
        Destroy(this);
    }
}
