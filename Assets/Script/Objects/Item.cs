using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Item", menuName = "Item/Create New Item")]

public class Item : ScriptableObject
{
    public itemType type;
    public string itemName;
    public Sprite icon;


    public Item(string name, itemType type)
    {
        this.type = type;
        this.itemName = name;
    }
    public enum itemType
    {
        Consumable,
        Material,
        Tool
    }
}
