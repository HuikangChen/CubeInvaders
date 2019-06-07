using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    
    public float random_spawn_cooldown;
    
    public float random_enemy_speed;

    public void Spawn(GameObject enemy, float min_speed, float max_speed)
    {
        SpawnEnemy(enemy, Random.Range(min_speed, max_speed));
    }

    IEnumerator StartSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, random_spawn_cooldown));

            SpawnEnemy(Random.Range(1, random_enemy_speed));
        }
    }

    void SpawnEnemy(float speed)
    {
        GameObject obj = PoolManager.Spawn(enemy, transform.position, Quaternion.identity);
        obj.GetComponent<Enemy>().move_speed = speed;
    }

    void SpawnEnemy(GameObject enemy, float speed)
    {
        GameObject obj = PoolManager.Spawn(enemy, transform.position, Quaternion.identity);
        obj.GetComponent<Enemy>().move_speed = speed;
    }
}
