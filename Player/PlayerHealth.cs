using UnityEngine;
using TMPro;

public class PlayerHealth : Resource {

    public static PlayerHealth singleton;

    void Awake()
    {
        singleton = this;
        LevelManager.OnNewLevel += Initialize;
    }

    // Use this for initialization
    void Start () {
        Initialize();
        OnEmpty += OnDeath;
	}

    void OnDeath(float current, float max)
    {
        print("dead");
        LevelManager.OnNewLevel -= Initialize;
        LevelManager.singleton.GameOver();
    }
}
