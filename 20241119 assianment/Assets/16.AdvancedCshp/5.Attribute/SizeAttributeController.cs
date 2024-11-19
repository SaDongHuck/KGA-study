using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    public class SizeAttributeController : MonoBehaviour
    {
        [ColorAttribute(1, 0, 0)] // ������
        public Renderer objectRenderer;

        [SizeAttribute(2, 2, 2)] // ũ�� 2,2,2
        public Transform objectTransform;

        [SizeAttribute(50, 50)] // RectTransform�� sizeDelta ����
        public RectTransform uiRectTransform;
    }
}
