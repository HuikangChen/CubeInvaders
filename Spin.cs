using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public bool spinOnStart;

    [Header("Spin Axis")]
    [Range(0.0f, 1.0f)]
    public float x;
    [Range(0.0f, 1.0f)]
    public float y;
    [Range(0.0f, 1.0f)]
    public float z;

    [Header("Speed Settings")]
    public float speed;
    public float maxSpeed;
    public float spinIncreaseRate;
    bool spinStarted;
    float originalSpeed;

    private void Start()
    {
        originalSpeed = speed;

        if (spinOnStart)
            spinStarted = true;
    }

    private void FixedUpdate()
    {
        if (!spinStarted)
            return;

        transform.Rotate(new Vector3(x, y, z).normalized * speed);
    }

    public void StartSpin()
    {
        spinStarted = true;
    }

    public void StopSpin()
    {
        spinStarted = false;
    }

    public void IncreaseSpeed(float rate)
    {
        if (speed >= maxSpeed)
            return;
        speed += Time.deltaTime * rate;
    }

    public void DecreaseSpeed(float rate)
    {
        if (speed <= originalSpeed)
            return;
        speed -= Time.deltaTime * rate;
    }
}
