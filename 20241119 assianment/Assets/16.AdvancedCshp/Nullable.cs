using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyProject
{
    //nullable ���� : ? �����ڸ� ���� ����ؾ���
    public class Nullable : MonoBehaviour
    {

        public bool isBlue;

        private Renderer rend;

        //���ͷ� Ÿ��(��Ÿ��) �ʵ带 ��üó�� null �Ǵ� �ּ�(instance hash)�� ����ϰ� ���� ��
        // ����, C++�� �����Ϳ� ����� ���·� ���� ���� ��, type �ڿ� ? ���̰�, �̸� nullable type �̶�� ��
        private int? nullableInt;

        private Myclass myclass;
        private Vector2? nullableVector;

        private void Awake()
        {
            rend = GetComponent<Renderer>();   
        }

        private void Start()
        {
            //1. 3�� ������ : bool ? ����true : ����false ;
            rend.material.color = isBlue ? Color.blue : Color.red;

            //2. ?. ?? : nullüũ ����
            //a, ��ü?.�Լ�();
            Myclass myclass1 = null;
            myclass1?.GatA();
            myclass1 = new Myclass() { a = 1 };
            myclass1?.GatA();
            //b-1. ��ü?.�����ʵ� : �ʵ尡 ����Ÿ���� ��쿡�� ���� ��ü�� null�� ���
            //nullReferenceException�� ����� ���
            myclass1 = null;
            GameObject someobj = myclass1?.obj;
            print(someobj);

            //b-2. ��ü?.�����ʵ�??�ٸ��ʵ�Ǵ°�ü : ��ü�� null�� ���, ??���� ���� ���� ��
            GameObject someObj2 = myclass1?.obj ?? new GameObject();
            print(someObj2);

            //c. ��ü?.���ʵ�??(�ʼ�)��ü �� : ��ü�� null ���, �����ϴ� �ʵ尡
            // ���ͷ� Ÿ���̶�� ������ ��ü ���� �����Ǿ�� ��

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

            nullableInt = null;// nullable ������ null�� �����ؼ� ������ ���

            //localInt = nullableInt.Value;//������ �����غ��� null�̱� ������ ������ �ȵ�

            if (nullableInt.HasValue)
            { //��������� null check
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
