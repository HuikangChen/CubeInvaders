using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CusorController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        FollowMouse();
    }

    void FollowMouse()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(position.x, position.y, 0);
    }
}
