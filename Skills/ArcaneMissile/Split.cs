using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour {

    void Start()
    {
        GetComponent<MissileController>().damage = GetComponent<MissileController>().damage / 2;
    }

    public void OnEnemyHit(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            for (int i = 1; i < 6; i++)
            {
                GameObject obj = PoolManager.Spawn(ArcaneMissile.singleton.missile, transform.position, Quaternion.identity);
                obj.GetComponent<MissileController>().damage = Random.Range(ArcaneMissile.singleton.min_dmg/2, (ArcaneMissile.singleton.max_dmg/2) + 1);
                if (GetComponent<BitterCold>())
                {
                    obj.AddComponent<BitterCold>();
                }
                if (GetComponent<Nirvana>())
                {
                    obj.AddComponent<Nirvana>();
                }
                obj.GetComponent<MissileController>().enemy_to_ignore = col.gameObject;
              
                obj.GetComponent<Rigidbody2D>().AddForce(ArcaneMissile.singleton.speed *
                                                         PlayerAim.singleton.AngleToVector((25 * i) + 15));
                if (obj.GetComponent<Split>())
                {
                    Destroy(obj.GetComponent<Split>());
                }
            }
        }
    }

    void OnDisable()
    {
        Destroy(this);
    }


}
