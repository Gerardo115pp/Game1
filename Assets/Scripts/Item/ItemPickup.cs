using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item; 

    public override void Interact()
    {
        base.Interact();
        Pickup();
    }

    void Pickup()
    {
        Inventory.inventory.AddItem(this.item);
        Destroy(gameObject);
    }
}
