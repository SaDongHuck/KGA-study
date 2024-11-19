using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    //Delegate Ű���� 1: �븮�� �Լ��� ������ ��ü���ش�
    //���������� ������ Classó�� ����

    //delegate�� ���� ���� : [��ȯ��] ��������Ʈ�̸�(�Ķ����)
    public delegate void SomeMethod(int a); //return�� ���� �Լ�(Method)
    public delegate int someFunction(int a, int b);

    //delegate Ű���� 2 : ���� �޼��� �������� Ȱ��
    public class Delegate : MonoBehaviour
    {
        public Text text;
        private void Start()
        {
            SomeMethod myMethod = PrintInt;
            myMethod(1); //Console: 1���
            myMethod += CrerateInt;
            myMethod(2); //Cosnsole: 2���, 2��� ���� ������Ʈ ����
            myMethod -= PrintInt;
            myMethod?.Invoke(3); // 3 �̶�� �̸��� ���� ������Ʈ ����
            myMethod -= CrerateInt;
            myMethod?.Invoke(4); //myMethod�� null�̸� �׳� ȣ�� ����

            if (myMethod != null)
                myMethod.Invoke(4);

            SomeMethod delegateisclass = new SomeMethod(PrintInt);
            delegateisclass(5); // console : 5���

            someFunction idontknow = Plus;
            int firstReturn = idontknow(1, 2);
            print(firstReturn);
            idontknow += Multiple;
            int secondReturn = idontknow(1, 2);
            print(secondReturn);

            //deegate�� ����޼��� Ȱ��

            SomeMethod someUnnamedMehod = delegate (int a)
            { text.text = a.ToString(); };

            //1�� ����ȭ : delegate ��ſ� => �����ڷ� ��ü
            someUnnamedMehod += (int a) => { print(a); };

            //2�� ����ȭ : �Ķ���� ������Ÿ���� ���� ����
            someUnnamedMehod += (b) =>
            {
                print(b);
                text.text = b.ToString();
            };

            //3�� ����ȭ :�Լ� ������ 1��(����Ŭ��;�� 1���� ���)��� �߰�ȣ ���� ����
            someUnnamedMehod += (c) => print(c);

            //�Լ� 1�� ����ȭ�� ��� return Ű���� ���� ���� ����
            //someFunction someUnnamedMehod = (someIntA, someIntB) => Plus(someIntA, someIntB);

            someUnnamedMehod(4);

            myMethod += someUnnamedMehod;

            myMethod -= someUnnamedMehod;

            //����޼����� ���� : �ش� �޼��带 ���Ŀ� �ٽ� ������ �� ����
            //���� ���������� ���� ����

            //.netFramewark ���� delegate

            //1. ������ ���� �Լ�(method) : Action
            System.Action nonParamMethod = () => { };
            System.Action<int> intParamMehod = (int a) => { };
            System.Action<string> stringParamMethod = (b) => { };
            //2. ������ �ִ� �Լ�(FUnction) : Func
            System.Func<int> nonParamFunc = () => { return 3; };
            System.Func<int, string> intparaFunc = (int a) => { return a.ToString(); }; 
            //3. ���ǰ˻縦 ���� ������ bool ������ ���� �Լ� : Predicate
            System.Predicate<int> isOne = (a) => { return a == 1; };
            //�� ��
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
            print("Plusȣ���");
            return a + b;   
        }

        private int Multiple(int c, int d)
        {
            print("Multiple ȣ���");
            return c * d; 
        }

        private float plusfloat(float a, float b)
        {
            return a + b;
        }
    }
}
