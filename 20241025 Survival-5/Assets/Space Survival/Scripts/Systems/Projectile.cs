using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
//����ü
public class Projectile : MonoBehaviour
{
    public float damage = 10;//������
    public float moveSpeed = 5;//�̵��ӵ�
    public float duration = 3;//���ӽð�

    public int pierceCount = 0; //���� Ƚ��

    private CircleCollider2D coll;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        coll.enabled = false;
    }

    private void OnEnable() //start���� ���� ȣ�� �ǹǷ� �ʱ�ȭ�� �ȵ� ���¿����� ��Ȳ�� ����ؾ� ��
    {
        //Destroy(gameObject ,duration); //3�� �Ŀ� ������Ʈ ����
        //ProjectilePool.pool.Push(this, duration); //projectilePool�� ���� Ǯ�� �ǵ���
        //LeanPool.Despawn(this, duration); // 3�� �Ŀ� LeanPool�� ���� Ǯ�� �ǵ���
    }

    //Overlapcircle �޼��带 ���� ������ ���� �ִ� �ݶ��̴��� ���� List
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
                    //��ȿ�� Ÿ���� �߻���
                    //Debug.Log($"Contacted Collider : {contactedColl.name}");
                    contactedColl.SendMessage("TakeDamage", damage);    
                    contactedColls.Add(contactedColl);
                    pierceCount--;
                    if (pierceCount == 0)
                    {
                        //���� Ƚ���� 0�� �Ǹ� ����
                        //�̷��� �ϸ� pierceCount�� 0�϶� ���Ѱ������� �� �� ����
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
