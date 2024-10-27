using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Transform target; //������ ��� ��ġ
    //���� ���� �ִ� Ÿ�� ���, ������ ��ġ�� ����
    public MissileProjectile projectilePrefab;

    public float damage;
    public float projectileSpeed;
    public float projectileScale;
    public float shotInterval;

    public float MaxDist; //�ִ� Ÿ�� �Ÿ�
    //lkl lkl lkl lkl lkl lkl lkl lkl lkl lkl lkl lkl lkl lkl lkl

    private void Start()
    {
        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotInterval);
            Fire();
        }
    }

    private void Fire()
    {
        //���� Vector2 �������� ���ؼ�, ����ü�� ����

        Vector2 pos = Random.insideUnitCircle * MaxDist;

        MissileProjectile proj = Instantiate(projectilePrefab, pos, Quaternion.identity);

        proj.damage = this.damage;
        proj.duration = 1 / projectileSpeed;
    }
}

