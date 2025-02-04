using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waypoint : MonoBehaviour
{
    public Transform[] waypoints;
    public NavMeshAgent agent;
    int currentWaypointIndex;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if(waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    private void Update()
    {
        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}
