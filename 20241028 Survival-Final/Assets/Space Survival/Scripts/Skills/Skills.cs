using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Skill
{
    public string skillName;
    public int skillLevel;
    public GameObject[] skillPrefab; //5���� �������� �����Ͽ� �� ������ �´� �������� �ε�
    public bool isTargeting;
    public bool isPassive;
    public GameObject currentSkillObject;
}
