using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float flowspeed;
   

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * flowspeed);
        if (transform.position.y < -2.55f)
            transform.position = Vector3.zero;
    }
}
