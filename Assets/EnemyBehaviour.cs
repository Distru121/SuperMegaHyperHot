using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public enum States
    {
        idle,
        chase,
        goback
    }

    public States currentState = States.idle;

    public Transform player;
    public float detectionRadius = 5f;
    public float stoppingDistance = 1f;

    Vector3 startingPosition;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startingPosition = transform.position;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, startingPosition);

        switch(currentState)
        {
            case States.idle:
                IdleBehaviour(distanceToPlayer);
            break;
            case States.chase:
                ChaseBehaviour(distanceToPlayer);
            break;
            case States.goback:
                GoBackBehaviour(distanceToPlayer);
            break;
        }
    }

    private void IdleBehaviour(float distanceToPlayer)
    {
        if(distanceToPlayer <= detectionRadius)
        {
            currentState = States.chase;
        }
        else
        {
            agent.isStopped = true;
        }
    }

    public void ChaseBehaviour(float distanceToPlayer)
    {
        if(distanceToPlayer > detectionRadius*3)
        {
            currentState = States.goback;
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
    }

    public void GoBackBehaviour(float distanceToPlayer)
    {
        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if(!agent.hasPath || agent.velocity.magnitude <= 0.01f)
                currentState = States.idle;
        }
        else
        {
            agent.SetDestination(startingPosition);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius*2);
    }
}
