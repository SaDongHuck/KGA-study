using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    public Barrier barrierPrefab; //배리어 프리팹
    public float barrierInterval; //생성 간격
    private float preBarrier; //마지막으로 생성된 시간

    public float barrierDurability; //배리어 내구도
    public float barrierDuration; //배리어 지속시간
    public float barrierDamage; //반사딜 
    public float knockBack; //밀려나는 거리

    //private IEnumerator Start()
    //{
    //    yield return null;
    //    //StartCoroutine(SapwnBarrier(barrierInterval));
    //}

    private void Update()
    {
        if (preBarrier + barrierInterval <= Time.time)
        {
            SpawnBarrier();
        }
    }

    private void SpawnBarrier()
    {
        Barrier barrier = Instantiate(barrierPrefab, GameManager.Instance.player.transform.position
         , Quaternion.identity);
        barrier.transform.SetParent(GameManager.Instance.player.transform);

        barrier.barrierDamage = this.barrierDamage;
        barrier.barrierDurability = this.barrierDurability;
        barrier.knockBack = this.knockBack;
        barrier.barrierDuration = this.barrierDuration;

        preBarrier = Time.time;
    }
}
