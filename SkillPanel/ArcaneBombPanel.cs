using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArcaneBombPanel : SkillPanel
{
    //Refrence to the arcane missile skill attached to player
    ArcaneBomb arcane_bomb;

    [Header("Damage Upgrade Stuff")]
    public TextMeshProUGUI shard_count_text;
    public TextMeshProUGUI damage_text;

    int temp_shard_count;
    int temp_min_dmg;
    int temp_max_dmg;
    //int temp_upgrade_cost;
    //int temp_dmg_lvl;

    [Space(20)]
    [Header("Rune Upgrade Stuff")]
    public Button rune1;
    public Button rune2;

    public Button tremendous_unlock_button;
    public Button bounce_button;
    public Button gas_button;
    public GameObject tremendous_coin_icon;
    public GameObject bounce_coin_icon;
    public GameObject gas_coin_icon;

    public Sprite unlocked_slot_icon;
    public Sprite tremendous_icon;
    public Sprite bounce_icon;
    public Sprite gas_icon;

    public TextMeshProUGUI rune1_cost_text;
    public TextMeshProUGUI rune2_cost_text;
    public TextMeshProUGUI tremendous_cost_text;
    public TextMeshProUGUI bounce_cost_text;
    public TextMeshProUGUI gas_cost_text;


    void OnEnable()
    {
        if (arcane_bomb == null)
        {
            arcane_bomb = ArcaneBomb.singleton;
            SetRuneCostText();
        }
        SetTempVariables();
        SetRemainingShards();
        SetDmgText();
        //SetButtonInteractability();
    }

    void SetRemainingShards()
    {
        shard_count_text.text = "" + temp_shard_count;
    }

    void SetTempVariables()
    {
        temp_shard_count = ArcaneShardManager.singleton.arcane_shard;
        temp_min_dmg = arcane_bomb.MinDmg();
        temp_max_dmg = arcane_bomb.MaxDmg();
        //temp_upgrade_cost = arcane_bomb.damage_upgrade_cost;
        //temp_dmg_lvl = arcane_bomb.current_damage_level;
    }

    void SetRuneCostText()
    {
        rune1_cost_text.text = "" + arcane_bomb.rune1_slot_cost;
        rune2_cost_text.text = "" + arcane_bomb.rune2_slot_cost;
        tremendous_cost_text.text = "" + arcane_bomb.tremendous_cost;
        bounce_cost_text.text = "" + arcane_bomb.bounce_cost;
        gas_cost_text.text = "" + arcane_bomb.gas_cost;
    }

    void SetDmgText()
    {
        damage_text.text = temp_min_dmg + "~" + temp_max_dmg;
        /*
        if (temp_min_dmg == arcane_bomb.min_dmg)
        {
            damage_text.color = Color.white;
            confirm_button.interactable = false;
        }
        else
        {
            damage_text.color = Color.green;
            confirm_button.interactable = true;
        }
        */
    }

    /*
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

        if (temp_min_dmg == arcane_bomb.min_dmg)
        {
            decrease_damage_button.interactable = false;
        }
        else
        {
            decrease_damage_button.interactable = true;
        }
    }
    
    //Increase Level Button
    public void IncreaseDamageLevel()
    {
        temp_shard_count -= temp_upgrade_cost;

        temp_min_dmg += arcane_bomb.MinDmgChangeAmount(temp_dmg_lvl);
        temp_max_dmg = temp_min_dmg * 2;
        temp_dmg_lvl++;
        temp_upgrade_cost = arcane_bomb.DmgCostChangeAmount(temp_dmg_lvl);

        SetDmgText();
        SetRemainingShards();
        SetButtonInteractability();
    }

    //Decrease Level Button
    public void DecreaseDamageLevel()
    {
        temp_dmg_lvl--;
        temp_upgrade_cost = arcane_bomb.DmgCostChangeAmount(temp_dmg_lvl);
        temp_shard_count += temp_upgrade_cost;

        temp_min_dmg -= arcane_bomb.MinDmgChangeAmount(temp_dmg_lvl);
        temp_max_dmg = temp_min_dmg * 2;

        SetDmgText();
        SetRemainingShards();
        SetButtonInteractability();
    }

    //Confirm Button
    public void ConfirmDamageLevel()
    {
        arcane_bomb.SetDamageStats(temp_min_dmg, temp_max_dmg, temp_dmg_lvl, temp_upgrade_cost);
        ArcaneShardManager.singleton.TakeArcaneShard(ArcaneShardManager.singleton.arcane_shard - temp_shard_count);

        SetTempVariables();
        SetRemainingShards();
        SetDmgText();
        SetButtonInteractability();
    }
    */
    //Unlock Rune1 Button
    public void UnlockRune1()
    {
        if (temp_shard_count >= arcane_bomb.rune1_slot_cost)
        {
            rune1.GetComponent<Image>().sprite = unlocked_slot_icon;
            rune1.GetComponent<Image>().color = Color.white;
            rune1.transform.GetChild(0).gameObject.SetActive(false);
            arcane_bomb.rune1_unlocked = true;
            rune1.interactable = false;

            temp_shard_count -= arcane_bomb.rune1_slot_cost;
            SetRemainingShards();
            ArcaneShardManager.singleton.TakeArcaneShard(ArcaneShardManager.singleton.arcane_shard - temp_shard_count);
            //SetButtonInteractability();
        }
        else
        {
            panel_manager.PopupMessage("NOT ENOUGH SHARDS");
        }
    }

    //Unlock Rune2 Button
    public void UnlockRune2()
    {
        if (temp_shard_count >= arcane_bomb.rune2_slot_cost)
        {
            rune2.GetComponent<Image>().sprite = unlocked_slot_icon;
            rune2.GetComponent<Image>().color = Color.white;
            rune2.transform.GetChild(0).gameObject.SetActive(false);
            arcane_bomb.rune2_unlocked = true;
            rune2.interactable = false;

            temp_shard_count -= arcane_bomb.rune2_slot_cost;
            SetRemainingShards();
            ArcaneShardManager.singleton.TakeArcaneShard(ArcaneShardManager.singleton.arcane_shard - temp_shard_count);
            //SetButtonInteractability();
        }
        else
        {
            panel_manager.PopupMessage("NOT ENOUGH SHARDS");
        }
    }

    //Unlock Tremendous Button & equip/unequip
    public void UnlockTremendous()
    {
        if (arcane_bomb.tremendous_unlocked)
        {
            if (tremendous_cost_text.text == "EQUIP")
            {
                if (arcane_bomb.rune1 == ArcaneBomb.Rune.None && arcane_bomb.rune1_unlocked)
                {
                    arcane_bomb.rune1 = ArcaneBomb.Rune.Tremendous;
                    rune1.GetComponent<Image>().sprite = tremendous_icon;
                    tremendous_cost_text.text = "UNEQUIP";
                }
                else if (arcane_bomb.rune2 == ArcaneBomb.Rune.None && arcane_bomb.rune2_unlocked)
                {
                    arcane_bomb.rune2 = ArcaneBomb.Rune.Tremendous;
                    rune2.GetComponent<Image>().sprite = tremendous_icon;
                    tremendous_cost_text.text = "UNEQUIP";
                }
            }
            else if (tremendous_cost_text.text == "UNEQUIP")
            {
                if (arcane_bomb.rune1 == ArcaneBomb.Rune.Tremendous)
                {
                    arcane_bomb.rune1 = ArcaneBomb.Rune.None;
                    rune1.GetComponent<Image>().sprite = unlocked_slot_icon;
                    tremendous_cost_text.text = "EQUIP";
                }
                else if (arcane_bomb.rune2 == ArcaneBomb.Rune.Tremendous)
                {
                    arcane_bomb.rune2 = ArcaneBomb.Rune.None;
                    rune2.GetComponent<Image>().sprite = unlocked_slot_icon;
                    tremendous_cost_text.text = "EQUIP";
                }
            }
        }
        else
        {
            if (arcane_bomb.rune1_unlocked == false && arcane_bomb.rune2_unlocked == false)
            {
                panel_manager.PopupMessage("UNLOCK RUNE SLOT FIRST");
                return;
            }

            if (temp_shard_count < arcane_bomb.tremendous_cost)
            {
                panel_manager.PopupMessage("NOT ENOUGH SHARDS");
                return;
            }

            arcane_bomb.tremendous_unlocked = true;
            tremendous_coin_icon.SetActive(false);
            tremendous_cost_text.text = "EQUIP";
            temp_shard_count -= arcane_bomb.tremendous_cost;
            ArcaneShardManager.singleton.TakeArcaneShard(ArcaneShardManager.singleton.arcane_shard - temp_shard_count);
            SetRemainingShards();
            //SetButtonInteractability();
        }
    }

    //Unlock bounce Button & equip/unequip
    public void Unlockbounce()
    {
        if (arcane_bomb.bounce_unlocked)
        {
            if (bounce_cost_text.text == "EQUIP")
            {
                if (arcane_bomb.rune1 == ArcaneBomb.Rune.None && arcane_bomb.rune1_unlocked)
                {
                    arcane_bomb.rune1 = ArcaneBomb.Rune.Bounce;
                    rune1.GetComponent<Image>().sprite = bounce_icon;
                    bounce_cost_text.text = "UNEQUIP";
                }
                else if (arcane_bomb.rune2 == ArcaneBomb.Rune.None && arcane_bomb.rune2_unlocked)
                {
                    arcane_bomb.rune2 = ArcaneBomb.Rune.Bounce;
                    rune2.GetComponent<Image>().sprite = bounce_icon;
                    bounce_cost_text.text = "UNEQUIP";
                }
            }
            else if (bounce_cost_text.text == "UNEQUIP")
            {
                if (arcane_bomb.rune1 == ArcaneBomb.Rune.Bounce)
                {
                    arcane_bomb.rune1 = ArcaneBomb.Rune.None;
                    rune1.GetComponent<Image>().sprite = unlocked_slot_icon;
                    bounce_cost_text.text = "EQUIP";
                }
                else if (arcane_bomb.rune2 == ArcaneBomb.Rune.Bounce)
                {
                    arcane_bomb.rune2 = ArcaneBomb.Rune.None;
                    rune2.GetComponent<Image>().sprite = unlocked_slot_icon;
                    bounce_cost_text.text = "EQUIP";
                }
            }
        }
        else
        {
            if (arcane_bomb.rune1_unlocked == false && arcane_bomb.rune2_unlocked == false)
            {
                panel_manager.PopupMessage("UNLOCK RUNE SLOT FIRST");
                return;
            }

            if (temp_shard_count < arcane_bomb.bounce_cost)
            {
                panel_manager.PopupMessage("NOT ENOUGH SHARDS");
                return;
            }

            arcane_bomb.bounce_unlocked = true;
            bounce_coin_icon.SetActive(false);
            bounce_cost_text.text = "EQUIP";
            temp_shard_count -= arcane_bomb.bounce_cost;
            ArcaneShardManager.singleton.TakeArcaneShard(ArcaneShardManager.singleton.arcane_shard - temp_shard_count);
            SetRemainingShards();
            //SetButtonInteractability();
        }
    }

    //Unlock gas Button & equip/unequip
    public void Unlockgas()
    {
        Debug.Log("gas Unlocked");
        if (arcane_bomb.gas_unlocked)
        {
            if (gas_cost_text.text == "EQUIP")
            {
                if (arcane_bomb.rune1 == ArcaneBomb.Rune.None && arcane_bomb.rune1_unlocked)
                {
                    arcane_bomb.rune1 = ArcaneBomb.Rune.Gas;
                    rune1.GetComponent<Image>().sprite = gas_icon;
                    gas_cost_text.text = "UNEQUIP";
                }
                else if (arcane_bomb.rune2 == ArcaneBomb.Rune.None && arcane_bomb.rune2_unlocked)
                {
                    arcane_bomb.rune2 = ArcaneBomb.Rune.Gas;
                    rune2.GetComponent<Image>().sprite = gas_icon;
                    gas_cost_text.text = "UNEQUIP";
                }
            }
            else if (gas_cost_text.text == "UNEQUIP")
            {
                if (arcane_bomb.rune1 == ArcaneBomb.Rune.Gas)
                {
                    arcane_bomb.rune1 = ArcaneBomb.Rune.None;
                    rune1.GetComponent<Image>().sprite = unlocked_slot_icon;
                    gas_cost_text.text = "EQUIP";
                }
                else if (arcane_bomb.rune2 == ArcaneBomb.Rune.Gas)
                {
                    arcane_bomb.rune2 = ArcaneBomb.Rune.None;
                    rune2.GetComponent<Image>().sprite = unlocked_slot_icon;
                    gas_cost_text.text = "EQUIP";
                }
            }
        }
        else
        {
            if (arcane_bomb.rune1_unlocked == false && arcane_bomb.rune2_unlocked == false)
            {
                panel_manager.PopupMessage("UNLOCK RUNE SLOT FIRST");
                return;
            }

            if (temp_shard_count < arcane_bomb.gas_cost)
            {
                panel_manager.PopupMessage("NOT ENOUGH SHARDS");
                return;
            }
            arcane_bomb.gas_unlocked = true;
            gas_coin_icon.SetActive(false);
            gas_cost_text.text = "EQUIP";
            temp_shard_count -= arcane_bomb.gas_cost;
            ArcaneShardManager.singleton.TakeArcaneShard(ArcaneShardManager.singleton.arcane_shard - temp_shard_count);
            SetRemainingShards();
            //SetButtonInteractability();
        }
    }
}
