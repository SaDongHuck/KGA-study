using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public Transform target; // 추적할 대상
    private NavMeshAgent agent; // NavMeshAgent 컴포넌

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // 타겟의 위치로 목적지를 설정
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
