using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
    public override void Contact()
    {
        print("��ź ������.");
        //���� �Ŵ������� ��Ź����.
        //��� ������ ���ִ޶��.

        GameManager.Instance.RemoveAllEnemies();
        base.Contact();
    }
}
