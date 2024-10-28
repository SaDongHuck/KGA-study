using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    public Barrier barrierPrefab; //�踮�� ������
    public float barrierInterval; //���� ����
    private float preBarrier; //���������� ������ �ð�

    public float barrierDurability; //�踮�� ������
    public float barrierDuration; //�踮�� ���ӽð�
    public float barrierDamage; //�ݻ�� 
    public float knockBack; //�з����� �Ÿ�

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
