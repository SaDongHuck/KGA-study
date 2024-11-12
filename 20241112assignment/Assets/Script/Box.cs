using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        // Player 레이어 번호로 직접 비교 (8로 가정)
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 원하는 상호작용 코드 작성
            print("Player와 Box 충돌 발생!");
        }

    }
}
