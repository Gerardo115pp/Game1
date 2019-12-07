using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;
    Item item;

    public void setItem(Item new_item)
    {
        this.item = new_item;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void clearSelf()
    {
        this.item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void useItem()
    {
        if (item != null)
        {
            item.onUiClicked();
        }

    }
}
