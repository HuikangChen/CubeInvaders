using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerMovement : MonoBehaviour {

    public float move_speed;
    public float jump_height;

    public Transform ground_check;
    public LayerMask ground_layer;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update () {
        MovePlayer(WalkInput(), JumpInput());
	}

    void MovePlayer(float walk_value, float jump_value)
    {
        rb.velocity = new Vector2(walk_value, jump_value);
    }

    float WalkInput()
    {
        if (Mathf.Abs(InputManager.ActiveDevice.LeftStickX.Value) > .75f)
        {
            return InputManager.ActiveDevice.LeftStickX.Value * move_speed;
        }
        return 0;
    }

    float JumpInput()
    {
        if (InputManager.ActiveDevice.Action1.WasPressed && grounded() == true)
        {
            return jump_height;
        }
        return rb.velocity.y;
    }

    bool grounded()
    {
        return Physics2D.OverlapCircle(ground_check.position, .1f, ground_layer);
    }
}
