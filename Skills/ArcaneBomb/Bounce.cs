using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

    public float range = 10f;
    public int bounceCount = 3;
    public int bounceLeft;

    private void Start()
    {
        bounceLeft = bounceCount;
    }

    public void DoBounce(Collider2D col)
    {
        if (bounceLeft == 0)
        {
            PoolManager.Despawn(gameObject);
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);

        if (hits.Length == 0)
        {
            PoolManager.Despawn(gameObject);
        }

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].tag == "Enemy" && hits[i].gameObject != col.gameObject)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().AddForce((hits[i].transform.position - transform.position).normalized * ArcaneBomb.singleton.speed);
                bounceLeft--;
                return;
            }
        }

        if(gameObject.activeInHierarchy)
        PoolManager.Despawn(gameObject);
    }

    private void OnDisable()
    {
        bounceLeft = bounceCount;
    }
}
