using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelKey : MonoBehaviour
{
    public GameObject menuPanel; // 패널 GameObject
    public KeyCode toggleKey = KeyCode.F; // 패널을 활성화/비활성화하는 키 (F 키)

    void Update()
    {
        // F 키 입력 감지
        if (Input.GetKeyDown(toggleKey))
        {
            Debug.Log("F Key Pressed");
            // 패널 활성화/비활성화 전환
            menuPanel.SetActive(!menuPanel.activeSelf);
        }
    }
}
