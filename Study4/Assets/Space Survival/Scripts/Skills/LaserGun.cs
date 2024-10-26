using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using Lean.Pool;

//�⺻���� : ����ü�� �߻��ϴ� ��ų
public class LaserGun1 : MonoBehaviour
{
    public Transform target; //����ü�� ���ؾ� �� ���⿡ �ִ� ���
    public Projectile projectilePrefab; //�߻��� ����ü�� ������
    public ProjectilePool projpool; //�������� ������ Ǯ �Ŵ���

    public float damage;          //����ü �����
    public float projectileSpeed; //����ü �ӵ�
    public float projectileScale; //����ü ũ��
    public float shotInterval;    //���� ��Ÿ��

    public int projectileCount;   //����ü ����
    public float innerInterval;   //���� ����
    [Tooltip("���� Ƚ��\n0 to Infinity")]
    public int pierceCount;       //����ü �����

    protected virtual void Start()
    {
        StartCoroutine(FireCoroutine());
    }

    protected virtual void Update()
    {
        if (target == null) { return; }
        transform.up = target.position - transform.position; //���� ����̿���
    }

    protected virtual IEnumerator FireCoroutine() //���� �ڷ�ƾ
    {
        while (true)
        {
            yield return new WaitForSeconds(shotInterval);
            //1. ����ü ������ �ö󰡸� 0.05�� �������� ����ü ������ŭ �߻� �ݺ�
            for (int i = 0; i < projectileCount; i++)
            {
                Fire();
                yield return new WaitForSeconds(innerInterval);
            }
            //Fire();
        }
    }

    protected virtual void Fire()
    {
        Projectile proj = //Instantiate(projectilePrefab, transform.position, transform.rotation);
        //�����̼��� �Ȱ��� �༭ ���� �ٶ󺸰� ������ �Ȱ��� ���� ���� ����
        //Projectile proj = projpool.Pop();
       // proj.transform.SetPositionAndRotation(transform.position, transform.rotation);

        //������Ʈ Ǯ ���̺귯����(Leampool)Ȱ��)
        LeanPool.Spawn(projectilePrefab, transform.position, transform.rotation);
        proj.damage = this.damage;
        proj.moveSpeed = projectileSpeed;
        proj.transform.localScale = proj.transform.localScale * projectileScale;
        proj.pierceCount = this.pierceCount;

        LeanPool.Despawn(proj, proj.duration);
    }

}
