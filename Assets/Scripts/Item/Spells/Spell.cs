using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Spell", menuName = "Inventory/Spell/Generic")]
public class Spell : Item
{
    public int damage;

    public override void onUiClicked()
    {
        base.onUiClicked();
        EquipmentManager.equipmentManager.EquipedSpell = this;
    }

    public virtual void cast(Vector3 target)
    {
    
    }

    public virtual void affectTarget(Collider target)
    {

    }
}
