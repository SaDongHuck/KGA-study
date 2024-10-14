using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private int count = 0;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello Unity!");
        Debug.LogWarning("Hello Unity!!");
        Debug.LogError("Hello Unity!");
    }



    // Update is called once per frame
    void Update()
    {
        Debug.Log("Updating.");
        
    }

}
