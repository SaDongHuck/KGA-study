using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillLevelUpPanel : MonoBehaviour
{
    public RectTransform list;
    public SkillLevelupButton SkillLevelupButton;

    //�÷��̰� ������ �ϸ� �г� Ȱ��ȭ ��û
    public void LevelUpPanelOpen(List<Skill> skillList, Action<Skill> callback)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f; 
        //��ų2���� UI�� ǥ���� ����

        List<Skill> selectSkilllist = new();

        while(selectSkilllist.Count < 3) // 2���� ��ų�� ���� �� ������ �ݺ�
        {
            int renNUm = Random.Range(0,skillList.Count); //������ ���� �ϳ� �̱�

            Skill selectedskill = skillList[renNUm]; //�����ϰ� ���õ� ��ų��������

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


    //������ �г��� ������ LevelUpPanelopen�� callback�� ȣ��
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
