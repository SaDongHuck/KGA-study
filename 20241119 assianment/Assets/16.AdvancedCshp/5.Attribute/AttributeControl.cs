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
    { // Scene에 있는 모든 Color 어트리뷰트를 찾아서 색을 입혀주는 역할로 만들고 싶다
        private void Start()
        {
            BindingFlags bind = BindingFlags.Public | BindingFlags.NonPublic |BindingFlags.Instance;
            MonoBehaviour[] monoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

            foreach (MonoBehaviour monoBehaviour in monoBehaviours)
            {
                Type type = monoBehaviour.GetType(); //타입 정보를 가져옴

                //List<FieldInfo> fieldInfos = new List<FieldInfo>(type.GetFields(bind));
                //fieldInfos.FindAll((x) => { return x.HasAttribute<ColorAttribute>(); });
                //리스트등 Collection에서 탐색은
                //Linq를 통해 간소화를 할 수 있다
                //1.Linq에서 재공하는 확장 메서드 사용
                IEnumerable<FieldInfo> colorAttachedFields =
                    type.GetFields(bind).Where(x => x.HasAttribute<ColorAttribute>());

                //2. SQL, 쿼리문과 비슷한 형태
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
                        print("color 어트리 뷰트가 잘못된곳에 붙였습니다.");
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
                        print("SizeAttribute가 잘못된 필드에 붙어 있습니다.");
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
