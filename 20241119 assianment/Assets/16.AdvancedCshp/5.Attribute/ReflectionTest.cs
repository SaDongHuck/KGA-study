using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Reflection;
using SAA = MyProject.SuperAwsomeAttribute;

//Reflection : System.Refelction ���ӽ����̽��� ���Ե� ��� ����
//������ Ÿ�ӿ��� ������ Ŭ����, �޼ҵ�, ������� �� ���� ���ý�Ʈ�� ���� �����͸�
//�����ϰ� ����ϴ� ���
//Attribute�� ������Ÿ�ӿ��� �����ϴ� ��Ÿ�������̹Ƿ� ���÷����� ���� �����͸� ������ �� �ִ�


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
            //atttest�� Ÿ���� Ȯ��
            MonoBehaviour attTestBoxingForm = attTest;
            Type attTesttype = attTestBoxingForm.GetType();
            print(attTesttype);
            //attributeTest��� Ŭ���� �����͸� �������� �������� �ð�
            BindingFlags bind = BindingFlags.Public | BindingFlags.Instance;
            //public���� ������ ������ ���ÿ� static�� �ƴ϶� ��ü���� ������ field �Ǵ� properties
            //attTestType : attTest�� GetType�� ���� Ŭ���� ���� ���� �����͸� ������ ����
            FieldInfo[] filedInfos = attTesttype.GetFields(bind);
            foreach(FieldInfo fileInfo in filedInfos)
            {
                SAA attribute = fileInfo.GetCustomAttribute<SAA>();
                print($"{fileInfo.Name}�� Ÿ���� {fileInfo.FieldType}");
                if(attribute is null )
                {
                    print($"{fileInfo.Name}���� ���� ��� ��Ʈ����Ʈ�� �����ϴ�");
                    continue;
                }

                print($"{fileInfo.Name}���� ���� ��� ��Ʈ����Ʈ�� �ֽ��ϴ�");
                print($"{attribute.GetAwesomeMessage}, {attribute.message}");
                print($"{fileInfo.GetValue(attTest)}");
            }


        }
    }
}
