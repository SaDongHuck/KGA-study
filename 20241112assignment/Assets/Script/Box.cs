using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        // Player ���̾� ��ȣ�� ���� �� (8�� ����)
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // ���ϴ� ��ȣ�ۿ� �ڵ� �ۼ�
            print("Player�� Box �浹 �߻�!");
        }

    }
}
