using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class LaserShotgun : LaserGun1
{
    public Transform[] shotPoints;

    protected override IEnumerator FireCoroutine()
    {
        while (true)
        {
            //yield return new WaitForSeconds(shotInterval);
            Fire();
            yield return new WaitForSeconds(shotInterval);
        }
    }

    protected override void Fire()
    {
        foreach (Transform shotPoint in shotPoints)
        {
            Projectile proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            //Projectile proj = projpool.Pop();
            //proj.transform.position = transform.position;
            
            proj.damage = this.damage;
            proj.moveSpeed = projectileSpeed;
            proj.transform.localScale = proj.transform.localScale * projectileScale;

            //����ü�� transform.up �������� �����ϹǷ�, up ������ �߻� �������� ���ϵ��� 
            //transform.up�� ���� ���͸� ������
            proj.transform.up = shotPoint.position - transform.position;
            proj.pierceCount = this.pierceCount;
        }
    }
}
