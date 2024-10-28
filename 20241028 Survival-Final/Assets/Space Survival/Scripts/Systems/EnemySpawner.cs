using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using SysRan = System.Random;

public class EnemySpawner : MonoBehaviour
{
    //1. 적이 한 번 스폰 될 때 마다 1마리가 아니라, 2~10 마리 스폰되도록 바꾸고,
    //2. 적 스폰 위치를 Vector2.zero가 아닌, 플레이어 기준 특정 거리 이상 위치에 스폰하기
    [Tooltip("한 번에 스폰될 적의 수\nX : 최소 카운트\nY : 최대 카운트")]
    public Vector2Int minMaxCount;
    [Tooltip("플레이어로부터 스폰될 최소, 최대 거리\nX : 최소 거리\nY : 최대거리")]
    public Vector2 minMaxDistance;
    //public int minCount;
    //public int maxCount;

    public GameObject enemyPrefab; //적 프리팹
    public float spawnInterval; //생성 간격
    public EnemyDataSO[] enemyDatas;



    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
        //Json으로 직렬화

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
        //랜덤하게 지정된 수만큼 소환
        for (int i = 0; i < count; i++)
        {
            Vector2 playerPos = GameManager.Instance.player.transform.position;
            //insideUnitCircle : 길이가 1인 원 안에서 랜덤한 좌표를 반환함
            Vector2 spawnPos = Vector2.zero;
            //1. 랜던 좌표를 받은 후, 거리가 3 이내면 다시 랜덤을받음
            //\=============================================================================================
            //int loopCount = 0;
            //do
            //{
            //    //플레이어로부터 거리가 0 ~ minMaxDistance.y 사이의 좌표를 받음
            //    spawnPos = Random.insideUnitCircle * minMaxDistance.y;

            //    spawnPos.x = (spawnPos.x > 0) ? 1f : -1f;
            //    spawnPos.y = (spawnPos.y > 0) ? 1f : -1f;

            //    spawnPos.x = (spawnPos.x * Random.Range(minMaxDistance.x, minMaxDistance.y));
            //    spawnPos.y = (spawnPos.y * Random.Range(minMaxDistance.x, minMaxDistance.y));

            //    loopCount++;
            //}
            //while (spawnPos.magnitude < minMaxDistance.x);
            //한번에 스폰하는 경우도 있지만, 조건이 맞지않는 좌표가 나와서 루프 도는 빈도가 높음
            //Debug.Log(loopCount);
            //랜덤으로 나온 좌표를 무조건 조건에 맞게 가공하도록 연산
            //===============================================================================================

            //랜덤 좌표를 하나 찍고,
            Vector2 ranPos = Random.insideUnitCircle;
            //찍힌 랜덤좌표 방향의 길이가 1인 벡터를 구함
            Vector2 nomalizedPos = ranPos.normalized;
            //중심 기준으로 ranPos가 확장할 범위를 구함
            float moveRad = minMaxDistance.y - minMaxDistance.x; //5 - 3 = 2
            //범위만큼 움직인 좌표를 구함
            Vector2 movedPos = ranPos * moveRad;
            //minDist 이내에는 좌표가 찍히면 안되므로, 찍힌 좌표를 해당방향으로 mindist만큼 움직일 벡터를 구함
            Vector2 noSpawnAreaVector = nomalizedPos * minMaxDistance.x;
            //움직인 좌표에 minDist 만큼 움직인 벡터를 더함
            spawnPos = movedPos + noSpawnAreaVector;

            //아래의 식과 같음 
            //spawnPos = (ranPos * (minMaxDistance.y - minMaxDistance.x)) + ranPos.normalized * minMaxDistance.x;
            
            //print($"spawnPos : {spawnPos}");
            Instantiate(enemyPrefab, playerPos + spawnPos, Quaternion.identity); 
        }
    }
}
