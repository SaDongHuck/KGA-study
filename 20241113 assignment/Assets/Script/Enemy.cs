using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public Transform target; // ������ ���
    private NavMeshAgent agent; // NavMeshAgent ������

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Ÿ���� ��ġ�� �������� ����
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
