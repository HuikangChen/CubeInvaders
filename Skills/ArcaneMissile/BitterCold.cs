using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitterCold : MonoBehaviour {
    
	void OnEnable () {
        ChangeMissileColor();
	}

    public void ChangeMissileColor()
    {
        if (!transform.GetChild(0).gameObject.activeInHierarchy)
            return;

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    void OnDisable()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        Destroy(this);
    }

    public void OnEnemyHit(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            if (GetComponent<MissileController>().enemy_to_ignore == null ||
                GetComponent<MissileController>().enemy_to_ignore && GetComponent<MissileController>().enemy_to_ignore != col.gameObject)
            {
                if (col.gameObject.activeInHierarchy == false)
                    return;

                if (col.GetComponent<BitterColdStack>() == null)
                {
                    col.gameObject.AddComponent<BitterColdStack>();
                    col.GetComponent<BitterColdStack>().Initialize();
                }
                col.GetComponent<BitterColdStack>().AddStack();
            }
        }
    }
}
