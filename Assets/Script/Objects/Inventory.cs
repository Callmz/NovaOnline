using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Stack> slots { get; private set; }
    private GameObject owner;
    public int space { get; private set; }

    public Inventory(GameObject owner)
    {
        slots = new List<Stack>();
        space = 1;
    }

    public void AddStack(Stack item)//, out int rest)
    {
        Stack i = new Stack(item.item, item.amount);

        int NeedToAdd = item.amount;

        while (NeedToAdd > 0)
        {
            int ValueToAdd; 
            
            Stack desiredSlot = slots.Find(a => (a.item.name == item.item.name) && (a.remainStack > 0));

            if (desiredSlot != null)
            {
                if (NeedToAdd > desiredSlot.remainStack)
                {
                    ValueToAdd = desiredSlot.remainStack;
                }
                else
                {
                    ValueToAdd = NeedToAdd;
                }
                NeedToAdd -= ValueToAdd;

                desiredSlot.addItem(ValueToAdd);
            }
            else if (slots.Count < space)
            {
                slots.Add(new Stack(new Item(item.item.name, item.item.type), NeedToAdd));
                NeedToAdd = 0;
            }
            else break;
        }

        item.setAmount(NeedToAdd);
    }
}
