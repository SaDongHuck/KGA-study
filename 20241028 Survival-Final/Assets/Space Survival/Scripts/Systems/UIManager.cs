using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//UI �����ϴ� �̱��� ������Ʈ
public class UIManager : SingletonManager<UIManager>
{
    //public static T intance => public static UIManamger instance;

    public Canvas mainCanvas;  //���� UI
    public GameObject pausePanel;//�Ͻ����� �г�
    public SkillLevelUpPanel levelUpPanel; //������ �г�

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

    //Reset �޽��� �Լ� : ������Ʈ�� ó�� �����ǰų�, ������Ʈ �޴��� Reset�� ������ ��� ȣ���
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

    
