using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Reflection;
using SAA = MyProject.SuperAwsomeAttribute;

//Reflection : System.Refelction 네임스페이스에 포함된 기능 전반
//컴파일 타임에서 생성된 클래스, 메소드, 멤버변수 등 여러 컨택스트에 대한 데이터를
//색인하고 취급하는 기능
//Attribute는 컴파일타임에서 생성하는 메타데이터이므로 리플렉션을 통해 데이터를 가져올 수 있다


namespace MyProject
{
    [RequireComponent(typeof(AttributeTest))]
    public class ReflectionTest : MonoBehaviour
    {
        AttributeTest attTest;

        private void Awake()
        {
            attTest = GetComponent<AttributeTest>();
        }

        private void Start()
        {
            //atttest의 타입을 확인
            MonoBehaviour attTestBoxingForm = attTest;
            Type attTesttype = attTestBoxingForm.GetType();
            print(attTesttype);
            //attributeTest라는 클래스 데이터를 오목조목 따져보는 시간
            BindingFlags bind = BindingFlags.Public | BindingFlags.Instance;
            //public으로 접근이 가능한 동시에 static이 아니라 객체별로 생성할 field 또는 properties
            //attTestType : attTest의 GetType을 통해 클래스 명세에 대한 데이터를 가지고 있음
            FieldInfo[] filedInfos = attTesttype.GetFields(bind);
            foreach(FieldInfo fileInfo in filedInfos)
            {
                SAA attribute = fileInfo.GetCustomAttribute<SAA>();
                print($"{fileInfo.Name}의 타입은 {fileInfo.FieldType}");
                if(attribute is null )
                {
                    print($"{fileInfo.Name}에는 슈퍼 어썸 어트리뷰트가 없습니다");
                    continue;
                }

                print($"{fileInfo.Name}에는 슈퍼 어썸 어트리뷰트가 있습니다");
                print($"{attribute.GetAwesomeMessage}, {attribute.message}");
                print($"{fileInfo.GetValue(attTest)}");
            }


        }
    }
}
