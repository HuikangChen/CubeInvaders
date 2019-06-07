using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour {

    static GameObject enemy_damage_text;
    static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("SkillPanels(UI)");

        if(enemy_damage_text == null)
        enemy_damage_text = Resources.Load<GameObject>("PopupTextParent");
    }

    public static void CreatePopupText(string text, Transform location)
    {
        GameObject damage_text = Instantiate(enemy_damage_text);//PoolManager.Spawn(enemy_damage_text, location.position, Quaternion.identity);
        Vector2 target_pos = new Vector2(location.position.x + Random.Range(-.25f, .25f),
                                                                        location.position.y + Random.Range(-.25f, .25f));

        damage_text.transform.SetParent(canvas.transform, false);
        damage_text.transform.position = target_pos;
        damage_text.GetComponent<EnemyDamageText>().SetText(text);
    }
}
