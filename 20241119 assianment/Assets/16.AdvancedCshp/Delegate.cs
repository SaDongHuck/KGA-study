using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    //Delegate 키워드 1: 대리자 함수의 리음을 대체해준다
    //내부적으로 일종의 Class처럼 동작

    //delegate의 선언 형태 : [반환형] 델리게이트이름(파라미터)
    public delegate void SomeMethod(int a); //return이 없는 함수(Method)
    public delegate int someFunction(int a, int b);

    //delegate 키워드 2 : 무명 메서드 선언으로 활용
    public class Delegate : MonoBehaviour
    {
        public Text text;
        private void Start()
        {
            SomeMethod myMethod = PrintInt;
            myMethod(1); //Console: 1출력
            myMethod += CrerateInt;
            myMethod(2); //Cosnsole: 2출력, 2라는 게임 오브젝트 생성
            myMethod -= PrintInt;
            myMethod?.Invoke(3); // 3 이라는 이름이 게임 오브젝트 생성
            myMethod -= CrerateInt;
            myMethod?.Invoke(4); //myMethod가 null이면 그냥 호충 안함

            if (myMethod != null)
                myMethod.Invoke(4);

            SomeMethod delegateisclass = new SomeMethod(PrintInt);
            delegateisclass(5); // console : 5출력

            someFunction idontknow = Plus;
            int firstReturn = idontknow(1, 2);
            print(firstReturn);
            idontknow += Multiple;
            int secondReturn = idontknow(1, 2);
            print(secondReturn);

            //deegate의 무명메서드 활용

            SomeMethod someUnnamedMehod = delegate (int a)
            { text.text = a.ToString(); };

            //1차 간소화 : delegate 대신에 => 연산자로 대체
            someUnnamedMehod += (int a) => { print(a); };

            //2차 간소화 : 파라미터 데이터타입을 생략 가능
            someUnnamedMehod += (b) =>
            {
                print(b);
                text.text = b.ToString();
            };

            //3차 간소화 :함수 내용이 1줄(세미클론;이 1개만 사용)경우 중괄호 생략 가능
            someUnnamedMehod += (c) => print(c);

            //함수 1줄 감소화의 경우 return 키워드 까지 생략 가능
            //someFunction someUnnamedMehod = (someIntA, someIntB) => Plus(someIntA, someIntB);

            someUnnamedMehod(4);

            myMethod += someUnnamedMehod;

            myMethod -= someUnnamedMehod;

            //무명메서드의 단점 : 해당 메서드를 추후에 다시 참조할 수 없다
            //선언 시점에서만 참조 가능

            //.netFramewark 내장 delegate

            //1. 리턴이 없는 함수(method) : Action
            System.Action nonParamMethod = () => { };
            System.Action<int> intParamMehod = (int a) => { };
            System.Action<string> stringParamMethod = (b) => { };
            //2. 리턴이 있는 함수(FUnction) : Func
            System.Func<int> nonParamFunc = () => { return 3; };
            System.Func<int, string> intparaFunc = (int a) => { return a.ToString(); }; 
            //3. 조건검사를 위해 무조건 bool 리턴을 가진 함수 : Predicate
            System.Predicate<int> isOne = (a) => { return a == 1; };
            //그 외
            System.Comparison<Color> compare = (Color a, Color b) => { return (int)(a.a - b.a); };  
        }

        private void PrintInt(int a)
        {
            print(a);
        }

        private void CrerateInt(int a)
        {
            new GameObject().name = a.ToString();
        }

        private int Plus(int a, int b)
        {
            print("Plus호출됨");
            return a + b;   
        }

        private int Multiple(int c, int d)
        {
            print("Multiple 호출됨");
            return c * d; 
        }

        private float plusfloat(float a, float b)
        {
            return a + b;
        }
    }
}
