using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class DefaultParameter : MonoBehaviour
    {
        //�⺻ �Ű����� : �Ű������� �����Ұ��� �Ҵ��� ���ص� �⺻������ Ư�� ���� ���� �ǵ��� �� �� �ִ�
        //��Ÿ���� �ƴ� �����Ͽ��� �� �� �ִ� ���̾�� ��(���ͷ�)
        //[��ȯ��] �Լ��̸�(Ÿ�� �Ű������� = �⺻��){}

        public Player newPlayer;
        private void Start ()
        {
            GameObject a = CreeateNewObject();
            a.name = "New Obj";

            GameObject b = createNewObject("new Obj2");

            newPlayer = CreatePlayer("�絿��", 0, 1, 2, 3, 4);
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

        //params Ű���� : �Ķ���Ϳ� �迭�� �ް���� ��쿡 �� ������ �迭
        //�Ķ���Ϳ� params Ű���带 �ٿ� �θ�, �迭 ���� ��� ��ǥ(,)�� �����Ͽ�
        //�迭�� �ڵ������� �� �ִ� �Ķ����

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
