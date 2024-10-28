using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public float barrierDurability; //�踮�� ������
    public float barrierDuration; //�踮�� ���ӽð�
    public float barrierDamage; //�ݻ�� 
    //private float preBarrierTime; //������ �踮�� �ð�
    //public float barrierInterval; //�踮�� ��Ÿ��
    //private bool isBarrierActive;
    public PolygonCollider2D coll;
    public float knockBack;

    private void Awake()
    {
        coll = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        Destroy(gameObject, barrierDuration);
        //StartCoroutine(DisableBarrierCoroutine(barrierDuration));
    }

    //private IEnumerator DisableBarrierCoroutine(float duration)
    //{
    //    yield return new WaitForSeconds(duration);
    //    Destroy(gameObject);
    //}

    //private IEnumerator BarrierCoroutine(float barrierDuration)
    //{
    //while (true)
    //{
    //Debug.Log("�ڷ�ƾ ����");
    ////isBarrierActive = true;
    //Instantiate(barrierPrefab, GameManager.Instance.player.transform.position, Quaternion.identity);
    //Debug.Log("�ڷ�ƾ ����22");

    //yield return new WaitForSeconds(barrierDuration);
    //Debug.Log("�ڷ�ƾ ����");
    //Destroy(gameObject);
    ////isBarrierActive = false;
    //}
    //}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(barrierDamage);
            //���⿡ AddForce �߰�
            Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
            if (enemyRigidbody != null)
            {
                Vector2 forceDir = (enemyRigidbody.position - (Vector2)transform.position).normalized;
                enemyRigidbody.AddForce(forceDir * knockBack);
            }
            TakeDamage(enemy.damage);
        }
    }

    private void TakeDamage(float enemyDamage)
    {
        barrierDurability = barrierDurability - enemyDamage;
        if (barrierDurability <= 0)
        {
            Destroy(gameObject);
        }
    }
}
