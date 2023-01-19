using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public Camera cam;
    public GameObject cursor;
    public GameObject destinyHud;

    private Rigidbody rig;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (destinyHud.activeSelf)
        {
            navMeshAgent.destination = destinyHud.transform.position;
        }
    }
}
