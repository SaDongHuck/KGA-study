using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arroound_Skill : MonoBehaviour
{
    public Transform player; // 레이저가 향해야 할 방향에 있는 대상
    public Projectile2 projectilePrefab; // 발사할 투사체의 프리팹

    public float damage; // 투사체 대미지
    public float projectileSpeed; // 투사체 속도
    public float projectileScale; // 투사체 크기
    public float radius;
    public float shotInterval; // 공격 쿨타임

    public float rotationSpeed; // 회전 속도
    public int pierceCount; // 투사체 관통력

    private void Start()
    {

        StartCoroutine(RotateAndFire());
    }

    private void Update()
    {
        if (player != null)
        {
            // 플레이어를 향해 회전
            Vector3 direction = player.position - transform.position;
            transform.up = direction.normalized;
        }
    }

    private IEnumerator RotateAndFire() // 회전하며 발사하는 코루틴
    {
        while (true)
        {
            // 레이저 회전
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            // 일정 시간마다 투사체 발사
            yield return new WaitForSeconds(shotInterval);
            Fire();
        }
    }

    private void Fire()
    {
        // 레이저 프리팹을 풀에서 생성
        Projectile2 proj = LeanPool.Spawn(projectilePrefab, transform.position, transform.rotation);
        proj.damage = this.damage;
        proj.moveSpeed = projectileSpeed;
        proj.transform.localScale = proj.transform.localScale * projectileScale;
        proj.pierceCount = this.pierceCount;

        // 투사체가 일정 시간이 지나면 비활성화
        LeanPool.Despawn(proj, proj.duration);
    }
}
