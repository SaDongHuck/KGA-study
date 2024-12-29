using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform vrCamera; // VR 카메라(또는 XR Rig의 Camera)
    public float distanceFromCamera = 2.0f; // 패널과 카메라 사이의 거리
    public Vector3 offset = new Vector3(0, -0.5f, 0); // 높이 등 추가 조정

    void LateUpdate()
    {
        if (vrCamera != null)
        {
            // 카메라 앞에 위치 계산
            Vector3 forwardDirection = vrCamera.forward;
            forwardDirection.y = 0; // 수평 방향 유지
            forwardDirection.Normalize();

            // 패널 위치 설정
            transform.position = vrCamera.position + forwardDirection * distanceFromCamera + offset;

            // 패널이 항상 카메라를 향하도록 설정
            transform.rotation = Quaternion.LookRotation(forwardDirection);
        }
    }
}
