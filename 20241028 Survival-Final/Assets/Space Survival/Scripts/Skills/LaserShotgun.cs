using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShotgun : LaserGun
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
            //Projectile proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            //Projectile proj = projpool.Pop();
            Projectile proj = LeanPool.Spawn(projectilePrefab,transform.position,transform.rotation);
            proj.transform.position = transform.position;

            proj.damage = this.damage;
            proj.moveSpeed = projectileSpeed;
            proj.transform.localScale = proj.transform.localScale * projectileScale;

            //����ü�� transform.up �������� �����ϹǷ�, up ������ �߻� �������� ���ϵ��� 
            //transform.up�� ���� ���͸� ������
            proj.transform.up = shotPoint.position - transform.position;
            proj.pierceCount = this.pierceCount;
            LeanPool.Despawn(proj, proj.duration);
        }
        //audioSource.PlayOneShot();
    }
}
