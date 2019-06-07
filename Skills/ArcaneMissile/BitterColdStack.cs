using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitterColdStack : MonoBehaviour {

    static GameObject bitter_cold_stack;
    Enemy enemy;
    int stack_count;
    float slow_amount;

    List<GameObject> cold_stacks = new List<GameObject>();

    // Use this for initialization
    public void Initialize () {
        if (bitter_cold_stack == null)
        {
            bitter_cold_stack = Resources.Load<GameObject>("BitterColdStack");
        }
        enemy = GetComponent<Enemy>();
        slow_amount = enemy.move_speed / 4;
	}

    public void AddStack()
    {
        if (stack_count < 4)
        {
            if (enemy.move_speed - slow_amount >= 0)
            {
                enemy.move_speed -= slow_amount;
            }
            else
            {
                enemy.move_speed = 0;
            }

            GameObject obj = Instantiate(bitter_cold_stack);
            obj.transform.SetParent(transform);
            obj.transform.localPosition = new Vector2(-.4f + (stack_count * .26f), 1);
            //GetComponent<SpriteRenderer>().color = Color.cyan;
            cold_stacks.Add(obj);
            stack_count++;
            StopCoroutine("DefrostTime");
            StartCoroutine("DefrostTime");
        }
    }

    public void RemoveAllStack()
    {
        for (int i = 0; i < cold_stacks.Count; i++)
        {
            Destroy(cold_stacks[i]);
        }
        cold_stacks.Clear();
        enemy.move_speed = enemy.move_speed + (stack_count * slow_amount);
        //GetComponent<SpriteRenderer>().color = Color.white;
        stack_count = 0;
    }

    void OnEnable()
    {
        if (cold_stacks.Count == 0)
            return;
        RemoveAllStack();
    }

    IEnumerator DefrostTime()
    {
        float time = 2f;
        while (time > 0.1f)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        RemoveAllStack();
    }
}
