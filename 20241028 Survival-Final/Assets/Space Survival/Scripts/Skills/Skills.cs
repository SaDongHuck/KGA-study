using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Skill
{
    public string skillName;
    public int skillLevel;
    public GameObject[] skillPrefab; //5개의 프리팹을 참조하여 각 레벨에 맞는 프리팹을 로드
    public bool isTargeting;
    public bool isPassive;
    public GameObject currentSkillObject;
}
