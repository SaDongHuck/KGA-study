using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace MyProject
{
    public class ExtensionMethod : MonoBehaviour
    {
        //Ư�� ��Ҹ� �Ķ���� ��� �տ� �����Ͽ� ��ġ �ش� ��ü�� ���� �޼ҵ��ΰ� ó��
        //����ϴ� ���

        private void Start()
        {
            StringHelper.StaticMethod();
            string a = StringHelper.Append("��", "��");
            print(a);
            string b = "��".Append("��");
            print(b);
            /*DateTime today = DateTime.Now;
            DateTime nextWeek = DateTime.Parse("2024�� 11�� 25��");
            print(today.To(nextWeek));*/                              
        }

        //static Ŭ���� : ���� ��ü�� �������� �ʾƵ� data������ ������ �޼ҵ��� ���Ǹ� ���ϰ� �ִ� Ŭ����
    }

    public static class StringHelper
    {
        public static void StaticMethod() { }

        //static �޼ҵ��� ù �Ķ���� �տ� this Ű���尡 ������
        // �ش� �Ķ���ʹ� .�Լ� �տ� ���� ����
        public static string /*perfix.*/ Append(this string perfix, string postfix)
        {
            return perfix + postfix;
        }
    }

    public static class DateTimeHelper
    {
        public static TimeSpan To(this DateTime start, DateTime end)
        {

            return start - end;
        }
    }
}
