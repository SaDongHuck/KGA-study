using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyProject
{
    //nullable 문법 : ? 연산자를 적극 사용해야함
    public class Nullable : MonoBehaviour
    {

        public bool isBlue;

        private Renderer rend;

        //리터럴 타입(값타입) 필드를 객체처럼 null 또는 주소(instance hash)를 사용하고 싶을 떄
        // 거의, C++의 포인터와 비슷한 형태로 쓰고 싶을 때, type 뒤에 ? 붙이고, 이를 nullable type 이라고 함
        private int? nullableInt;

        private Myclass myclass;
        private Vector2? nullableVector;

        private void Awake()
        {
            rend = GetComponent<Renderer>();   
        }

        private void Start()
        {
            //1. 3항 연산자 : bool ? 조건true : 조건false ;
            rend.material.color = isBlue ? Color.blue : Color.red;

            //2. ?. ?? : null체크 가능
            //a, 객체?.함수();
            Myclass myclass1 = null;
            myclass1?.GatA();
            myclass1 = new Myclass() { a = 1 };
            myclass1?.GatA();
            //b-1. 객체?.참조필드 : 필드가 참조타입일 경우에만 가는 객체가 null일 경우
            //nullReferenceException을 내뱉는 경우
            myclass1 = null;
            GameObject someobj = myclass1?.obj;
            print(someobj);

            //b-2. 객체?.참조필드??다른필드또는객체 : 객체가 null일 경우, ??뒤의 값이 대입 됨
            GameObject someObj2 = myclass1?.obj ?? new GameObject();
            print(someObj2);

            //c. 객체?.값필드??(필수)대체 값 : 객체가 null 경우, 접근하는 필드가
            // 리터럴 타입이라면 무조건 대체 값이 지정되어야 함

            int someInt = myclass1?.a ?? 1;
            print(someInt);

            if(myclass1 != null){
               someobj = myclass1.obj;
            }
            else{
                someobj = new GameObject(); 
            }

            print($"nullableInt : {nullableInt}");

            string intToText = 1.ToString();
            intToText = nullableInt?.ToString()??0.ToString();
            print(intToText);

            nullableInt = 2;

            int localInt = 3;
            nullableInt = localInt;
            localInt = nullableInt.Value;

            nullableInt = null;// nullable 변수에 null을 대입해서 변수를 비움

            //localInt = nullableInt.Value;//변수를 접근해봤자 null이기 때문에 접근이 안됨

            if (nullableInt.HasValue)
            { //명시적으로 null check
                localInt = nullableInt.Value;
            }else { localInt = 0; } 

            print(localInt);

            print(localInt);
        }

        public class Myclass
        {
            public int a;
            public GameObject obj;
            public Myclass()
            {
                obj = new GameObject();
                obj.name = "MyClass";
            }
            public int GatA() {
                print("Return A");
                return a; }
        }
    }
}
