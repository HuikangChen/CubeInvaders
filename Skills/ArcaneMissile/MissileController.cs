using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour {

    public float life_time;
    public int damage;
    public int ap_cost;

    public GameObject impact_effect_red;
    public GameObject impact_effect_blue;

    Rigidbody2D rb;
    bool can_despawn;

    public GameObject enemy_to_ignore;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        can_despawn = true;
    }

	// Update is called once per frame
	void Update () {
        //rb.velocity = transform.up * speed;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (can_despawn)
        {
            if (col.tag == "Enemy")
            {
                if (enemy_to_ignore == null || (enemy_to_ignore != null && enemy_to_ignore != col.gameObject))
                {
                    if (GetComponent<BitterCold>())
                    {
                        GetComponent<BitterCold>().OnEnemyHit(col);
                    }

                    if (GetComponent<Nirvana>())
                    {
                        GetComponent<Nirvana>().OnEnemyHit(col);
                    }

                    if (GetComponent<Split>())
                    {
                        GetComponent<Split>().OnEnemyHit(col);
                    }

                    GameObject obj;

                    if (GetComponent<BitterCold>())
                    {
                        obj = PoolManager.Spawn(impact_effect_blue, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        obj = PoolManager.Spawn(impact_effect_red, transform.position, Quaternion.identity);
                    }
                    PoolManager.Despawn(obj, 2f);
                    can_despawn = false;
                    DamageTextManager.CreatePopupText(damage.ToString(), col.transform);
                    col.GetComponent<EnemyController>().TakeHealth(damage);
                    PoolManager.Despawn(gameObject);
                }
            }
            else if (col.tag == "Ground")
            {
                GameObject obj;

                if (GetComponent<BitterCold>())
                {
                    obj = PoolManager.Spawn(impact_effect_blue, transform.position, Quaternion.identity);
                }
                else
                {
                    obj = PoolManager.Spawn(impact_effect_red, transform.position, Quaternion.identity);
                }

                PoolManager.Despawn(obj, 2f);
                can_despawn = false;

                PoolManager.Despawn(gameObject);
            }
        }
    }

    void OnDisable()
    {
        if (enemy_to_ignore != null)
        {
            enemy_to_ignore = null;
        }
    }
}
