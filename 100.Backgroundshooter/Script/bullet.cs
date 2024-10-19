using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 0.5f;
    // Update is called once per frame
    void Update()
    {
        float moveY = speed * Time.deltaTime;
        transform.Translate(0, moveY, 0);
    }
}
