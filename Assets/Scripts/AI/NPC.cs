using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    //=============================================================================
    // Author (kinda): Kevin Charron
    //=============================================================================

    // Patrol points
    [Header("Patrol points")]
    public Transform[] points;
    int destPoint = 0;

    [Header(" ")]
    protected NavMeshAgent agent;
    protected GameObject player;
    protected bool Patrolling;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        agent.autoBraking = false;
        //agent.stoppingDistance = 5f;
    }

    // https://docs.unity3d.com/Manual/nav-AgentPatrol.html i copy pasted a lot from this lul
    protected void Patrol()
    {
        if (points.Length == 0)
            return;
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;     
    }
    protected virtual void Update()
    {
        if (Patrolling)
        {
            foreach (Transform point in points)
            {
                if (agent.destination != point.position)
                {
                    Patrol();
                }
            }
            if (!agent.pathPending && agent.remainingDistance < 1.35f)
                Patrol();
        }
    }

}

