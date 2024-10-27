using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IContactable
{
    //모든 아이템을 상속받은 클래스가 가져야 하지만 다 따로 정의 해야함
    //public abstract void Contact();

    public virtual void Contact()
    {
        Destroy(gameObject);
    }
}
