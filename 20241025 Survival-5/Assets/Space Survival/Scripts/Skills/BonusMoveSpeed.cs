using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMoveSpeed : MonoBehaviour
{
    public float Bonus_Speed = 5;
    private Player player;

    private void Awake()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
    }

    private void Start()
    {
        player.moveSpeed += Bonus_Speed;
    }
}
