using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public enum ItemType { USE, EQUIPIMENT, QUEST, ETC, SPECIAL};

    public int itemID;
    public int itemCount;
    public string itemName;
    public string itemDescrition;
    public Sprite icon;
    public ItemType itemType;
    

    public Item(int _itemID, string _itemName, string _itemDescrition, ItemType _itemType,int _itemCount = 1)
    {
        itemID = _itemID; itemName = _itemName; itemDescrition = _itemDescrition;
        itemCount = _itemCount; itemType = _itemType;
        icon = Resources.Load("item/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }
}
