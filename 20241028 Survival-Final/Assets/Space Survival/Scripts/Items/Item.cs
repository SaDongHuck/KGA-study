using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IContactable
{
    //��� �������� ��ӹ��� Ŭ������ ������ ������ �� ���� ���� �ؾ���
    //public abstract void Contact();

    public virtual void Contact()
    {
        Destroy(gameObject);
    }
}
