using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
    //Ư�� �޽����Լ��� ���� Component�� Enable / Dsiable�� �������� ����
    //Start, Update�� �־�� ��

    public override void Contact()
    {
        print("��ź ������.");
        //���� �Ŵ������� ��Ź����.
        //��� ������ ���ִ޶��.

        GameManager.Instance.RemoveAllEnemies();
        base.Contact();
    }

    private void Update()
    {
        
    }
}
