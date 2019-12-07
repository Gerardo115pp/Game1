using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager equipmentManager;
    public Image equipped_spell_ui;

    public delegate void onSpellEquipedChange();
    public onSpellEquipedChange onSpellEquipedChangeCallback;

    private Spell equipedSpell;
    
    void Awake()
    {
        if (EquipmentManager.equipmentManager != null)
        {
            Debug.LogError("EquimentManager already inicialized");
        }

        EquipmentManager.equipmentManager = this;
    }

    public Spell EquipedSpell
    {
        set
        {
            this.equipedSpell = value;
            onSpellEquipedChangeCallback.Invoke();
            equipped_spell_ui.enabled =  true;
            equipped_spell_ui.sprite = value.icon;
        }
        get
        {
            return this.equipedSpell;
        }
    }
}
