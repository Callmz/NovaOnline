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
            // Move o player até o lugar clicado
            navMeshAgent.destination = destinyHud.transform.position;
        }
    }
}
