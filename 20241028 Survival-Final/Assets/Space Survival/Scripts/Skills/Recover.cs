using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recover : MonoBehaviour
{
    public float recover; //����

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameManager.Instance.player.TakeHeal(recover);
        }
    }
}
