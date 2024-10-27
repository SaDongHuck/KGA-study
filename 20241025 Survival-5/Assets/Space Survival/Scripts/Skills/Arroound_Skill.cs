using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arroound_Skill : MonoBehaviour
{
    public Transform player; // �������� ���ؾ� �� ���⿡ �ִ� ���
    public Projectile2 projectilePrefab; // �߻��� ����ü�� ������

    public float damage; // ����ü �����
    public float projectileSpeed; // ����ü �ӵ�
    public float projectileScale; // ����ü ũ��
    public float radius;
    public float shotInterval; // ���� ��Ÿ��

    public float rotationSpeed; // ȸ�� �ӵ�
    public int pierceCount; // ����ü �����

    private void Start()
    {

        StartCoroutine(RotateAndFire());
    }

    private void Update()
    {
        if (player != null)
        {
            // �÷��̾ ���� ȸ��
            Vector3 direction = player.position - transform.position;
            transform.up = direction.normalized;
        }
    }

    private IEnumerator RotateAndFire() // ȸ���ϸ� �߻��ϴ� �ڷ�ƾ
    {
        while (true)
        {
            // ������ ȸ��
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            // ���� �ð����� ����ü �߻�
            yield return new WaitForSeconds(shotInterval);
            Fire();
        }
    }

    private void Fire()
    {
        // ������ �������� Ǯ���� ����
        Projectile2 proj = LeanPool.Spawn(projectilePrefab, transform.position, transform.rotation);
        proj.damage = this.damage;
        proj.moveSpeed = projectileSpeed;
        proj.transform.localScale = proj.transform.localScale * projectileScale;
        proj.pierceCount = this.pierceCount;

        // ����ü�� ���� �ð��� ������ ��Ȱ��ȭ
        LeanPool.Despawn(proj, proj.duration);
    }
}
