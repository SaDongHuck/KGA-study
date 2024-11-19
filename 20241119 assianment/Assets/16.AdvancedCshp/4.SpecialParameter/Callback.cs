using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    //함수를 호출 하고난 결과 어떤 다른 함수가 호출되야 할때, 그걸 콜백함수라고 부름
    public class Callback : MonoBehaviour
    {
        //보통은 특정 함수 수행 후에 다른 함수가 호출 되길 원할 때 그 함수를 c#ver : 대리자 형태로
        // Javarscript var : 함수 포인터로 넘김

        public GameObject destroytarget;

        public CallBackTest popup;

        public Action callback;
        public void onDestroyButtonClick()
        {
            popup.showPopup(
                onYes);
        }

        public void onYes(bool yes)
        {
            if (yes){
                Destroy(destroytarget);
            }
            else
            {
                print("아쉽게도");
            }
        }
    }
}
