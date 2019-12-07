using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "defualt name";
    public Sprite icon;
    public bool isDefault = false;

    public virtual void Use()
    {
        // Use the item
        // Something might happen
    }

    public virtual void onUiClicked()
    {

    }
}
