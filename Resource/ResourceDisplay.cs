using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] protected Resource resource;

    protected virtual void OnEnable()
    {
        if (resource == null)
        {
            Debug.LogError("No Resource Found, Please Add Resource");
            return;
        }

        resource.OnAdd += UpdateDisplay;
        resource.OnTake += UpdateDisplay;
        resource.OnInitialize += UpdateDisplay;
        resource.Initialize();
    }

    protected virtual void UpdateDisplay(float current, float max)
    {
        
    }

    protected virtual void OnDisable()
    {
        resource.OnAdd -= UpdateDisplay;
        resource.OnTake -= UpdateDisplay;
        resource.OnInitialize -= UpdateDisplay;
    }
}
