using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MyProject
{
    public class UIRanking : MonoBehaviour
    {
        public TextMeshProUGUI[] UserName;
        public TextMeshProUGUI[] CharacterClass;
        public TextMeshProUGUI[] Level;

        private async void Start()
        {
            List<UserData> rankList = await DataBaeManager.Instance.Ranking(10);
            UpdateRanking(rankList);
        }

        private void UpdateRanking(List<UserData> rankList)
        {
            for (int i = 0; i < UserName.Length; i++)
            {
                if (i < rankList.Count)
                {
                    UserName[i].text = rankList[i].userName;
                    CharacterClass[i].text = rankList[i].characterClass; 
                    Level[i].text = $"LV.{rankList[i].level}"; 
                }
                else
                {
                    
                    UserName[i].text = "---";
                    CharacterClass[i].text = "---";
                    Level[i].text = "LV.---";
                }
            }
        }
    }
}
