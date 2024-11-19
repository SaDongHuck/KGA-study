using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    public class SizeAttributeController : MonoBehaviour
    {
        [ColorAttribute(1, 0, 0)] // 빨간색
        public Renderer objectRenderer;

        [SizeAttribute(2, 2, 2)] // 크기 2,2,2
        public Transform objectTransform;

        [SizeAttribute(50, 50)] // RectTransform의 sizeDelta 설정
        public RectTransform uiRectTransform;
    }
}
