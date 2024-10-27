using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Attack : MonoBehaviour
{
    public float bonusattack;
    private Player player;

    private void Awake()
    {
       player = GameManager.Instance.player.GetComponent<Player>();
    }

    private void Start()
    {
        player.damage += bonusattack;
    }
}
