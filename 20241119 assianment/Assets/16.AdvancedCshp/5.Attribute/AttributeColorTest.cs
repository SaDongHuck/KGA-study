using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    public class AttributeColorTest : MonoBehaviour
    {
        [Color(0,1,0,1)]
        public Renderer rend;

        [SerializeField,Color(r:1, b:0.5f)]
        public Graphic graph;

        [Color]
        public float norRendererOrGrephic;

        [Size(2,2,2)]
        public Transform objectTransform;

        [Size(4,4,4)]
        public RectTransform uiRectTransform;
    }
}
