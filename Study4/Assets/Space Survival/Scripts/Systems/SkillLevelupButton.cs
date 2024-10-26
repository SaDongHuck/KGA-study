using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillLevelupButton : MonoBehaviour
{
    public Text skillNameText;
    public Button button;

    public void SetSkillSeletButton(string skillname, UnityAction onclick)
    {
        skillNameText.text = skillname;
        button.onClick.AddListener(() => onclick());
    }
}
