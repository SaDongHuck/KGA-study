using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefabobject;
    public Transform location;
    public float timedelay;

    IEnumerator MonsterSpawn()
    {
        while(true)
        {
            Instantiate(prefabobject, location.position, location.rotation);
            yield return new WaitForSeconds(timedelay);
        }
    }

    void Start()
    {
         StartCoroutine(MonsterSpawn());
    }

}
