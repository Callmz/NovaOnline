using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public GameObject cursor;
    public GameObject destinyHud;

    private Rigidbody rig;
    private NavMeshAgent navMeshAgent;
    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        inventory = new Inventory(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (destinyHud.activeSelf)
        {
            navMeshAgent.destination = destinyHud.transform.position;
            destinyHud.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            int rand = Random.Range(1, 4);
            int quant = Random.Range(1, 1000);

            Stack drop;

            //if (rand == 1)
            //{
            //    drop = new Stack(new Item("Maçã", Item.itemType.Consumable), quant);
            //}
            //else if (rand == 2)
            //{
            //    drop = new Stack(new Item("Madeira", Item.itemType.Material), quant);
            //}
            //else
            //{
            //    drop = new Stack(new Item("Pá", Item.itemType.Tool), 1);
            //}

            //int rest;
            drop = new Stack(new Item("Maçã", Item.itemType.Consumable), 108);
            Debug.Log($"Drop: {drop.amount}");
            inventory.AddStack(drop);//, out rest);


            //drop.setAmount(rest);

            Debug.Log($"==========INVENTORY==========");
            foreach (Stack slot in inventory.slots)
            {
                Debug.Log($"{slot.item.name} ({slot.amount})");
            }
            Debug.Log($"=============================");

            Debug.Log($"Resto: {drop.amount}");
        }
    }
}
