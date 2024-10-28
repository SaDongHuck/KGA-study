using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//UI 관리하는 싱글톤 오브젝트
public class UIManager : SingletonManager<UIManager>
{
    //public static T intance => public static UIManamger instance;

    public Canvas mainCanvas;  //메인 UI
    public GameObject pausePanel;//일시정지 패널
    public SkillLevelUpPanel levelUpPanel; //레벨업 패널

    public Text killCountText;
    public Text totalKillCountText;
    public Text levelText;
    public Text expText;

    public Image hpBar;

    bool isPaused = false;

    protected override void Awake()
    {
        base.Awake();
        //mainCanvas = GetComponent<Canvas>();
        //pauePanel = transform.Find("PausePanel").gameObject;
        //levelUpPanel = transform.Find("LevelUpPanel").gameObject;

    }

    private void Start()
    {
        Restart();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            pausePanel.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
        }

        hpBar.fillAmount = GameManager.Instance.player.HpAmount;
        killCountText.text = GameManager.Instance.player.killCount.ToString();
        totalKillCountText.text = GameManager.Instance.player.totalKillCount.ToString();
    }

    //Reset 메시지 함수 : 컴포넌트가 처음 부착되거나, 컴포넌트 메뉴의 Reset을 선택할 경우 호출됨
    private void Reset()
    {
        mainCanvas = GetComponent<Canvas>();
        pausePanel = transform.Find("PausePanel")?.gameObject;
        levelUpPanel = transform.Find("LevelUpPanel")?.GetComponent<SkillLevelUpPanel>();
    }

    public void Restart()
    {
        pausePanel.SetActive(false);
        levelUpPanel.gameObject.SetActive(false);
    }
}

    
