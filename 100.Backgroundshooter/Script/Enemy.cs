using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movespeed = 3;

    void Update()
    {
       float distancey = movespeed * Time.deltaTime;
       this.gameObject.transform.Translate(0, -1 * distancey,0);
    }

}
