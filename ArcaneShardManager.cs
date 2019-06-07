using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArcaneShardManager : MonoBehaviour {

    public static ArcaneShardManager singleton;

    public List<GameObject> shard_list = new List<GameObject>();

    public GameObject shardDestinationGO;
    public TextMeshProUGUI arcane_shard_text;
    public float text_anim_speed;
    public float text_anim_size;
    bool text_anim_running;

    public int arcane_shard;
    Vector2 shard_destination;

    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        FindShardDestination();
    }

    void FindShardDestination()
    {
        shard_destination = Camera.main.ScreenToWorldPoint(shardDestinationGO.transform.position);
    }

    public bool AddArcaneShard(int amount)
    {
        arcane_shard += amount;
        UpdateShardText();
        return true;
    }

    public bool TakeArcaneShard(int amount)
    {
        if (arcane_shard - amount < 0)
        {
            return false;
        }

        arcane_shard -= amount;
        UpdateShardText();
        return true;
    }

    void UpdateShardText()
    {
        arcane_shard_text.text = "x" + arcane_shard;

        if (text_anim_running == false)
        {
            StartCoroutine("TextAnim");
        }
    }

    IEnumerator TextAnim()
    {
        text_anim_running = true;

        Vector3 normal_size = arcane_shard_text.transform.localScale;
        Vector3 target_size = arcane_shard_text.transform.localScale * text_anim_size;

        while (Vector3.Distance(arcane_shard_text.transform.localScale, target_size) > 0.01)
        {
            arcane_shard_text.transform.localScale = Vector3.Lerp(arcane_shard_text.transform.localScale, target_size, text_anim_speed * Time.deltaTime);
            yield return null;
        }

        arcane_shard_text.transform.localScale = target_size;

        while (Vector3.Distance(arcane_shard_text.transform.localScale, normal_size) > 0.01)
        {
            arcane_shard_text.transform.localScale = Vector3.Lerp(arcane_shard_text.transform.localScale, normal_size, text_anim_speed * Time.deltaTime);
            yield return null;
        }

        arcane_shard_text.transform.localScale = normal_size;

        text_anim_running = false;
    }

    public IEnumerator PickupShards()
    {
        for (int i = 0; i < shard_list.Count; i++)
        {
            StartCoroutine(MoveShard(shard_list[i]));
            AddArcaneShard(100);
        }
        shard_list.Clear();
        yield return null;
    }

    IEnumerator MoveShard(GameObject obj)
    {
        obj.GetComponent<Rigidbody2D>().gravityScale = 0;
        obj.GetComponent<BoxCollider2D>().enabled = false;

        Vector2 dir = shard_destination - new Vector2(obj.transform.position.x, obj.transform.position.y);

        yield return new WaitForSeconds(Random.Range(0.1f, 0.25f));

        obj.GetComponent<Rigidbody2D>().AddForce(dir * 60);

        while (Vector2.Distance(obj.transform.position, shard_destination) > 0.2)
        {
            yield return null;
        }

        PoolManager.Despawn(obj);
    }
}
