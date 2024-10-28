using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    public float healthBonus;
    public Player player;

    
    private IEnumerator Start()
    {
        yield return null;
        player = GameManager.Instance.player.GetComponent<Player>();
        player.hp += healthBonus;
        player.maxHp += healthBonus;
    }
}
