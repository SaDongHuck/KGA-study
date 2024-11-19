using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using UnityEngine.UI;
namespace MyProject
{
    public class AttributeControl : MonoBehaviour
    { // Scene�� �ִ� ��� Color ��Ʈ����Ʈ�� ã�Ƽ� ���� �����ִ� ���ҷ� ����� �ʹ�
        private void Start()
        {
            BindingFlags bind = BindingFlags.Public | BindingFlags.NonPublic |BindingFlags.Instance;
            MonoBehaviour[] monoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

            foreach (MonoBehaviour monoBehaviour in monoBehaviours)
            {
                Type type = monoBehaviour.GetType(); //Ÿ�� ������ ������

                //List<FieldInfo> fieldInfos = new List<FieldInfo>(type.GetFields(bind));
                //fieldInfos.FindAll((x) => { return x.HasAttribute<ColorAttribute>(); });
                //����Ʈ�� Collection���� Ž����
                //Linq�� ���� ����ȭ�� �� �� �ִ�
                //1.Linq���� ����ϴ� Ȯ�� �޼��� ���
                IEnumerable<FieldInfo> colorAttachedFields =
                    type.GetFields(bind).Where(x => x.HasAttribute<ColorAttribute>());

                //2. SQL, �������� ����� ����
                colorAttachedFields = from field in type.GetFields(bind)
                                      where field.HasAttribute<ColorAttribute>()
                                      select field;
                foreach(FieldInfo fieldinfo in colorAttachedFields)
                {
                    ColorAttribute att = fieldinfo.GetCustomAttribute<ColorAttribute>();
                    object value = fieldinfo.GetValue(monoBehaviour);

                    if(value is Renderer rend)
                    {
                        rend.material.color = att.color;
                    }
                    else if(value is Graphic graph)
                    {
                        graph.color = att.color;
                    }
                    else
                    {
                        print("color ��Ʈ�� ��Ʈ�� �߸��Ȱ��� �ٿ����ϴ�.");
                    }
                }
                IEnumerable<FieldInfo> sizeAttachedField =
                    type.GetFields(bind).Where(x => x.HasAttribute<SizeAttribute>());

                sizeAttachedField = from field in type.GetFields(bind)
                                    where field.HasAttribute<SizeAttribute>()
                                    select field;
                foreach(FieldInfo fieldinfo in sizeAttachedField)
                {
                    SizeAttribute att = fieldinfo.GetCustomAttribute<SizeAttribute>();
                    object size = fieldinfo.GetValue(monoBehaviour);

                    if(size is Transform transform)
                    {
                        transform.localScale = new Vector3(att.size.x, att.size.y, att.size.z); 
                    }
                    else
                    {
                        print("SizeAttribute�� �߸��� �ʵ忡 �پ� �ֽ��ϴ�.");
                    }
                }
            }
        }

    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ColorAttribute : Attribute
    {
        public Color color; 

        public ColorAttribute(float r = 0, float g =0, float b = 0, float a = 1)
        {
            color = new Color(r, g, b, a);
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class SizeAttribute : Attribute
    {
        public Vector3 size;

        public SizeAttribute(float x = 1, float y = 1, float z = 1)
        {
            size = new Vector3(x, y, z);
        }
    }

    public static class AttributeHelper
    {
        public static bool HasAttribute<T>(this MemberInfo Info) where T : Attribute
        {
            return Info.GetCustomAttributes(typeof(T), true).Length > 0;
        }
    }
}
