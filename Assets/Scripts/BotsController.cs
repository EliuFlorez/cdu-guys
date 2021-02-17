using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Core
using Core;

public class BotsController : MonoBehaviour
{
    public Transform goals;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.state == Game.State.play)
        {
            _agent.destination = goals.position;
        }
    }
}
