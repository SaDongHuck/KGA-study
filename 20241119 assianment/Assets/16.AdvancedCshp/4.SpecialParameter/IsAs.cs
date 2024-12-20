using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class IsAs : MonoBehaviour
    {
       //is as : 참조 타입의 캐스팅(형변환)과 관련이 있음

        class A {
            public static implicit operator GameObject(A a) => GameObject.Find("A");
            //public static object operator ==(A a, object b) { return true; }
            //public static object operator !=(A a, object b) { return true; }
        }
        class B : A{ }

        private void Start()
        {
            A a = new A();
            B b = a as B; //as : ()를 사용한 명시적 캐스팅과 달리 캐스팅 불가능 하더라도 Exception을 내뱉지 않음
                          //대신 null을 반환
                          //()를 사용한 명시적 캐스팅 보다 효율적인 연산
                          //대신 사용자 정의 명시적 및 암시적 변환 연산자를 활용하지 못함

            print(b?.GetType().ToString() ?? "B is null");

            //is : as가 직접 캐스팅한 객체가 결과로 오는 대신, 캐스팅 가능한지 여부만 true/false로 연산됨

            print(a is A);
            print(b is A);

            print(b is null);

            // == 연산자보다 효율적
            //대신 사용자 정의 연산자를 사용하지 못함

            print(b == null);
        }
    }
}
