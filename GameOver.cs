using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public string scene_name;

    public void Retry()
    {
        SceneManager.LoadScene(scene_name);
    }
}
