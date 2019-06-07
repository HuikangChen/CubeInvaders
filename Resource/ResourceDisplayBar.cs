using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplayBar : ResourceDisplay
{
    public Image fillBar;
    Camera cam;

    protected override void OnEnable()
    {
        base.OnEnable();
        cam = Camera.main;
    }

    protected override void UpdateDisplay(float current, float max)
    {
        base.UpdateDisplay(current, max);

        fillBar.fillAmount = current / max;
    }

    private void FixedUpdate()
    {
        RotateTowardsCamera();
    }

    void RotateTowardsCamera()
    {
        transform.LookAt(cam.transform);
    }
}
