using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
//투사체
public class Projectile : MonoBehaviour
{
    public float damage = 10;//데미지
    public float moveSpeed = 5;//이동속도
    public float duration = 3;//지속시간

    public int pierceCount = 0; //관통 횟수

    private CircleCollider2D coll;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        coll.enabled = false;
    }

    private void OnEnable() //start보다 먼저 호출 되므로 초기화가 안된 상태에서의 상황을 고려해야 함
    {
        //Destroy(gameObject ,duration); //3초 후에 오브젝트 제거
        //ProjectilePool.pool.Push(this, duration); //projectilePool을 통해 풀을 되돌림
        //LeanPool.Despawn(this, duration); // 3초 후에 LeanPool을 통해 풀에 되돌림
    }

    //Overlapcircle 메서드를 통해 감지한 적이 있는 콜라이더를 담을 List
    List<Collider2D> contactedColls = new();

    private void Update()
    {
        Move(Vector2.up);
        Collider2D contactedColl = Physics2D.OverlapCircle(transform.position, coll.radius);
        if (contactedColl != null)
        {
            if (contactedColl.CompareTag("Enemy"))
            {
                if (contactedColls.Contains(contactedColl) == false)
                {
                    //유효한 타격이 발생함
                    //Debug.Log($"Contacted Collider : {contactedColl.name}");
                    contactedColl.SendMessage("TakeDamage", damage);    
                    contactedColls.Add(contactedColl);
                    pierceCount--;
                    if (pierceCount == 0)
                    {
                        //관통 횟수가 0이 되면 삭제
                        //이렇게 하면 pierceCount가 0일때 무한관통으로 쓸 수 있음
                        //ProjectilePool.pool.Push(this);
                        LeanPool.Despawn(this);


                    }
                }
            }
        }

    }

    private void OnDisable()
    {
        contactedColls.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, coll.radius);
    }

    public void Move(Vector2 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            //ProjectilePool.pool.Push(this);
            LeanPool.Despawn(this);
        }
    }

}
