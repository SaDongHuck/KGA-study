using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOx : MonoBehaviour
{
    public GameObject item;
    private void Contact()
    {
        Instantiate(item, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
