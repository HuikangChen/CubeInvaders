using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.ProgressBars.Scripts;
using TMPro;

public class Arcane : Resource {

    public static Arcane singleton;

    public float arcane_regen;

    void Awake()
    {
        singleton = this;
    }

    private void OnEnable()
    {
        Initialize();
    }

    public void StartArcaneRegen()
    {
        StartCoroutine("ArcaneRegen");
    }

    public void StopArcaneRegen()
    {
        StopCoroutine("ArcaneRegen");
    }

    IEnumerator ArcaneRegen()
    {
        while (true)
        {
            Add(arcane_regen * Time.deltaTime);
            yield return null;
        }
    }
}
