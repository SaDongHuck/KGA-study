using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace MyProject
{
    public class UIManager : MonoBehaviour
    {
        public UISignUp signUpPage;//회원가입 페이지
        public UILogin logInPage;//로그인 페이지
        public UIUserInfo userInfo;
        public UIPoPuP popup;
        public UIRanking ranking;

        private Dictionary<string, GameObject> pages = new Dictionary<string, GameObject>();

        private GameObject currentPage; //현재 열려있는 페이지

        public static  UIManager Instance;
        private void Awake()
        {
            Instance = this;    
            pages.Add("SignUp", signUpPage.gameObject);
            pages.Add("LogIn", logInPage.gameObject);
            pages.Add("UserInfo", userInfo.gameObject);
            pages.Add("Popup", popup.gameObject);
            pages.Add("Ranking", ranking.gameObject);
        }

        private void Start()
        {
            foreach(GameObject page in pages.Values)
            {
                page.SetActive(false);
            }
            pageOpen("LogIn");
        }

        public void pageOpen(string pageName)
        {
            if(pages.ContainsKey(pageName))
            {
                currentPage?.SetActive(false);
                currentPage = pages[pageName];
                currentPage.SetActive(true);    
            }
        }
    }
}
