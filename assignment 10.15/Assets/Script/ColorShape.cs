using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Myshape
{
    public Transform Transform;
    public MeshFilter MeshFilter;
    public Color targetcolor;
    public MeshRenderer targetRenderer;

    public Vector3 Startposition;
    public Vector3 Startrotation;
    public Mesh Mesh;
    public Material Material;
}

public class ColorShape : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Myshape> shapes;
    void Start()
    {
        foreach(Myshape shape in shapes)
        {
            shape.Transform.position = shape.Startposition;
            shape.MeshFilter.mesh = shape.Mesh;
            shape.targetRenderer.material.color = shape.targetcolor;
            shape.Transform.rotation = Quaternion.Euler(shape.Startrotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
