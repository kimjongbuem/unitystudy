using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType { USE, EQUIPIMENT, QUEST, ETC, SPECIAL};

    public int itemID;
    public int itemCount;
    public string itemName;
    public string itemDescrition;
    public Sprite icon;
    public ItemType itemType;
    

    public Item(int _itemID, int _itemCount, string _itemName, string _itemDescrition, ItemType _itemType)
    {

    }
}
