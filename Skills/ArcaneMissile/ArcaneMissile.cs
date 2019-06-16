using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneMissile : MonoBehaviour {

    public static ArcaneMissile singleton;

    public GameObject missile;
    public GameObject missleRedFlash;
    public GameObject missleBlueFlash;
    public float speed;

    public enum Rune {None, BitterCold, Nirvana, Split }

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
    public int bitter_cold_cost;
    public bool bitter_cold_unlocked;

    [Space(10)]
    public int nirvana_cost;
    public bool nirvana_unlocked;

    [Space(10)]
    public int split_cost;
    public bool split_unlocked;

    [Space(20)]
    [Header("Damage Settings")]
    public int min_dmg;
    public int max_dmg;
    public int ap_cost;

    public int current_damage_level = 1;
    public int damage_upgrade_cost = 600;

    void Awake()
    {
        singleton = this;
    }

    public void ShootMissile(Vector2 dir, Vector2 spawn_pos)
    {
        if (Arcane.singleton.SafeTake(ApCost()))
        {
            GameObject obj = PoolManager.Spawn(missile, spawn_pos, Quaternion.identity);
            obj.GetComponent<MissileController>().damage = Random.Range(min_dmg, max_dmg + 1);
            ActionCamera.singleton.StartShake(.05f, .04f);

            if (rune1 == Rune.BitterCold || rune2 == Rune.BitterCold)
            {
                SpawnMuzzleFlash(missleBlueFlash, spawn_pos, QuickMaths.VectorToAngle(dir.x, dir.y));
            }
            else
            {
                SpawnMuzzleFlash(missleRedFlash, spawn_pos, QuickMaths.VectorToAngle(dir.x, dir.y));
            }

            switch (rune1)
            {
                case Rune.BitterCold:
                    obj.AddComponent<BitterCold>();
                    break;
                case Rune.Nirvana:
                    obj.AddComponent<Nirvana>();
                    break;
                case Rune.Split:
                    obj.AddComponent<Split>();
                    break;
            }

            switch (rune2)
            {
                case Rune.BitterCold:
                    obj.AddComponent<BitterCold>();
                    break;
                case Rune.Nirvana:
                    obj.AddComponent<Nirvana>();
                    break;
                case Rune.Split:
                    obj.AddComponent<Split>();
                    break;
            }

            obj.GetComponent<Rigidbody2D>().AddForce(dir * speed);
        }
    }

    public void SetRune(Rune rune1, Rune rune2)
    {
        this.rune1 = rune1;
        this.rune2 = rune2;
    }

    public int ApCost()
    {
        if(rune1 == Rune.Nirvana || rune2 == Rune.Nirvana)
        {
            return 0;
        }
        return ap_cost;
    }

    public void SetDamageStats(int new_min_dmg, int new_max_dmg, int new_dmg_lvl, int new_upgrade_cost)
    {
        min_dmg = new_min_dmg;
        max_dmg = new_max_dmg;
        current_damage_level = new_dmg_lvl;
        damage_upgrade_cost = new_upgrade_cost;
    }

    public int MinDmgChangeAmount(int dmg_lvl)
    {
        return (dmg_lvl / 5) + 1;
    }

    public int DmgCostChangeAmount(int dmg_lvl)
    {
        return ((dmg_lvl / 5) + 1) * 600 + ((dmg_lvl/5) * 800);
    }

    void SpawnMuzzleFlash(GameObject flash, Vector2 spawnPoint, float angle)
    {
        GameObject obj = PoolManager.Spawn(flash, spawnPoint, Quaternion.identity);
        obj.transform.localEulerAngles = new Vector3(angle + 180, -90, 90);
        PoolManager.Despawn(obj, 1.5f);
    }
}
