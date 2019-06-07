using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour {

    public bool active;
    [HideInInspector]
    public SkillPanelManager panel_manager;

    void Start()
    {
        panel_manager = SkillPanelManager.singleton;
    }

}
