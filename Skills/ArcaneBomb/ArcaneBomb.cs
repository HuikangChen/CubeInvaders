using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBomb : MonoBehaviour {

    public static ArcaneBomb singleton;
    ArcaneMissile arcane_missile;

    public GameObject bomb;
    public float speed;
    public GameObject redFlash;

    public enum Rune { None, Tremendous, Bounce, Gas }

    [Space(20)]
    [Header("Rune Settings")]
    public Rune rune1;
    public Rune rune2;
    public int rune1_slot_cost;
    public int rune2_slot_cost;
    [HideInInspector]
    public bool rune1_unlocked;
    [HideInInspector]
    public bool rune2_unlocked;

    [Space(10)]
    public int tremendous_cost;
    public bool tremendous_unlocked;
    public float tremendous_size;

    [Space(10)]
    public int bounce_cost;
    public bool bounce_unlocked;

    [Space(10)]
    public int gas_cost;
    public bool gas_unlocked;

    [Space(20)]
    [Header("Damage Settings")]
    int min_dmg;
    int max_dmg;
    public int ap_cost;
    public int min_dmg_multiplier;
    public int max_dmg_multiplier;

    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        arcane_missile = ArcaneMissile.singleton;
    }

    public void ShootBomb(Vector2 dir, Vector2 spawn_pos)
    {
        if (Arcane.singleton.SafeTake(ap_cost))
        {
            GameObject obj = PoolManager.Spawn(bomb, spawn_pos, Quaternion.identity);
            SpawnMuzzleFlash(redFlash, spawn_pos, QuickMaths.VectorToAngle(dir.x, dir.y));
            obj.GetComponent<BombController>().damage = Random.Range(MinDmg(), MaxDmg() + 1);
            //obj.GetComponent<MissileController>().damage = Random.Range(MinDmg(), MaxDmg() + 1);
            ActionCamera.singleton.StartShake(.05f, .065f);

            switch (rune1)
            {
                case Rune.Tremendous:
                    obj.AddComponent<Tremendous>();
                    break;
                case Rune.Bounce:
                    obj.AddComponent<Bounce>();
                    break;
                case Rune.Gas:
                    obj.AddComponent<Gas>();
                    break;
            }

            switch (rune2)
            {
                case Rune.Tremendous:
                    obj.AddComponent<Tremendous>();
                    break;
                case Rune.Bounce:
                    obj.AddComponent<Bounce>();
                    break;
                case Rune.Gas:
                    obj.AddComponent<Gas>();
                    break;
            }

            if (rune1 == Rune.Tremendous || rune2 == Rune.Tremendous)
            {
                obj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    obj.transform.GetChild(i).localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
            }
            else
            {
                obj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    obj.transform.GetChild(i).localScale = new Vector3(1f, 1f, 1f);
                }
            }

            obj.GetComponent<Rigidbody2D>().AddForce(dir * speed);
        }
    }

    public int MinDmg()
    {
        return arcane_missile.min_dmg * min_dmg_multiplier;
    }

    public int MaxDmg()
    {
        return arcane_missile.max_dmg * max_dmg_multiplier;
    }

    void SpawnMuzzleFlash(GameObject flash, Vector2 spawnPoint, float angle)
    {
        GameObject obj = PoolManager.Spawn(flash, spawnPoint, Quaternion.identity);
        obj.transform.localEulerAngles = new Vector3(angle + 180, -90, 90);
        PoolManager.Despawn(obj, 1.5f);
    }
}
