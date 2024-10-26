using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
    public override void Contact()
    {
        print("폭탄 습득함.");
        //게임 매니저에게 부탁하자.
        //모든 적들을 없애달라고.

        GameManager.Instance.RemoveAllEnemies();
        base.Contact();
    }
}
