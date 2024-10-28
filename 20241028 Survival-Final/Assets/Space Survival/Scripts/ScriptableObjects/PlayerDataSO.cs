using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

//플레이어 데이터를 취급하는 ScriptableObject
//1.MonoBehaviour가 아닌 ScriptableObject를 상속 시킴
//
[CreateAssetMenu(fileName = "Player Date", menuName = "Scriptable Object/Player Data", order = 0)]
public class PlayerDataSO : ScriptableObject
{
    //asset 파일로 생성 후 데이터를 입력할 수 있음
    //2. 파일 생성 
    public string playerName;
    public float health;
    public float damage;
    public float moveSpeed;

    public Sprite sp;
    public GameObject startSkillPrefab;
    public bool rotateRenderer;
    
}
