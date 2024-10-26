using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using SysRan = System.Random;

public class EnemySpawner : MonoBehaviour
{
    //1. ���� �� �� ���� �� �� ���� 1������ �ƴ϶�, 2~10 ���� �����ǵ��� �ٲٰ�,
    //2. �� ���� ��ġ�� Vector2.zero�� �ƴ�, �÷��̾� ���� Ư�� �Ÿ� �̻� ��ġ�� �����ϱ�
    [Tooltip("�� ���� ������ ���� ��\nX : �ּ� ī��Ʈ\nY : �ִ� ī��Ʈ")]
    public Vector2Int minMaxCount;
    [Tooltip("�÷��̾�κ��� ������ �ּ�, �ִ� �Ÿ�\nX : �ּ� �Ÿ�\nY : �ִ�Ÿ�")]
    public Vector2 minMaxDistance;
    //public int minCount;
    //public int maxCount;

    public GameObject enemyPrefab; //�� ������
    public float spawnInterval; //���� ����
    
    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            int enemyCount = Random.Range(minMaxCount.x, minMaxCount.y);
            Spawn(enemyCount);
        }
    }

    private void Spawn(int count)
    {
        //�����ϰ� ������ ����ŭ ��ȯ
        for (int i = 0; i < count; i++)
        {
            Vector2 playerPos = GameManager.Instance.player.transform.position;
            //insideUnitCircle : ���̰� 1�� �� �ȿ��� ������ ��ǥ�� ��ȯ��
            Vector2 spawnPos = Vector2.zero;
            //1. ���� ��ǥ�� ���� ��, �Ÿ��� 3 �̳��� �ٽ� ����������
            int loopCount = 0;
            do
            {
                //�÷��̾�κ��� �Ÿ��� 0 ~ minMaxDistance.y ������ ��ǥ�� ����
                spawnPos = Random.insideUnitCircle * minMaxDistance.y;

                spawnPos.x = (spawnPos.x > 0) ? 1f : -1f;
                spawnPos.y = (spawnPos.y > 0) ? 1f : -1f;

                spawnPos.x = (spawnPos.x * Random.Range(minMaxDistance.x, minMaxDistance.y));
                spawnPos.y = (spawnPos.y * Random.Range(minMaxDistance.x, minMaxDistance.y));

                loopCount++;
            }
            while (spawnPos.magnitude < minMaxDistance.x);
            //�ѹ��� �����ϴ� ��쵵 ������, ������ �����ʴ� ��ǥ�� ���ͼ� ���� ���� �󵵰� ����
            Debug.Log(loopCount);
            //�������� ���� ��ǥ�� ������ ���ǿ� �°� �����ϵ��� ����

            Instantiate(enemyPrefab, playerPos + spawnPos, Quaternion.identity); 
        }
    }
}
