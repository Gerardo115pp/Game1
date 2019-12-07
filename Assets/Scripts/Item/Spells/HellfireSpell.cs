using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Hellfire Spell", menuName = "Inventory/Spell/Hellfire")]
public class HellfireSpell : Spell
{
    public GameObject fireball_obj;

    public override void cast(Vector3 target)
    {
        AudioManager.audioManager.play("Hellfire");
        base.cast(target);
        Caster inicial_point = PlayerController.player.caster;
        Instantiate(fireball_obj, inicial_point.transform.position, inicial_point.transform.rotation);
    }

    public override void affectTarget(Collider other)
    {
        CharacterStats character_stats = other.GetComponentInParent<CharacterStats>();
        if(character_stats != null)
        {
            character_stats.takeDamage(this.damage);
        }
    }
}
