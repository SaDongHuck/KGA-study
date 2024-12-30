using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphareTarget : MonoBehaviour
{
    public SphareSpawner spawner;
    public int scoreValue = 10;   // 구체를 맞췄을 때 얻는 점수

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) // 투사체와 충돌
        {
            LaunchController launchController = FindObjectOfType<LaunchController>();
            if (launchController != null)
            {
                launchController.AddScore(10); // 점수 추가
            }
            spawner.SphereHit(gameObject); // 구체 제거
        }
    }
}
