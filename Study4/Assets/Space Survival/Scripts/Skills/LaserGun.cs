using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using Lean.Pool;

//기본공격 : 투사체를 발사하는 스킬
public class LaserGun1 : MonoBehaviour
{
    public Transform target; //투사체가 향해야 할 방향에 있는 대상
    public Projectile projectilePrefab; //발사할 투사체의 프리팹
    public ProjectilePool projpool; //프리팹을 보관한 풀 매니저

    public float damage;          //투사체 대미지
    public float projectileSpeed; //투사체 속도
    public float projectileScale; //투사체 크기
    public float shotInterval;    //공격 쿨타임

    public int projectileCount;   //투사체 개수
    public float innerInterval;   //연사 간격
    [Tooltip("관통 횟수\n0 to Infinity")]
    public int pierceCount;       //투사체 관통력

    protected virtual void Start()
    {
        StartCoroutine(FireCoroutine());
    }

    protected virtual void Update()
    {
        if (target == null) { return; }
        transform.up = target.position - transform.position; //방향 계산이였나
    }

    protected virtual IEnumerator FireCoroutine() //공격 코루틴
    {
        while (true)
        {
            yield return new WaitForSeconds(shotInterval);
            //1. 투사체 개수가 올라가면 0.05초 간격으로 투사체 개수만큼 발사 반복
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
        //로테이션을 똑같이 줘서 위를 바라보고 있으면 똑같이 위로 가게 설정
        //Projectile proj = projpool.Pop();
       // proj.transform.SetPositionAndRotation(transform.position, transform.rotation);

        //오브젝트 풀 라이브러리브(Leampool)활용)
        LeanPool.Spawn(projectilePrefab, transform.position, transform.rotation);
        proj.damage = this.damage;
        proj.moveSpeed = projectileSpeed;
        proj.transform.localScale = proj.transform.localScale * projectileScale;
        proj.pierceCount = this.pierceCount;

        LeanPool.Despawn(proj, proj.duration);
    }

}
