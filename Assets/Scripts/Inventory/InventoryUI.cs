using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform ItemsParent;
    public GameObject inventoryUi;

    private Inventory inventory;
    private InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        this.inventory = Inventory.inventory;
        this.inventory.onItemChangedCallback += updateUi;
        this.slots = this.ItemsParent.GetComponentsInChildren<InventorySlot>();
        inventoryUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            this.inventoryUi.SetActive(!this.inventoryUi.activeSelf);
        }
    }

    void updateUi()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)  
            {
                slots[i].setItem(inventory.items[i]); 
            }
            else
            {
                slots[i].clearSelf();
            }
        }
    }
}
