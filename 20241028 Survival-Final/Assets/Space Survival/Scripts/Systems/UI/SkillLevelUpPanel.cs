using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLevelUpPanel : MonoBehaviour
{
    public RectTransform list;
    public SkillLevelUpButton buttonPrefab;

    //플레이어가 레벨업 하면 레벨 패널 활성화 요철
    public void LevelUpPanelOpen(List<Skill> skillList, System.Action<Skill> callback)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;

        //스킬 2개 UI에 표시
        List<Skill> selectedSkillList = new();
        while (selectedSkillList.Count < 2) //2개의 스킬이 선택 될때까지 반복
        {
            int ranNum = UnityEngine.Random.Range(0, skillList.Count); //랜덤한 숫자 하나 뽑기

            Skill selectedSkill = skillList[ranNum]; //랜덤하게 선택된 스킬 가져오기

            if (selectedSkillList.Contains(selectedSkill))
            {
                //이미 뽑힌 스킬이 또 뽑혔으면
                continue;
            }

            selectedSkillList.Add(selectedSkill); //뽑힌 스킬을 List에 집어넣음

            //선택된 스킬을 레벨업하는 버튼을 생성
            SkillLevelUpButton skillButton = Instantiate(buttonPrefab,list);

            skillButton.SetSkillSelectButton(selectedSkill.skillName,
                () =>
                {
                    callback(selectedSkill);
                    LevelUpPanelClose();
                });
        }
    }

    //레벨 패널을 닫을시 플레이어의 LevelUpPanelIpen의 collback을 호출
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
