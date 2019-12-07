using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;
    public Stat armor;
    [SerializeField]
    protected int max_health = 0;
    [SerializeField]
    protected int health;

    public delegate void onDamageTaken();
    public onDamageTaken onDamageTakenCallback;

    public delegate void onAgony();
    public onAgony onAgonyCallback;

    // Start is called before the first frame update
    void Awake()
    {
        this.health = max_health;
    }

    public virtual void die()
    {
        if(onAgonyCallback != null)
        {
            onAgonyCallback.Invoke();
        }
        Destroy(gameObject);
    }

    public virtual float takeDamage(int damage, bool is_player=false)
    {
        damage = Math.Abs(damage - (damage * (this.armor.BaseValue / 100)));
        if (damage > 0)
        {
            this.health -= damage;
            if (this.onDamageTakenCallback != null)
            {
                this.onDamageTakenCallback.Invoke();
            }
            if (this.health <= 0)
            {
                if (!is_player)
                {
                    this.die();
                }
            }
        }
        return (int)(damage * 100) / this.max_health;

    }

    public int Health
    {
        get { return this.health; }
        set 
        {
            if(value > 0)
            {
                this.health += value;
                Mathf.Clamp(this.health, 0f, this.max_health);

            }
        }
    }

}
