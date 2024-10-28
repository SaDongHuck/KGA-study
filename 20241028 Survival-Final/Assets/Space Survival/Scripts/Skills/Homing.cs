using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//����ü
public class Homing : MonoBehaviour
{
    public float damage = 10;//������
    public float moveSpeed = 5;//�̵��ӵ�
    public float rotateSpeed;

    public CircleCollider2D coll;
    public Rotater rotater;

    private void Update()
    {
        Enemy targetEnemey = null; //������� ������ ��
        float targetDistance = float.MaxValue; //������ �Ÿ�

        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            //Distance : �� ������Ʈ���� �Ÿ��� �����ִ� �޼���
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < targetDistance) //������ ���� ������ ������
            {
                //���� ����� �Ÿ��� ����
                targetDistance = distance;
                //���� ����� �Ÿ��� ��ġ�� ���� Ÿ������ ����OnLevelUp
                targetEnemey = enemy;
            }
        }
        Vector2 fireDir = Vector2.zero;
        if (targetEnemey != null) //Ÿ���� �ִٸ�
            fireDir = targetEnemey.transform.position - transform.position;
        Vector2 nomalizedfiredir = fireDir.normalized;
        Move(nomalizedfiredir);

        //ScriptableData ���ϱ� �������� ���� ��ü���� Null���ܶ��� ����
        //rotater.rotateSpeed = this.rotateSpeed;
    }

    public void Move(Vector2 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
        }
    }
}