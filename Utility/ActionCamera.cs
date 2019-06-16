using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ActionCamera : MonoBehaviour
{
    public static ActionCamera singleton;
    
    Vector3 velocityDampResult = Vector3.zero;
    Vector3 shakeOffset = Vector3.zero; 
    Transform cameraTransform;

    void Awake()
    {
        singleton = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GetComponent<Camera>().transform;
    }

    void FixedUpdate() {
        MoveCamera();
    }
    
    void MoveCamera() {

        transform.position = new Vector3(0, 0, -10) + shakeOffset;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            shakeOffset = new Vector3(x, y, -10);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        shakeOffset = Vector3.zero;
    }

    public void StartShake(float duration, float magnitude) {
        StartCoroutine(Shake(duration, magnitude));
    }
}
