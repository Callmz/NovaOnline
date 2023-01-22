using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Stack> slots { get; private set; }
    public int space { get; private set; }
    private GameObject owner;

    public Inventory(GameObject owner)
    {
        slots = new List<Stack>();
        space = 50;
    }

    public void AddStack(Stack item)
    {
        // Precisamos adicionar ao inventário (NeedToAdd) todos os itens que estamos tentando pegar
        int NeedToAdd = item.amount;

        while (NeedToAdd > 0)
        {
            // Será o valor que efetivamente iremos adicionar à pilha (Stack) de itens
            int ValueToAdd; 
            
            // Pega um Slot que já exista e que tem espaço sobrando na pilha
            Stack desiredSlot = slots.Find(i => (i.item.name == item.item.name) && (i.remainStack > 0));

            if (desiredSlot != null)
            {
                // Enche o máximo que puder, caso o valor seja muito alto ao ponto de superar o espaço sobrando
                if (NeedToAdd > desiredSlot.remainStack)
                {
                    ValueToAdd = desiredSlot.remainStack;
                }
                // Caso não ultrapasse o limite, apenas adicione o valor normalmente
                else
                {
                    ValueToAdd = NeedToAdd;
                }
                NeedToAdd -= ValueToAdd;

                desiredSlot.addItem(ValueToAdd);
            }
            // Se não, confere se tem vaga pra um novo slot e se aloja lá
            else if (slots.Count < space)
            {
                slots.Add(new Stack(new Item(item.item.name, item.item.type), NeedToAdd));
                NeedToAdd = 0;
            }
            // Não tem mais espaço sobrando no inventário, logo, não pegue nada
            else break;
        }

        // Botar o que sobrou de volta na pilha
        item.setAmount(NeedToAdd);
    }
}
