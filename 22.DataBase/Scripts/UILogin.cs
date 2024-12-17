using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    public class UILogin : MonoBehaviour
    {
        public TMP_InputField email;
        public TMP_InputField password;
        public Button loginButton;
        public Button signUpButton;

        private void Awake()
        {
            signUpButton.onClick.AddListener(SiginUPButtonClick);
            loginButton.onClick.AddListener(LoginButtonClick);
        }

        private void LoginButtonClick()
        {
            DataBaeManager.Instance.Login(email.text, password.text);
            loginButton.interactable = false;
        }
        private void SiginUPButtonClick()
        {
            UIManager.Instance.pageOpen("SignUp");
        }

    }
}
