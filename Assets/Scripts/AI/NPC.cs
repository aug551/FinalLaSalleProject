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
    [Header("")]

    int destPoint = 0;
    public NavMeshAgent agent;
    public GameObject player;
    protected bool Patrolling;

    private void Awake()
    {
        agent.autoBraking = false;
    }

    // https://docs.unity3d.com/Manual/nav-AgentPatrol.html i copy pasted a lot from this lul
    protected virtual void Patrol()
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
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                Patrol();
            Debug.Log("patrolling");
        }
    }

}

