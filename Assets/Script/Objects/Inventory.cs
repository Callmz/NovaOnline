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
        // Precisamos adicionar ao invent�rio (NeedToAdd) todos os itens que estamos tentando pegar
        int NeedToAdd = item.amount;

        while (NeedToAdd > 0)
        {
            // Ser� o valor que efetivamente iremos adicionar � pilha (Stack) de itens
            int ValueToAdd; 
            
            // Pega um Slot que j� exista e que tem espa�o sobrando na pilha
            Stack desiredSlot = slots.Find(i => (i.item.name == item.item.name) && (i.remainStack > 0));

            if (desiredSlot != null)
            {
                // Enche o m�ximo que puder, caso o valor seja muito alto ao ponto de superar o espa�o sobrando
                if (NeedToAdd > desiredSlot.remainStack)
                {
                    ValueToAdd = desiredSlot.remainStack;
                }
                // Caso n�o ultrapasse o limite, apenas adicione o valor normalmente
                else
                {
                    ValueToAdd = NeedToAdd;
                }
                NeedToAdd -= ValueToAdd;

                desiredSlot.addItem(ValueToAdd);
            }
            // Se n�o, confere se tem vaga pra um novo slot e se aloja l�
            else if (slots.Count < space)
            {
                slots.Add(new Stack(new Item(item.item.name, item.item.type), NeedToAdd));
                NeedToAdd = 0;
            }
            // N�o tem mais espa�o sobrando no invent�rio, logo, n�o pegue nada
            else break;
        }

        // Botar o que sobrou de volta na pilha
        item.setAmount(NeedToAdd);
    }
}
