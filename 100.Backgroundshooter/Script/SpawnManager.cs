using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public bool enableSpawn = false;
    public GameObject Enemy;

    void EnemySpawn()
    {
        float random1 = Random.Range(-7.87f, 7.9f);
        if(enableSpawn)
        {
             Instantiate(Enemy, new Vector3(random1, 5.49f, 0.9f), Quaternion.identity);
        }
    }

    void Start()
    {
        InvokeRepeating("EnemySpawn", 3, 1);
    }
    void Update()
    {
        
    }

    
}
