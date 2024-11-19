using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using UnityEngine;


//�׷��� attribute�� ������: �������ǹ�, �Ӽ�, Ư��, Ư¡���� ����
//c#������ Attribute�� Ư�� ���ؽ�Ʈ(Ŭ���� ����, �Լ� ���� ������ ����)�� ����
//������ Ÿ�ӿ��� �־����� ��Ÿ������

namespace MyProject
{
    public class AttributeTest : MonoBehaviour
    {
        //Attribute�� ����ϴ� ���. ��� ���ý�Ʈ �տ� [] ���̿� attribute Ŭ������ ����� Ŭ������ �̸�
        //(���� Attribute�� �� �̸�)�� ������ �ȴ�

        [TextArea(4,15)]
        public string sometext;

        [SuperAwsome(GetAwesomeMessage = "", message = "")]
        public int awesomeInt;
       
    }


    //�����ڰ� �ۼ��� Ŀ���� ��Ʈ����ũ (System.Attribute�� ����� Ŭ����)�տ�
    //AttributeUsageAttribute��� ��Ʈ����Ʈ�� �߰��Ͽ� �ش� ��Ʈ����Ʈ�� ����� �����ϰų� �߰� ������ ����

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field)]
    public class SuperAwsomeAttribute : Attribute
    {
        public string message;
        public string GetAwesomeMessage;

        public SuperAwsomeAttribute()
        {
            message = "I'm Super Awsome";
            GetAwesomeMessage = "Super Awesome";
        }

        public SuperAwsomeAttribute(string message)
        {
            this.message = message;
        }
        
    } 
    
}
