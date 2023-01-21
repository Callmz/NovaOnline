using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack
{
    public int remainStack;
    public Item item;
    public int amount { get; private set; }

    private int maxStack;

    public Stack(Item item = null, int amount = 1)
    {
        if (item == null) { this.amount = 0; Debug.Log("ITEM NULO"); }
        else
        {
            this.item = item;

            maxStack = 999;

            if (item.type == Item.itemType.Tool) { maxStack = 1; }

            if (amount > maxStack) { throw new UnityException($"Trying to add {amount} '{item.name}', but maximum stack for '{item.type}' is {maxStack}."); }

            this.amount = amount;

            remainStack = maxStack - this.amount;
        }
    }

    public void ToMaxStack()
    {
        amount = maxStack;
    }

    public void addItem(int amount)
    {
        if (amount > remainStack) { throw new UnityException($"Trying to add {amount} '{item.name}', but maximum stack for '{item.type}' is {maxStack}."); }
        this.amount += amount;
        if (this.amount < 0) { this.amount = 0; }
        remainStack = maxStack - this.amount;
    }

    public void setAmount(int amount)
    {
        if (amount > maxStack) { throw new UnityException($"Tryint to set amount to '{amount}', but max stack is '{maxStack}'."); }
        if (amount < 0) { throw new UnityException($"Stack amount cannot be lower than zero."); }
        this.amount = amount;
        remainStack = maxStack - this.amount;
    }
}
