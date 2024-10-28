using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public float barrierDurability; //배리어 내구도
    public float barrierDuration; //배리어 지속시간
    public float barrierDamage; //반사딜 
    //private float preBarrierTime; //마지막 배리어 시간
    //public float barrierInterval; //배리어 쿨타임
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
    //Debug.Log("코루틴 시작");
    ////isBarrierActive = true;
    //Instantiate(barrierPrefab, GameManager.Instance.player.transform.position, Quaternion.identity);
    //Debug.Log("코루틴 시작22");

    //yield return new WaitForSeconds(barrierDuration);
    //Debug.Log("코루틴 종료");
    //Destroy(gameObject);
    ////isBarrierActive = false;
    //}
    //}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(barrierDamage);
            //여기에 AddForce 추가
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
