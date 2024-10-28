using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

//�÷��̾� �����͸� ����ϴ� ScriptableObject
//1.MonoBehaviour�� �ƴ� ScriptableObject�� ��� ��Ŵ
//
[CreateAssetMenu(fileName = "Player Date", menuName = "Scriptable Object/Player Data", order = 0)]
public class PlayerDataSO : ScriptableObject
{
    //asset ���Ϸ� ���� �� �����͸� �Է��� �� ����
    //2. ���� ���� 
    public string playerName;
    public float health;
    public float damage;
    public float moveSpeed;

    public Sprite sp;
    public GameObject startSkillPrefab;
    public bool rotateRenderer;
    
}
