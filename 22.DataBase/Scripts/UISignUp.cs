using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace MyProject
{
    public class UISignUp : MonoBehaviour
    {
        public TMP_InputField email;
        public TMP_InputField userName;
        public TMP_InputField password;

        public Button SignUpButton;
        public Button loginButton;

        private void Awake()
        {
            SignUpButton.onClick.AddListener(SiginUpButtonClick);
            loginButton.onClick.AddListener(LoginButtonClick);
        }

        private void OnEnable()
        {
            SignUpButton.interactable = true;
        }
        private void LoginButtonClick()
        {
            UIManager.Instance.pageOpen("LogIn");
        }

        private void SiginUpButtonClick()
        {
            DataBaeManager.Instance.SignUp(email.text, userName.text, password.text);
            SignUpButton.interactable = false;
        }
    }
}
