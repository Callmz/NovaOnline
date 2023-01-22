using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack
{
    // Espaço de sobra na pilha
    public int remainStack;
    public Item item;
    public int amount { get; private set; }

    // Limite de empilhamento
    private int maxStack;

    public Stack(Item item = null, int amount = 1)
    {
        if (item == null) { this.amount = 0; }
        else
        {
            this.item = item;
            maxStack = 999;

            // Ferramentas não estacam
            if (item.type == Item.itemType.Tool) { maxStack = 1; }

            if (amount > maxStack) { throw new UnityException($"Trying to add {amount} '{item.name}', but maximum stack for '{item.type}' is {maxStack}."); }

            this.amount = amount;
            
            // Calculando quando espaço ainda tem de sobra
            remainStack = maxStack - this.amount;
        }
    }

    // Função para adicionar mais itens na pilha
    public void addItem(int amount)
    {
        // Tentar colocar mais que o limite, gera erro
        if (amount > remainStack) { throw new UnityException($"Trying to add {amount} '{item.name}', but maximum stack for '{item.type}' is {maxStack}."); }
        this.amount += amount;

        // Tentar tirar mais do que o que tem, não gera erro
        if (this.amount < 0) { this.amount = 0; }

        // Como sempre, calculando o espaço restante
        remainStack = maxStack - this.amount;
    }

    // Função para mudar diretamente o tamanho da pilha atualizando o espaço restante e impondo limites
    public void setAmount(int amount)
    {
        if (amount > maxStack) { throw new UnityException($"Tryint to set amount to '{amount}', but max stack is '{maxStack}'."); }
        if (amount < 0) { throw new UnityException($"Stack amount cannot be lower than zero."); }

        this.amount = amount;
        remainStack = maxStack - this.amount;
    }
}
