using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class BetterJump : MonoBehaviour {

    public float fall_multiplier = 2.5f;
    public float low_jump_multiplier = 2f;

    Rigidbody2D rb;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fall_multiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !InputManager.ActiveDevice.Action1)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (low_jump_multiplier - 1) * Time.deltaTime;
        }
	}
}
