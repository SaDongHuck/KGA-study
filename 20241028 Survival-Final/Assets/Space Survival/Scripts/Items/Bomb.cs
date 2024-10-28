using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
    //특정 메시지함수가 없는 Component는 Enable / Dsiable이 동작하지 않음
    //Start, Update가 있어야 함

    public override void Contact()
    {
        print("폭탄 습득함.");
        //게임 매니저에게 부탁하자.
        //모든 적들을 없애달라고.

        GameManager.Instance.RemoveAllEnemies();
        base.Contact();
    }

    private void Update()
    {
        
    }
}
