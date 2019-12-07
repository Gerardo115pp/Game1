using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Teleportation Spell", menuName = "Inventory/Spell/Teleport")]
public class TeleportSpell : Spell
{
    public override void cast(Vector3 target)
    {
        base.cast(target);
        AudioManager.audioManager.play("Teleport_sound");
        PlayerController.player.transform.position = target;
    }
}
