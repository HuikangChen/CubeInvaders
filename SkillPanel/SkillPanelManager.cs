using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillPanelManager : MonoBehaviour {

    public static SkillPanelManager singleton;

    [Header("Skill Panels")]
    public GameObject AllSkillPanel;
    public GameObject ArcaneMissilePanel;
    public GameObject ArcaneBombPanel;

    [HideInInspector]
    public List<GameObject> active_panels = new List<GameObject>();

    [Header("Other Panels")]
    public GameObject popup_panel;
    public GameObject continue_text;

    public Transform startPos;
    public Transform endPos;
    bool panelMoving;
    public float panelMoveSpeed;

    void Awake()
    {
        singleton = this;
    }

    void Update()
    {
        if (active_panels.Count > 0 && (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Space)))
        {
            DeactivatePanel(active_panels[active_panels.Count - 1]);
        }
    }

    public void ActivatePanel(GameObject panel)
    {
        if (panelMoving)
        {
            return;
        }

        if (active_panels.Count > 0)
        {
            active_panels[active_panels.Count - 1].GetComponent<SkillPanel>().active = false;
            active_panels[active_panels.Count - 1].SetActive(false);
        }

        if (continue_text.activeInHierarchy == false)
        {
            continue_text.SetActive(true);
        }

        panel.SetActive(true);
        panel.GetComponent<SkillPanel>().active = true;
        active_panels.Add(panel);
        StartCoroutine(MovePanel(panel, startPos, endPos, false));
    }

    public void DeactivatePanel(GameObject panel)
    {
        if (panelMoving)
        {
            return;
        }

        panel.GetComponent<SkillPanel>().active = false;
        active_panels.Remove(panel);
        if (active_panels.Count > 0)
        {
            active_panels[active_panels.Count - 1].GetComponent<SkillPanel>().active = true;
            active_panels[active_panels.Count - 1].SetActive(true);
        }

        if (active_panels.Count == 0)
        {
            if (continue_text.activeInHierarchy)
            {
                continue_text.SetActive(false);
            }
        }

        StartCoroutine(MovePanel(panel, endPos, startPos, true));
    }

    public void PopupMessage(string message)
    {
        popup_panel.gameObject.SetActive(true);
        popup_panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = message;
        StopCoroutine("PopupMessageCo");
        StartCoroutine("PopupMessageCo");
    }

    IEnumerator PopupMessageCo()
    {
        yield return new WaitForSeconds(1.5f);

        popup_panel.gameObject.SetActive(false);
    }

    IEnumerator MovePanel(GameObject panel, Transform start, Transform end, bool hidePanel)
    {
        panelMoving = true;
        panel.transform.position = start.position;

        while (Vector2.Distance(panel.transform.position, end.position) > 0.01f)
        {
            panel.transform.position = Vector2.Lerp(panel.transform.position, end.position, panelMoveSpeed * Time.deltaTime);
            yield return null;
        }

        panel.transform.position = end.position;

        if (hidePanel)
        {
            panel.SetActive(false);
        }

        panelMoving = false;
    }
}
