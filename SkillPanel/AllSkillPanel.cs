using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSkillPanel : SkillPanel {
    public void OpenArcaneMissilePanel()
    {
        if (!active)
            return;

        panel_manager.ActivatePanel(panel_manager.ArcaneMissilePanel);
    }

    public void OpenArcaneBombPanel()
    {
        if (!active)
            return;

        panel_manager.ActivatePanel(panel_manager.ArcaneBombPanel);
    }
}
