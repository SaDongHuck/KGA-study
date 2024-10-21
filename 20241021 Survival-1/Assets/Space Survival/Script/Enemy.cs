using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //public float hp = 10f; //ä��
    private float maxhp;
    public float hp = 10f;
    public float moveSpeed = 5f; //�̵��ӵ�
    public float damage = 10f; //���ݷ�

    //�ʰ��
    public float hpamount { get {  return hp / maxhp; } } //���� ���Ǵ� �׸��� ������Ƽ�� �����

    private Transform target; //������ ���

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
        //print(MoveDir.magnitude);//vector.magnitude : �ش� ���Ͱ� "������ͷ�" ���ֵ� ��, ������ ����
        //print(MoveDir.normalized);

        hpBar.fillAmount = hpamount;
    }

    private void Move(Vector2 dir)//dir ���� Ŀ���� 1�� ������ �ϰ� ���� ���
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
