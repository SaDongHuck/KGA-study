using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//투사체
public class Homing : MonoBehaviour
{
    public float damage = 10;//데미지
    public float moveSpeed = 5;//이동속도
    public float rotateSpeed;

    public CircleCollider2D coll;
    public Rotater rotater;

    private void Update()
    {
        Enemy targetEnemey = null; //대상으로 지정된 적
        float targetDistance = float.MaxValue; //대상과의 거리

        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            //Distance : 두 오브젝트간의 거리를 구해주는 메서드
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < targetDistance) //이전에 비교한 적보다 가까우면
            {
                //가장 가까운 거리를 구함
                targetDistance = distance;
                //가장 가까운 거리에 위치한 적을 타겟으로 지정OnLevelUp
                targetEnemey = enemy;
            }
        }
        Vector2 fireDir = Vector2.zero;
        if (targetEnemey != null) //타겟이 있다면
            fireDir = targetEnemey.transform.position - transform.position;
        Vector2 nomalizedfiredir = fireDir.normalized;
        Move(nomalizedfiredir);

        //ScriptableData 쓰니까 로테이터 없는 객체들은 Null예외떠서 제외
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