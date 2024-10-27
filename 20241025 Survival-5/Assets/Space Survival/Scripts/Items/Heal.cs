using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Heal : Item
{
    public float healAmount;
    public override void Contact()
    {
        print("ȸ���� ������.");

        GameManager.Instance.player.TakeHeal(healAmount);
        base.Contact();
    }
}
