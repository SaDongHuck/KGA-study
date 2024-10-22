using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //public float hp = 10f; //채력
    private float maxhp;
    public float hp = 10f;
    public float moveSpeed = 5f; //이동속도
    public float damage = 10f; //공격력

    //초고수
    public float hpamount { get {  return hp / maxhp; } } //자주 계산되는 항목은 프로퍼티로 만들기

    private Transform target; //추적할 대상

    public Image hpBar;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
        maxhp = hp;
    }

    private void Update()
    {
        Vector2 MoveDir = target.position - transform.position;
        Move(MoveDir.normalized);
        //print(MoveDir.magnitude);//vector.magnitude : 해당 백터가 "방향백터로" 간주될 때, 백터의 길이
        //print(MoveDir.normalized);

        hpBar.fillAmount = hpamount;
    }

    private void Move(Vector2 dir)//dir 값이 커져도 1로 고정을 하고 싶은 경우
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    //OnHit,
    public void TakeDamage(float Damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.IncrementKillCount();
        }
    }

}
