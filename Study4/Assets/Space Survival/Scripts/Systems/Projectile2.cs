using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    public float damage = 10; // 데미지
    public float moveSpeed = 5; // 이동 속도
    public float duration = 3; // 지속 시간
    public float rotationSpeed = 360; // 회전 속도

    public int pierceCount = 0; // 관통 횟수

    private CircleCollider2D coll;
    private Transform playerTransform; // 플레이어의 Transform

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        coll.enabled = false;
    }

    private void OnEnable()
    {
        // 플레이어의 Transform을 가져옵니다.
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // 투사체의 초기 위치를 플레이어 주변으로 설정합니다.
        transform.position = playerTransform.position + new Vector3(0, 1, 0); // 플레이어 위쪽으로 시작
    }

    List<Collider2D> contactedColls = new();

    private void Update()
    {
        RotateAroundPlayer();

        // 충돌 감지 로직
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
        // 플레이어를 중심으로 회전합니다.
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
