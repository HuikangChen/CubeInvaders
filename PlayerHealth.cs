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
	}

    void OnDeath()
    {
        LevelManager.OnNewLevel -= Initialize;
        LevelManager.singleton.GameOver();
    }
}
