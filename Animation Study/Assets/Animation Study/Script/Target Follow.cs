using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    //���� Ÿ��
    public Transform target;

    //�������� ���� ������
    public bool followPosition;
    //ȸ���� ���� ������
    public bool followRotation;

    private void Update()
    {
        if (target == null) return;
        if (followPosition) transform.position = target.position;
        if (followPosition) transform.rotation = target.rotation;
    }
}
