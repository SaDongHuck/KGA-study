using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillLevelUpPanel : MonoBehaviour
{
    public RectTransform list;
    public SkillLevelupButton SkillLevelupButton;

    //플레이거 레벨업 하면 패널 활성화 요청
    public void LevelUpPanelOpen(List<Skill> skillList, Action<Skill> callback)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f; 
        //스킬2개만 UI에 표시할 예정

        List<Skill> selectSkilllist = new();

        while(selectSkilllist.Count < 3) // 2개의 스킬이 선택 될 떄까지 반복
        {
            int renNUm = Random.Range(0,skillList.Count); //랜덤한 숫자 하나 뽑기

            Skill selectedskill = skillList[renNUm]; //랜덤하게 선택된 스킬가져오기

            if (selectSkilllist.Contains(selectedskill))
            {
                continue;
            }
            selectSkilllist.Add(selectedskill);

            SkillLevelupButton skillButton = Instantiate(SkillLevelupButton,list);

            skillButton.SetSkillSeletButton(selectedskill.skillName, ()
                =>
            {
                callback(selectedskill);
                LevelUpPanelClose();
            });


        }


    }


    //레벨업 패널을 닫을시 LevelUpPanelopen의 callback을 호출
    public void LevelUpPanelClose()
    {
        foreach (Transform buttons in list)
        {
            Destroy(buttons.gameObject);
        }
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
