using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Transform target; //공격할 대상 위치
    //게임 씬에 있는 타겟 대신, 랜덤한 위치에 생성
    public MissileProjectile projectilePrefab;

    public float damage;
    public float projectileSpeed;
    public float projectileScale;
    public float shotInterval;

    public float MaxDist; //최대 타겟 거리
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
        //랜덤 Vector2 포지션을 정해서, 투사체를 생성

        Vector2 pos = Random.insideUnitCircle * MaxDist;

        MissileProjectile proj = Instantiate(projectilePrefab, pos, Quaternion.identity);

        proj.damage = this.damage;
        proj.duration = 1 / projectileSpeed;
    }
}

