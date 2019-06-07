using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOT : MonoBehaviour
{
    public float damage;
    public float rate = .2f;
    float stamp;
    public float lifeTime = 8;
    float currentLife;
    public float range = 3f;

    private void OnEnable()
    {
        currentLife = lifeTime;
    }

    private void Update()
    {
        if (currentLife <= 0)
        {
            PoolManager.Despawn(gameObject);
        }

        currentLife -= Time.deltaTime;

        if (stamp <= Time.time)
        {
            stamp = Time.time + rate;
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].tag == "Enemy")
                {
                    DamageTextManager.CreatePopupText(((int)damage).ToString(), hits[i].transform);
                    hits[i].GetComponent<Enemy>().TakeHealth((int)damage);
                }
            }
        }
    }

}
