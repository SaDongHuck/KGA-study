using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphareSpawner : MonoBehaviour
{
    public GameObject spherePrefab; // 구체 프리팹
    public float spawnRange = 10f;  // 생성 범위
    public int maxSpheres = 10;     // 최대 구체 개수
    public Material[] sphereMaterials; // 구체 색상 배열

    private int currentSphereCount = 0;

    void Start()
    {
        SpawnSpheres();
    }

    public void SpawnSpheres()
    {
        for (int i = 0; i < maxSpheres; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                Random.Range(0.5f, spawnRange), // 바닥에서 떨어진 위치
                Random.Range(-spawnRange, spawnRange)
            );

            GameObject newSphere = Instantiate(spherePrefab, randomPosition, Quaternion.identity);

            SphareTarget sphereTarget = newSphere.GetComponent<SphareTarget>();
            if (sphereTarget != null)
            {
                sphereTarget.spawner = this; // 현재 SphereSpawner 참조 전달
            }

            // 랜덤 크기 설정
            float randomScale = Random.Range(0.5f, 2f);
            newSphere.transform.localScale = Vector3.one * randomScale;

            // 랜덤 색상 설정
            Renderer renderer = newSphere.GetComponent<Renderer>();
            if (renderer != null && sphereMaterials.Length > 0)
            {
                renderer.material = sphereMaterials[Random.Range(0, sphereMaterials.Length)];
            }

            currentSphereCount++;
        }
    }

    public void SphereHit(GameObject sphere)
    {
        Destroy(sphere); // 구체 파괴
        currentSphereCount--;

        // 구체가 모두 사라지면 다시 생성
        if (currentSphereCount <= 0)
        {
            SpawnSpheres();
        }
    }
}
