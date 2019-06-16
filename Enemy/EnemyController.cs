using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The enemies are relatively simple so most of it's functionality can be contained within this script
/// Contains the enemy's functionality including its health and movement
/// It's health and movespeed are scaled based on the difficulty of the game
/// </summary>

public class EnemyController : MonoBehaviour {

    /// <summary>
    /// The difficulty of the levels are split up into tiers/stages. 
    /// Every tier contains 5 stages
    /// </summary>

    //the base health of the enemy
    [Tooltip("Base hp, it will be scaled on the other multiplers")]
    [SerializeField]
    private float base_health;

    //The health multiplier after every tier
    [Tooltip("Every 5 stages, this multiplier is applied to the base health")]
    [SerializeField]
    private int health_tier_multiplier;

    //The health multiplier after every stage in that tier
    [Tooltip("Every stage this multiplier is applied and added onto the max health")]
    [SerializeField]
    private int health_stage_multiplier;

    //Our enemy's max health after the calculations. it is calculated everytime enemy is spawned
    //Equation for maxHealh = ((CurrentLevel / 5) * health_tier_multiplier) + (((CurrentLevel - 1) % 5) * health_stage_multiplier)
    private float max_health;

    //Current health of the enemy during game
    private float current_health;

    //The display of the health on the enemy
    [SerializeField]
    private Image health_bar;

    public float move_speed;

    //particle FX
    [SerializeField] private GameObject death_effect;
    [SerializeField] private GameObject explosion_effect;

    private Rigidbody2D rb;
    private bool despawned;
    
    //How much arcane shard this enemy will drop after it's killed
    public int arcane_shard_amount;

    //The arcane shard prefab
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
        max_health = (base_health + (LevelManager.singleton.current_level / 5)  * health_tier_multiplier) + 
                                        (((LevelManager.singleton.current_level - 1) % 5) * health_stage_multiplier);
    }
}
