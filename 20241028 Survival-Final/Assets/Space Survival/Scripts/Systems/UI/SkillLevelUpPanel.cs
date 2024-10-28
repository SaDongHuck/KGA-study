using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLevelUpPanel : MonoBehaviour
{
    public RectTransform list;
    public SkillLevelUpButton buttonPrefab;

    //�÷��̾ ������ �ϸ� ���� �г� Ȱ��ȭ ��ö
    public void LevelUpPanelOpen(List<Skill> skillList, System.Action<Skill> callback)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;

        //��ų 2�� UI�� ǥ��
        List<Skill> selectedSkillList = new();
        while (selectedSkillList.Count < 2) //2���� ��ų�� ���� �ɶ����� �ݺ�
        {
            int ranNum = UnityEngine.Random.Range(0, skillList.Count); //������ ���� �ϳ� �̱�

            Skill selectedSkill = skillList[ranNum]; //�����ϰ� ���õ� ��ų ��������

            if (selectedSkillList.Contains(selectedSkill))
            {
                //�̹� ���� ��ų�� �� ��������
                continue;
            }

            selectedSkillList.Add(selectedSkill); //���� ��ų�� List�� �������

            //���õ� ��ų�� �������ϴ� ��ư�� ����
            SkillLevelUpButton skillButton = Instantiate(buttonPrefab,list);

            skillButton.SetSkillSelectButton(selectedSkill.skillName,
                () =>
                {
                    callback(selectedSkill);
                    LevelUpPanelClose();
                });
        }
    }

    //���� �г��� ������ �÷��̾��� LevelUpPanelIpen�� collback�� ȣ��
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
