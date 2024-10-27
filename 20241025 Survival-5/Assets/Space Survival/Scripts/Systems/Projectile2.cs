using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    public float damage = 10; // ������
    public float moveSpeed = 5; // �̵� �ӵ�
    public float duration = 3; // ���� �ð�
    public float rotationSpeed = 360; // ȸ�� �ӵ�

    public int pierceCount = 0; // ���� Ƚ��

    private CircleCollider2D coll;
    private Transform playerTransform; // �÷��̾��� Transform

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        coll.enabled = false;
    }

    private void OnEnable()
    {
        // �÷��̾��� Transform�� �����ɴϴ�.
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // ����ü�� �ʱ� ��ġ�� �÷��̾� �ֺ����� �����մϴ�.
        transform.position = playerTransform.position + new Vector3(0, 1, 0); // �÷��̾� �������� ����
    }

    List<Collider2D> contactedColls = new();

    private void Update()
    {
        RotateAroundPlayer();

        // �浹 ���� ����
        Collider2D contactedColl = Physics2D.OverlapCircle(transform.position, coll.radius);
        if (contactedColl != null)
        {
            if (contactedColl.CompareTag("Enemy"))
            {
                if (!contactedColls.Contains(contactedColl))
                {
                    contactedColl.SendMessage("TakeDamage", damage);
                    contactedColls.Add(contactedColl);
                    pierceCount--;
                    if (pierceCount == 0)
                    {
                        LeanPool.Despawn(this);
                    }
                }
            }
        }
    }

    private void RotateAroundPlayer()
    {
        // �÷��̾ �߽����� ȸ���մϴ�.
        transform.RotateAround(playerTransform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        contactedColls.Clear();
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, coll.radius);
    }*/
}
