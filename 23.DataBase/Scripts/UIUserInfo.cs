using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace MyProject
{
    public class UIUserInfo : MonoBehaviour
    {
        public TextMeshProUGUI userName;
        public TextMeshProUGUI CharacterClass;
        public TextMeshProUGUI level;

        public Button levelUPButon;
        public Button UserDeleteButton;

        public Button RankingButton;

        private UserData UserData;


        private void Awake()
        {
            levelUPButon.onClick.AddListener(levelUPclick);
            UserDeleteButton.onClick.AddListener(DeleteClick);
            RankingButton.onClick.AddListener(Ranking);
        }

        private void OnEnable()
        {
            levelUPButon.interactable = true;
            UserDeleteButton.interactable = true;
            RankingButton.interactable = true;
        }
        public void UserInfoOpen(UserData userData)
        {
            this.UserData = userData; 
            userName.text = userData.userName;
            CharacterClass.text = userData.characterClass;
            level.text = $"LV.{UserData.level}";
        }

        public void levelUPclick()
        {
            UserData.level++;
            DataBaeManager.Instance.LevelUP(UserData);
            level.text = $"Lv.{UserData.level}";
            levelUPButon.interactable = false;
        }

        public void DeleteClick()
        {
            DataBaeManager.Instance.Delete(UserData);
            UserDeleteButton.interactable = false;
        }

        public void Ranking()
        {
            UIManager.Instance.pageOpen("Ranking");
            RankingButton.interactable = false;
        }
        
    }
}
