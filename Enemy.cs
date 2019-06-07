using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float max_health_base;
    public int max_health_tier_multiplier;
    public int max_health_stage_multiplier;

    float max_health;
    float current_health;

    public float move_speed;

    public GameObject death_effect;
    public GameObject explosion_effect;
    public Image health_bar;

    Rigidbody2D rb;
    bool despawned;
    
    public int arcane_shard_amount;
    public GameObject arcane_shard;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        LevelManager.OnGameOver += OnGameOver;
    }

    void OnEnable()
    {
        SetHealthDifficulty();
        current_health = max_health;
        UpdateHealthBar();
        despawned = false;
    }

    void Update()
    {
        rb.velocity = Vector2.down * move_speed;
    }

    void OnGameOver()
    {
        KillEnemy(explosion_effect);
    }
	
    public void AddHealth(float amount)
    {
        if ((current_health + amount) <= max_health)
        {
            current_health += amount;
        }
        else
        {
            current_health = max_health;
        }

        UpdateHealthBar();
    }

    public void TakeHealth(float amount)
    {
        if ((current_health - amount) > 0)
        {
            current_health -= amount;
        }
        else
        {
            current_health = 0;
            DropArcaneShard();
            KillEnemy(death_effect);
        }

        UpdateHealthBar();
    }

    public float CurrentHealth()
    {
        return current_health;
    }

    void UpdateHealthBar()
    {
        health_bar.fillAmount = current_health / max_health;
    }

    void DropArcaneShard()
    {
        for (int i = 0; i < arcane_shard_amount; i++)
        {
            GameObject obj = PoolManager.Spawn(arcane_shard, transform.position, Quaternion.identity);
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1;
            rb.GetComponent<BoxCollider2D>().enabled = true;
            rb.AddForce(Vector2.left * Random.Range(-500, 500));
            ArcaneShardManager.singleton.shard_list.Add(obj);
        }
    }

    void KillEnemy(GameObject deathEffect)
    {
        if (despawned == false)
        {
            despawned = true;
            ActionCamera.singleton.StartShake(.2f, .075f);
            GameObject obj = PoolManager.Spawn(deathEffect, transform.position, Quaternion.identity);
            LevelManager.singleton.monster_left--;
            LevelManager.singleton.UpdateRemainingMonsterText();
            PoolManager.Despawn(obj, 2.5f);
            PoolManager.Despawn(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<Transform>().parent.GetComponent<PlayerHealth>().Take(1);
            KillEnemy(explosion_effect);
        }
    }

    void SetHealthDifficulty()
    {
        max_health = (max_health_base + (LevelManager.singleton.current_level / 5)  * max_health_tier_multiplier) + 
                                        (((LevelManager.singleton.current_level - 1) % 5) * max_health_stage_multiplier);
    }
}
