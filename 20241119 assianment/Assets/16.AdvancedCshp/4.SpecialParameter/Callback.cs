using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    //�Լ��� ȣ�� �ϰ� ��� � �ٸ� �Լ��� ȣ��Ǿ� �Ҷ�, �װ� �ݹ��Լ���� �θ�
    public class Callback : MonoBehaviour
    {
        //������ Ư�� �Լ� ���� �Ŀ� �ٸ� �Լ��� ȣ�� �Ǳ� ���� �� �� �Լ��� c#ver : �븮�� ���·�
        // Javarscript var : �Լ� �����ͷ� �ѱ�

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
                print("�ƽ��Ե�");
            }
        }
    }
}
