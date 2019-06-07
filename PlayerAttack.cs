using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerAttack : MonoBehaviour {

    public static PlayerAttack singleton;

    public Transform spawn_point;

    //Skills
    public ArcaneMissile arcane_missile;
    public ArcaneBomb arcane_bomb;

    public bool disabled = true;

    public float missile_fire_rate;
    public float bomb_fire_rate;
    float missile_time_stamp;
    float bomb_time_stamp;

    void Awake()
    {
        singleton = this;
    }

    void Update () {
        CheckForAttack();
	}

    void CheckForAttack()
    {
        if (disabled)
            return;

        if (Input.GetMouseButton(0))
        {
            if (Time.time >= missile_time_stamp)
            {
                missile_time_stamp = Time.time + missile_fire_rate;
                arcane_missile.ShootMissile(ShootDir(), spawn_point.position);
            }
        }
        else if (Input.GetMouseButton(1))
        {
            if (Time.time >= bomb_time_stamp)
            {
                bomb_time_stamp = Time.time + bomb_fire_rate;
                arcane_bomb.ShootBomb(ShootDir(), spawn_point.position);
            }
        }
    }

    Vector2 ShootDir()
    {
        Vector2 mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDir = (mouseDir - (Vector2)spawn_point.position).normalized;

        float angle = QuickMaths.VectorToAngle(shootDir.x, shootDir.y);
        angle = angle + Random.Range(-5f, 5f);
        shootDir = QuickMaths.AngleToVector(angle).normalized;

        return shootDir;
    }
}
