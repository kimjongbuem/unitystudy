using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventroySlot : MonoBehaviour
{
    public Image icon;
    public Text names;
    public Text item_count;
    public GameObject select_item;

    public void AddItem(Item _item)
    {
        names.text = _item.itemName;
        icon.sprite = _item.icon;
        if(_item.itemType == Item.ItemType.USE)
        if (_item.itemCount > 0)
            item_count.text = "x " + _item.itemCount.ToString();
        else item_count.text = "";
    }
    public void RemoveItem()
    {
        item_count.text = "";
        names.text = "";
        icon.sprite = null;
    }
}
