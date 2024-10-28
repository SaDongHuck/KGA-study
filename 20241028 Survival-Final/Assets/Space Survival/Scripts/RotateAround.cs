using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float rotationSpeed;
    //public GameObject arrow_center;
    public float damage;

    private void Awake()
    {
        //arrow_center = GameObject.Find("Player");
    }
    void Update()
    {
        transform.RotateAround(GameManager.Instance.player.transform.position,
            Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            Debug.Log("RoateAround");
            enemy.TakeDamage(damage);
        }
    }
}
