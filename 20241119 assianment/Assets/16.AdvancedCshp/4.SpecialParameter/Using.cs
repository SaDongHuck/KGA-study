//using 키워드
//1. 외부의 라이브러리 중 내가 사용할 라이브러리를 추가
// C/C++ #inclue와 비슷하고, JAVA의 import와 똑같다
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
//2. 이 .cs파일에서만 사용할 컨텍스트(클래스, 구조체, 대리자, 인터페이스등) 이름을 지정할 수 있음
using Rn = UnityEngine.Random;

namespace MyProject
{
    public class Using : MonoBehaviour
    {
        //~ : C++의 소멸자와 같은 소멸자 자체는 C#에서도 정의가 가능하난
        // 기본적으로 IDisposable 인터페이스를 통해 Dispose()를 호출하도록 함
        private void Start()
        {
            //3. IDusposable 인터페이스를 구현한 객체가 특정 블록 내에서만 동작을 한 후에
            //블록 끝에서 암시적으로 메모리를 해제하도록 하는 기능
            using (HttpClient httpClient = new HttpClient())
            {

            }
            HttpClient client = new HttpClient (); 

            client.Dispose ();
        }
    }
}
