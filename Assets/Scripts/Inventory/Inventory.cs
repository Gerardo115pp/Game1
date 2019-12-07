using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory inventory;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    void Awake()
    {
        if(inventory != null)
        {
            Debug.LogError("Inventory already inicialized");
        }
        Inventory.inventory = this;
    }

    public List<Item> items;
    // Start is called before the first frame update
    void Start()
    {
        this.items = new List<Item>();
    }

    public void AddItem(Item new_item)
    {
        if(!new_item.isDefault)
        {
            this.items.Add(new_item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }
    }

    public Item getItem(int index)
    {
        Item return_item = null;
        if(index < this.items.Count && index >= 0)
        {
            return_item = this.items[index];
        }
        return return_item;
    }
}
