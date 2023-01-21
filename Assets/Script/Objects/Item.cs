using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public itemType type { get; private set; }

    public string name { get; private set; }

    public Item(string name, itemType type)
    {
        this.type = type;
        this.name = name;
    }
    public enum itemType
    {
        Consumable,
        Material,
        Tool
    }
}
