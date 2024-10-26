using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]  
public class Skill
{
    public string skillName;
    public int skillLevel;
    public GameObject skillprefab;
    public bool isTargeting; //���� ����� ���� ���ϴ� ��ų����
    public GameObject[] skillprefabs; //5���� �������� �����Ͽ� �� ������ �´� �������� �ε��ϵ��� Ȱ��
    public GameObject currentSkillObject;

}
