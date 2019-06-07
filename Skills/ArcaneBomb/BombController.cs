using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {
    
    public int damage;

    public GameObject impact_effect;
    public GameObject impact_damage;
    public GameObject impact_gas;

    Rigidbody2D rb;
    bool can_despawn;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        can_despawn = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (can_despawn)
        {
            if (col.tag == "Enemy")
            {

                GameObject obj = PoolManager.Spawn(impact_effect, transform.position, Quaternion.identity);
                GameObject impactDmg = PoolManager.Spawn(impact_damage, transform.position, Quaternion.identity);
                ActionCamera.singleton.StartShake(.15f, .05f);

                if (ArcaneBomb.singleton.rune1 == ArcaneBomb.Rune.Tremendous || ArcaneBomb.singleton.rune2 == ArcaneBomb.Rune.Tremendous)
                {
                    obj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    impactDmg.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    for (int i = 0; i < obj.transform.childCount; i++)
                    {
                        obj.transform.GetChild(i).localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    }
                }
                else
                {
                    obj.transform.localScale = new Vector3(1f, 1f, 1f);
                    impactDmg.transform.localScale = new Vector3(1f, 1f, 1f);
                    for (int i = 0; i < obj.transform.childCount; i++)
                    {
                        obj.transform.GetChild(i).localScale = new Vector3(1f, 1f, 1f);
                    }
                }

                if (GetComponent<Tremendous>())
                {
                    impactDmg.GetComponent<BombImpactController>().damage = damage * (int)GetComponent<Tremendous>().damageMultiplier;
                }
                else
                {
                    impactDmg.GetComponent<BombImpactController>().damage = damage;
                }
                PoolManager.Despawn(obj, 5);
                PoolManager.Despawn(impactDmg, .1f);
            
                DamageTextManager.CreatePopupText(damage.ToString(), col.transform);

                if (GetComponent<Gas>())
                {
                    GameObject gas = PoolManager.Spawn(impact_gas, transform.position, Quaternion.identity);
                    if (GetComponent<Tremendous>())
                    {
                        gas.GetComponent<DOT>().damage = damage * (int)GetComponent<Tremendous>().damageMultiplier / 4f;
                    }
                    else
                    {
                        gas.GetComponent<DOT>().damage = damage / 4;
                    }
                }

                if (GetComponent<Bounce>())
                {
                    GetComponent<Bounce>().DoBounce(col);
                }
                else
                {
                    can_despawn = false;
                    PoolManager.Despawn(gameObject);
                }

            }
            else if (col.tag == "Ground")
            {
                GameObject obj = PoolManager.Spawn(impact_effect, transform.position, Quaternion.identity);
                PoolManager.Despawn(obj, 5);
                ActionCamera.singleton.StartShake(.15f, .05f);

                if (ArcaneBomb.singleton.rune1 == ArcaneBomb.Rune.Tremendous || ArcaneBomb.singleton.rune2 == ArcaneBomb.Rune.Tremendous)
                {
                    obj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    for (int i = 0; i < obj.transform.childCount; i++)
                    {
                        obj.transform.GetChild(i).localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    }
                }
                else
                {
                    obj.transform.localScale = new Vector3(1f, 1f, 1f);
                    for (int i = 0; i < obj.transform.childCount; i++)
                    {
                        obj.transform.GetChild(i).localScale = new Vector3(1f, 1f, 1f);
                    }
                }

                GameObject impactDmg = PoolManager.Spawn(impact_damage, transform.position, Quaternion.identity);
                if (GetComponent<Tremendous>())
                {
                    impactDmg.GetComponent<BombImpactController>().damage = damage * (int)GetComponent<Tremendous>().damageMultiplier;
                }
                else
                {
                    impactDmg.GetComponent<BombImpactController>().damage = damage;
                }

                if (GetComponent<Gas>())
                {
                    GameObject gas = PoolManager.Spawn(impact_gas, transform.position, Quaternion.identity);
                    if (GetComponent<Tremendous>())
                    {
                        gas.GetComponent<DOT>().damage = damage * (int)GetComponent<Tremendous>().damageMultiplier / 4f;
                    }
                    else
                    {
                        gas.GetComponent<DOT>().damage = damage / 4;
                    }
                }

                PoolManager.Despawn(impactDmg, .1f);
                can_despawn = false;
                PoolManager.Despawn(gameObject);
            }
        }
    }
}
