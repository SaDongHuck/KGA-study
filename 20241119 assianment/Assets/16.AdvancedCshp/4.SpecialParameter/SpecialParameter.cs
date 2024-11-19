using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace MyProject
{
    public class SpecialParameter : MonoBehaviour
    {
        // ref 키워드 : value 타입(enum, strunct , literal) 데이터 파라미터를 통해 함수에 전달할 경우
        // 메모리에 값을 복사하여 전달하는데, 이를 포인터로 대체 하는 파라미터로 선언할 경우에 사용

        private void Start()
        {
            int a = 10;
            int b = 20;
            Swap(ref a, ref b);
            print($"a: {a}, b: {b}");

            GameObject obj1 = new GameObject("No.1");
            obj1.transform.position = new Vector3(1, 0, 0);
            GameObject obj2 = new GameObject("No.2");
            SwapObj(obj1, obj2);
            print($"obj1 : {obj1.name}, obj2 : {obj2.name}");

            GameObject outobj1 = new GameObject("Out");

            outobj1.transform.position = new Vector3(1, 2, 3);

            GameObject outobj2 = new GameObject("Not Out");

            outobj2.transform.position = new Vector3(3, 2, 1);


            //out 키워드 파라미터는 특성상 선언시 초기화 된 값이 의미가 없기 때문에
            //함수 호출과 같은 라인에서 선언이 가능
            if (TryGetPosition(outobj1, out Vector3 outPos))
            {
                print($"Out : {outPos}");
            }
            if (TryGetPosition(outobj1, out Vector3 outPos2))
            {
                print($"Out : {outPos2}");
            }

        }

        void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        void SwapObj(GameObject obj1,GameObject obj2)
        {
            string temp = obj1.name;
            obj1.name = obj2.name;
            obj2.name = temp;
        }

        //out : 전통적인 프로그래밍 문법에서 리턴은 단 하나
        //근데 함수 수행 경과 받고 싶은 데이터가 여러개일 경우에는 어떡하나?

        object[] TryGetComponent(Type type) // even 하지 않은 리턴
        {
            Component comp = GetComponent(type);
            bool boolReturn = comp is not null;
            return new object[2] { boolReturn, comp };
        }

        //return은 기본적인 반환만 하고 추가적인 데이터는 out 파라미터로 전달된 변수에 대입하는걸로 대체
        //함수에 out 키워드가 포함되어 있을 경우 
        bool TryGetPosition(GameObject target, out Vector3 pos)
        {
             //만약 target.name이 "Out"이면 target.trasform.position을 대입
             //아니면 Vector3.zero를 대입
             if(target.name == "Out")
             {
                pos = target.transform.position;
                return true;
             }
             else
             {
                pos = Vector3.zero;
                return false;
             }
        }


    }

   
}
