using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour {

    public static LevelManager singleton;
    public delegate void GameStateHandler();
    public static event GameStateHandler OnGameOver;
    public static event GameStateHandler OnNewLevel;

    public GameObject game_over_panel;

    [Header("-LEVEL ANNOUNCER SETTINGS-")]
    public GameObject level_announcer;
    public TextMeshProUGUI stage_number;
    public float la_speed;
    public Transform la_start;
    public Transform la_end;

    ArcaneManager arcane_manager;
    public int current_level;

    [Space(20)]
    [Header("-MONSTER SETTINGS-")]

    [Space(10)]
    [Header("Monster and Spawner Types")]
    public List<Enemy> enemy = new List<Enemy>();
    public List<EnemySpawner> spawner = new List<EnemySpawner>();

    [Space(10)]
    [Header("Drop Speed")]
    public float drop_speed_difference;
    public float drop_speed_base;
    public float drop_speed_multiplier;
    float min_drop_speed;
    float max_drop_speed;

    [Space(10)]
    [Header("Spawn Cooldown")]
    public float min_base_cooldown;
    public float max_base_cooldown;
    float min_spawn_cooldown;
    float max_spawn_cooldown;

    [Space(10)]
    [Header("Monster Count")]
    public int base_monster_count;
    int monster_count;
    [HideInInspector]
    public int monster_left;

    public TextMeshProUGUI monster_left_text;
    SkillPanelManager skill_panel_manager;

    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        arcane_manager = ArcaneManager.singleton;
        arcane_manager.Initialize();
        arcane_manager.StartArcaneRegen();

        skill_panel_manager = SkillPanelManager.singleton;

        DamageTextManager.Initialize();

        StartCoroutine("StartLevel");
    }

    IEnumerator StartLevel()
    {
        current_level++;
        arcane_manager.Initialize();

        if (OnNewLevel != null)
        {
            OnNewLevel();
        }

        yield return StartCoroutine(AnnounceLevel("stage " + current_level));

        SetDifficulty();

        PlayerAttack.singleton.disabled = false;

        stage_number.text = "Stage " + current_level;

        yield return new WaitForSeconds(.15f);

        while (monster_left > 0)
        {
            if (monster_count > 0)
            {
                spawner[Random.Range(0, spawner.Count)].Spawn(enemy[0].gameObject, min_drop_speed, max_drop_speed);
                yield return new WaitForSeconds(Random.Range(min_spawn_cooldown, max_spawn_cooldown));
                monster_count--;
            }
            yield return null;
        }

        StartCoroutine("EndLevel");
    }

    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(.1f);

        monster_left_text.gameObject.SetActive(false);

        stage_number.text = " ";

        PlayerAttack.singleton.disabled = true;

        yield return StartCoroutine(AnnounceLevel("clear"));

        yield return StartCoroutine(ArcaneShardManager.singleton.PickupShards());

        skill_panel_manager.ActivatePanel(skill_panel_manager.AllSkillPanel);

        while (skill_panel_manager.active_panels.Count > 0)
        {
            yield return null;
        }

        StartCoroutine("StartLevel");
    }

    IEnumerator AnnounceLevel(string text)
    {
        level_announcer.GetComponent<TextMeshProUGUI>().text = text;
        level_announcer.transform.position = la_start.position;
        while (Vector2.Distance(level_announcer.transform.position, la_end.position) > 0.1)
        {
            level_announcer.transform.position = Vector2.Lerp(level_announcer.transform.position, la_end.position, la_speed * Time.deltaTime);
            yield return null;
        }
        level_announcer.transform.position = la_end.position;

        while (Vector2.Distance(level_announcer.transform.position, la_start.position) > 0.1)
        {
            level_announcer.transform.position = Vector2.Lerp(level_announcer.transform.position, la_start.position, la_speed * Time.deltaTime * 1.5f);
            yield return null;
        }
        level_announcer.transform.position = la_start.position;
    }

    void SetDifficulty()
    {
        SetDropSpeed();
        SetSpawnCooldown();
        SetMonsterCount();
    }

    void SetDropSpeed()
    {
        min_drop_speed = (((current_level - 1) % 5) * drop_speed_multiplier) + drop_speed_base;
        max_drop_speed = min_drop_speed * drop_speed_difference;
    }

    void SetSpawnCooldown()
    {
        min_spawn_cooldown = min_base_cooldown - (((current_level - 1) % 5) + 1)/5;
        max_spawn_cooldown = max_base_cooldown - (((current_level - 1) % 5) + 1)/3;
    }

    void SetMonsterCount()
    {
        monster_count = (base_monster_count * ((current_level/5) + 1)) + ((current_level - 1) % 5)  * 3;
        monster_left = monster_count;
        UpdateRemainingMonsterText();
    }

    public void UpdateRemainingMonsterText()
    {
        if (monster_left_text.gameObject.activeInHierarchy == false)
        {
            monster_left_text.gameObject.SetActive(true);
        }
        monster_left_text.text = "Enemies: " + monster_left;
    }

    public void GameOver()
    {
        if (OnGameOver != null)
        {
            OnGameOver();
        }

        StopAllCoroutines();

        PlayerAttack.singleton.disabled = true;
        game_over_panel.SetActive(true);
    }

}
