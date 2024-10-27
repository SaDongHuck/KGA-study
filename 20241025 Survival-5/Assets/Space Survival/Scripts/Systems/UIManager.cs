using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : SingleTonManager<UIManager>
{
    //public statcic T instance => public static UIManager instance;
    public Canvas maincanvas; //���� UICnavas
    public GameObject pausePanel; //�Ͻ����� �г�
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
        if(Input.GetKeyDown(KeyCode.Escape)) //esc Ű
        {
            isPaused = !isPaused;
            pausePanel.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }

    //Reset�޽��� �Լ� : ������Ʈ�� ó�� �����ǰų� ������Ʈ�� �޴��� Rest�� ������ ��� ȣ��
    private void Reset()
    {
        maincanvas = GetComponent<Canvas>();
        pausePanel = transform.Find("PausePanel")?.gameObject;
        levelupPanel = transform.Find("levelupPanel")?.GetComponent<SkillLevelUpPanel>();
    }


   

}
