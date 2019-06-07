using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamageText : MonoBehaviour {

    public Animator animator;
    public Text damage_text;

	// Use this for initialization
	void Start () {
        AnimatorClipInfo[] clip_info = animator.GetCurrentAnimatorClipInfo(0);
        //PoolManager.Despawn(gameObject, clip_info[0].clip.length);
        Destroy(gameObject, clip_info[0].clip.length);
	}

    public void SetText(string text)
    {
        damage_text.text = text;
    }
}
