using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArcaneMissilePanel : SkillPanel {

    //Refrence to the arcane missile skill attached to player
    ArcaneMissile arcane_missile;

    [Header("Damage Upgrade Stuff")]
    public TextMeshProUGUI shard_count_text;
    public TextMeshProUGUI damage_text;

    [Space(10)]
    public Button increase_damage_button;
    public Button decrease_damage_button;
    public Button confirm_button;

    int temp_shard_count;
    int temp_min_dmg;
    int temp_max_dmg;
    int temp_upgrade_cost;
    int temp_dmg_lvl;

    [Space(20)]
    [Header("Rune Upgrade Stuff")]
    public Button rune1;
    public Button rune2;

    public Button bitter_cold_unlock_button;
    public Button nirvana_unlock_button;
    public Button split_unlock_button;
    public GameObject bitter_cold_coin_icon;
    public GameObject nirvana_coin_icon;
    public GameObject split_coin_icon;

    public Sprite unlocked_slot_icon;
    public Sprite bitter_cold_icon;
    public Sprite nirvana_icon;
    public Sprite split_icon;

    public TextMeshProUGUI rune1_cost_text;
    public TextMeshProUGUI rune2_cost_text;
    public TextMeshProUGUI bitter_cold_cost_text;
    public TextMeshProUGUI nirvana_cost_text;
    public TextMeshProUGUI split_cost_text;


    void OnEnable()
    {
        if (arcane_missile == null)
        {
            arcane_missile = ArcaneMissile.singleton;
            SetRuneCostText();
        }
        SetTempVariables();
        SetRemainingShards();
        SetDmgText();
        SetButtonInteractability();
    }

    void SetRemainingShards()
    {
        shard_count_text.text = "" + temp_shard_count;
    }

    void SetTempVariables()
    {
        temp_shard_count = ArcaneShardManager.singleton.arcane_shard;
        temp_min_dmg = arcane_missile.min_dmg;
        temp_max_dmg = arcane_missile.max_dmg;
        temp_upgrade_cost = arcane_missile.damage_upgrade_cost;
        temp_dmg_lvl = arcane_missile.current_damage_level;
    }

    void SetRuneCostText()
    {
        rune1_cost_text.text = "" + arcane_missile.rune1_slot_cost;
        rune2_cost_text.text = "" + arcane_missile.rune2_slot_cost;
        bitter_cold_cost_text.text = "" + arcane_missile.bitter_cold_cost;
        nirvana_cost_text.text = "" + arcane_missile.nirvana_cost;
        split_cost_text.text = "" + arcane_missile.split_cost;
    }

    void SetButtonInteractability()
    {
        if (temp_shard_count >= temp_upgrade_cost)
        {
            increase_damage_button.interactable = true;
        }
        else
        {
            increase_damage_button.interactable = false;
        }

        if (temp_min_dmg == arcane_missile.min_dmg)
        {
            decrease_damage_button.interactable = false;
        }
        else
        {
            decrease_damage_button.interactable = true;
        }
    }

    void SetDmgText()
    {
        damage_text.text = temp_min_dmg + "~" + temp_max_dmg;
        if (temp_min_dmg == arcane_missile.min_dmg)
        {
            damage_text.color = Color.white;
            confirm_button.interactable = false;
            confirm_button.gameObject.SetActive(false);
        }
        else
        {
            damage_text.color = Color.green;
            confirm_button.gameObject.SetActive(true);
            confirm_button.interactable = true;
        }
    }

    //Increase Level Button
    public void IncreaseDamageLevel()
    {
        temp_shard_count -= temp_upgrade_cost;

        temp_min_dmg += arcane_missile.MinDmgChangeAmount(temp_dmg_lvl);
        temp_max_dmg = temp_min_dmg * 2;
        temp_dmg_lvl++;
        temp_upgrade_cost = arcane_missile.DmgCostChangeAmount(temp_dmg_lvl);

        SetDmgText();
        SetRemainingShards();
        SetButtonInteractability();
    }

    //Decrease Level Button
    public void DecreaseDamageLevel()
    {
        temp_dmg_lvl--;
        temp_upgrade_cost = arcane_missile.DmgCostChangeAmount(temp_dmg_lvl);
        temp_shard_count += temp_upgrade_cost;

        temp_min_dmg -= arcane_missile.MinDmgChangeAmount(temp_dmg_lvl);
        temp_max_dmg = temp_min_dmg * 2;

        SetDmgText();
        SetRemainingShards();
        SetButtonInteractability();
    }

    //Confirm Button
    public void ConfirmDamageLevel()
    {
        arcane_missile.SetDamageStats(temp_min_dmg, temp_max_dmg, temp_dmg_lvl, temp_upgrade_cost);
        ArcaneShardManager.singleton.TakeArcaneShard(ArcaneShardManager.singleton.arcane_shard - temp_shard_count);

        SetTempVariables();
        SetRemainingShards();
        SetDmgText();
        SetButtonInteractability();
    }

    //Unlock Rune1 Button
    public void UnlockRune1()
    {
        if (temp_shard_count >= arcane_missile.rune1_slot_cost)
        {
            rune1.GetComponent<Image>().sprite = unlocked_slot_icon;
            rune1.GetComponent<Image>().color = Color.white;
            rune1.transform.GetChild(0).gameObject.SetActive(false);
            arcane_missile.rune1_unlocked = true;
            rune1.interactable = false;

            temp_shard_count -= arcane_missile.rune1_slot_cost;
            SetRemainingShards();
            ArcaneShardManager.singleton.TakeArcaneShard(ArcaneShardManager.singleton.arcane_shard - temp_shard_count);
            SetButtonInteractability();
        }
        else
        {
            panel_manager.PopupMessage("NOT ENOUGH SHARDS");
        }
    }

    //Unlock Rune2 Button
    public void UnlockRune2()
    {
        if (temp_shard_count >= arcane_missile.rune2_slot_cost)
        {
            rune2.GetComponent<Image>().sprite = unlocked_slot_icon;
            rune2.GetComponent<Image>().color = Color.white;
            rune2.transform.GetChild(0).gameObject.SetActive(false);
            arcane_missile.rune2_unlocked = true;
            rune2.interactable = false;

            temp_shard_count -= arcane_missile.rune2_slot_cost;
            SetRemainingShards();
            ArcaneShardManager.singleton.TakeArcaneShard(ArcaneShardManager.singleton.arcane_shard - temp_shard_count);
            SetButtonInteractability();
        }
        else
        {
            panel_manager.PopupMessage("NOT ENOUGH SHARDS");
        }
    }

    //Unlock BitterCold Button & equip/unequip
    public void UnlockBitterCold()
    {
        if (arcane_missile.bitter_cold_unlocked)
        {
            if (bitter_cold_cost_text.text == "EQUIP")
            {
                if (arcane_missile.rune1 == ArcaneMissile.Rune.None && arcane_missile.rune1_unlocked)
                {
                    arcane_missile.rune1 = ArcaneMissile.Rune.BitterCold;
                    rune1.GetComponent<Image>().sprite = bitter_cold_icon;
                    bitter_cold_cost_text.text = "UNEQUIP";
                }
                else if (arcane_missile.rune2 == ArcaneMissile.Rune.None && arcane_missile.rune2_unlocked)
                {
                    arcane_missile.rune2 = ArcaneMissile.Rune.BitterCold;
                    rune2.GetComponent<Image>().sprite = bitter_cold_icon;
                    bitter_cold_cost_text.text = "UNEQUIP";
                }
            }
            else if (bitter_cold_cost_text.text == "UNEQUIP")
            {
                if (arcane_missile.rune1 == ArcaneMissile.Rune.BitterCold)
                {
                    arcane_missile.rune1 = ArcaneMissile.Rune.None;
                    rune1.GetComponent<Image>().sprite = unlocked_slot_icon;
                    bitter_cold_cost_text.text = "EQUIP";
                }
                else if (arcane_missile.rune2 == ArcaneMissile.Rune.BitterCold)
                {
                    arcane_missile.rune2 = ArcaneMissile.Rune.None;
                    rune2.GetComponent<Image>().sprite = unlocked_slot_icon;
                    bitter_cold_cost_text.text = "EQUIP";
                }
            }
        }
        else
        {
            if (arcane_missile.rune1_unlocked == false && arcane_missile.rune2_unlocked == false)
            {
                panel_manager.PopupMessage("UNLOCK RUNE SLOT FIRST");
                return;
            }

            if (temp_shard_count < arcane_missile.bitter_cold_cost)
            {
                panel_manager.PopupMessage("NOT ENOUGH SHARDS");
                return;
            }

            arcane_missile.bitter_cold_unlocked = true;
            bitter_cold_coin_icon.SetActive(false);
            bitter_cold_cost_text.text = "EQUIP";
            temp_shard_count -= arcane_missile.bitter_cold_cost;
            ArcaneShardManager.singleton.TakeArcaneShard(ArcaneShardManager.singleton.arcane_shard - temp_shard_count);
            SetRemainingShards();
            SetButtonInteractability();
        }
    }

    //Unlock Nirvana Button & equip/unequip
    public void UnlockNirvana()
    {
        if (arcane_missile.nirvana_unlocked)
        {
            if (nirvana_cost_text.text == "EQUIP")
            {
                if (arcane_missile.rune1 == ArcaneMissile.Rune.None && arcane_missile.rune1_unlocked)
                {
                    arcane_missile.rune1 = ArcaneMissile.Rune.Nirvana;
                    rune1.GetComponent<Image>().sprite = nirvana_icon;
                    nirvana_cost_text.text = "UNEQUIP";
                }
                else if (arcane_missile.rune2 == ArcaneMissile.Rune.None && arcane_missile.rune2_unlocked)
                {
                    arcane_missile.rune2 = ArcaneMissile.Rune.Nirvana;
                    rune2.GetComponent<Image>().sprite = nirvana_icon;
                    nirvana_cost_text.text = "UNEQUIP";
                }
            }
            else if (nirvana_cost_text.text == "UNEQUIP")
            {
                if (arcane_missile.rune1 == ArcaneMissile.Rune.Nirvana)
                {
                    arcane_missile.rune1 = ArcaneMissile.Rune.None;
                    rune1.GetComponent<Image>().sprite = unlocked_slot_icon;
                    nirvana_cost_text.text = "EQUIP";
                }
                else if (arcane_missile.rune2 == ArcaneMissile.Rune.Nirvana)
                {
                    arcane_missile.rune2 = ArcaneMissile.Rune.None;
                    rune2.GetComponent<Image>().sprite = unlocked_slot_icon;
                    nirvana_cost_text.text = "EQUIP";
                }
            }
        }
        else
        {
            if (arcane_missile.rune1_unlocked == false && arcane_missile.rune2_unlocked == false)
            {
                panel_manager.PopupMessage("UNLOCK RUNE SLOT FIRST");
                return;
            }

            if (temp_shard_count < arcane_missile.nirvana_cost)
            {
                panel_manager.PopupMessage("NOT ENOUGH SHARDS");
                return;
            }

            arcane_missile.nirvana_unlocked = true;
            nirvana_coin_icon.SetActive(false);
            nirvana_cost_text.text = "EQUIP";
            temp_shard_count -= arcane_missile.nirvana_cost;
            ArcaneShardManager.singleton.TakeArcaneShard(ArcaneShardManager.singleton.arcane_shard - temp_shard_count);
            SetRemainingShards();
            SetButtonInteractability();
        }
    }

    //Unlock Split Button & equip/unequip
    public void UnlockSplit()
    {
        if (arcane_missile.split_unlocked)
        {
            if (split_cost_text.text == "EQUIP")
            {
                if (arcane_missile.rune1 == ArcaneMissile.Rune.None && arcane_missile.rune1_unlocked)
                {
                    arcane_missile.rune1 = ArcaneMissile.Rune.Split;
                    rune1.GetComponent<Image>().sprite = split_icon;
                    split_cost_text.text = "UNEQUIP";
                }
                else if (arcane_missile.rune2 == ArcaneMissile.Rune.None && arcane_missile.rune2_unlocked)
                {
                    arcane_missile.rune2 = ArcaneMissile.Rune.Split;
                    rune2.GetComponent<Image>().sprite = split_icon;
                    split_cost_text.text = "UNEQUIP";
                }
            }
            else if (split_cost_text.text == "UNEQUIP")
            {
                if (arcane_missile.rune1 == ArcaneMissile.Rune.Split)
                {
                    arcane_missile.rune1 = ArcaneMissile.Rune.None;
                    rune1.GetComponent<Image>().sprite = unlocked_slot_icon;
                    split_cost_text.text = "EQUIP";
                }
                else if (arcane_missile.rune2 == ArcaneMissile.Rune.Split)
                {
                    arcane_missile.rune2 = ArcaneMissile.Rune.None;
                    rune2.GetComponent<Image>().sprite = unlocked_slot_icon;
                    split_cost_text.text = "EQUIP";
                }
            }
        }
        else
        {
            if (arcane_missile.rune1_unlocked == false && arcane_missile.rune2_unlocked == false)
            {
                panel_manager.PopupMessage("UNLOCK RUNE SLOT FIRST");
                return;
            }

            if (temp_shard_count < arcane_missile.split_cost)
            {
                panel_manager.PopupMessage("NOT ENOUGH SHARDS");
                return;
            }
            arcane_missile.split_unlocked = true;
            split_coin_icon.SetActive(false);
            split_cost_text.text = "EQUIP";
            temp_shard_count -= arcane_missile.split_cost;
            ArcaneShardManager.singleton.TakeArcaneShard(ArcaneShardManager.singleton.arcane_shard - temp_shard_count);
            SetRemainingShards();
            SetButtonInteractability();
        }
    }
}
