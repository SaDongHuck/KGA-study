using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Health : MonoBehaviour
{
    public float BonusHealth = 5;
    private Player player;

    private void Awake()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
    }

    private void Start()
    {
        player.hp += BonusHealth;
    }
}
