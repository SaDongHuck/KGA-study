using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Globalization;
using System;

namespace MyProject
{
    public class UIPoPuP : MonoBehaviour
    {
        public TextMeshProUGUI title;
        public TextMeshProUGUI Message;
        public Button closeButton;

        private Action callback;

        private void Awake()
        {
            closeButton.onClick.AddListener(closeButtonClick);
        }
        public void PopupOpen(string title, string Message, Action callback)
        {
            this.title.text = title;
            this.Message.text = Message;
            this.callback = callback;
        }

        private void closeButtonClick()
        {
            callback?.Invoke(); 
        }
    }
}
