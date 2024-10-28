using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartSceneCtl : MonoBehaviour
{
    public Button StartButton;

    private void Awake()
    {
        StartButton.onClick.AddListener(OnStartgame);

    }

    public void OnStartgame()
    {
        SceneManager.LoadScene("GameScene");
    }

    
}

