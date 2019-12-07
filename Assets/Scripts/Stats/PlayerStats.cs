using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{

    public Image health_gui;
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.equipmentManager.onSpellEquipedChangeCallback += this.alterStats;
    }

    private void alterStats()
    {

    }

    public float MaxHealth
    {
        get { return this.max_health; }
    }

    public override float takeDamage(int damage,bool is_player=true)
    {
        float damage_percent = base.takeDamage(damage,true);
        float new_scale = (float)(this.health_gui.rectTransform.localScale.y -  (damage_percent/100));
        this.health_gui.rectTransform.localScale = new Vector3(1f, new_scale, 1f);
        if(this.health <= 0)
        {
            this.die();
        }
        return 0f;
    }

    public void healPlayer(int amount)
    {
        /*
        * The health setter in the character_stats class adds the value to the current healt this value can not be negative
        */
        this.Health = amount;
        float percent_healed = (amount * 100) / this.max_health;
        float new_y = Mathf.Clamp(this.health_gui.rectTransform.localScale.y + (percent_healed / 100), 0f, 1f);
        this.health_gui.rectTransform.localScale = new Vector3(1f, new_y, 1f);
    }

    public override void die()
    {
        GameStateController.stateController.playerDied();
        AudioManager.audioManager.play("Player_Dies");
        base.die();
    }
}
