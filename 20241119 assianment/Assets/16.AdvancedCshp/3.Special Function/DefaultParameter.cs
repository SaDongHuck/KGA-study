using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class DefaultParameter : MonoBehaviour
    {
        //기본 매개변수 : 매개변수에 전달할값을 할당을 안해도 기본적으로 특정 값이 전달 되도록 할 수 있다
        //런타임이 아닌 컴파일에서 알 수 있는 값이어야 함(리터럴)
        //[반환형] 함수이름(타입 매개변수명 = 기본값){}

        public Player newPlayer;
        private void Start ()
        {
            GameObject a = CreeateNewObject();
            a.name = "New Obj";

            GameObject b = createNewObject("new Obj2");

            newPlayer = CreatePlayer("사동혁", 0, 1, 2, 3, 4);
        }

        private GameObject CreeateNewObject()
        {
            return new GameObject();
        }

        private GameObject createNewObject(string name = "Some Obj")
        {
            GameObject retunobject = new GameObject();
            retunobject.name = name;
            return retunobject; 
        }

        private Player CreatePlayer(string name, int level = 1, float damage = 5f,
            Vector2 position = default, GameObject obj = null
            )
        {
            Player returnPlayer = new Player();
            returnPlayer.name = name;
            returnPlayer.level = level;
            returnPlayer.damage = damage;
            returnPlayer.position = position;
            returnPlayer.returnObject = obj;

            return returnPlayer;
        }

        //params 키워드 : 파라미터에 배열을 받고싶은 경우에 맨 마지막 배열
        //파라미터에 params 키워드를 붙여 두면, 배열 생성 대신 쉼표(,)로 구분하여
        //배열을 자동생성할 수 있는 파라미터

        private Player CreatePlayer(string name, int level = 1, params int[] items)
        {
            Player returnPlayer = CreatePlayer(name);
            returnPlayer.items = items;
            return returnPlayer;
        }

        [Serializable]
        public class Player
        {
            public string name;
            public int level;
            public float damage;
            public Vector2 position;
            public GameObject returnObject;
            public int[] items;
        }
    }
}
