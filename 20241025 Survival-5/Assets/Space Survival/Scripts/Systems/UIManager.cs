using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : SingleTonManager<UIManager>
{
    //public statcic T instance => public static UIManager instance;
    public Canvas maincanvas; //메인 UICnavas
    public GameObject pausePanel; //일시정지 패널
    public SkillLevelUpPanel levelupPanel;

    public Text killcountText;
    public Text TotlaKillCountText;
    public Text levelText;
    public Text expText;
    public Image hpBar;

    protected override void Awake()
    {
        base.Awake();
        //maincanvas = GetComponent<Canvas>();
        //pausePanel = transform.Find("PausePanel").gameObject;
        //levelupPanel = transform.Find("levelupPanel").gameObject.gameObject 
    }

    private void Start()
    {
        pausePanel.SetActive(false);
        levelupPanel.gameObject.SetActive(false);
    }

    bool isPaused = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //esc 키
        {
            isPaused = !isPaused;
            pausePanel.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }

    //Reset메시지 함수 : 컴포넌트가 처음 부착되거나 컴포넌트의 메뉴의 Rest을 선택한 경우 호출
    private void Reset()
    {
        maincanvas = GetComponent<Canvas>();
        pausePanel = transform.Find("PausePanel")?.gameObject;
        levelupPanel = transform.Find("levelupPanel")?.GetComponent<SkillLevelUpPanel>();
    }


   

}
