using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Renderer roomRenderer; // 룸의 Renderer
    private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow }; // 색상 배열
    private int currentColorIndex = 0;

    // 버튼 클릭 시 호출
    public void ChangeRoomColor()
    {
        // 다음 색상으로 전환
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        roomRenderer.material.color = colors[currentColorIndex];
    }
}
