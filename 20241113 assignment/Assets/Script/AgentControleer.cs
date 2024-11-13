using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentControleer : MonoBehaviour
{
    public Transform pointer;
    private NavMeshAgent agent;
    public bool isStop;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();   
    }

    private void Update()
    {
        agent.SetDestination(pointer.position);
        agent.isStopped = isStop;
    }
}
