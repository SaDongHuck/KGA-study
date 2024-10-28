using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : SingletonManager<DataManager>
{
    //PlayerPrefs Ŭ���� : ����̽��� ����� ���� �����͸� �ҷ����ų�, 
    //����̽��� ���� �����͸� �����ϴ� ����� ����ϴ� Ŭ����
    //�ַ� ���� �Լ��� ȣ���Ͽ� ����� Ȱ����

    public bool clearPrefsOnStart;

    public int totalkillCount;

    IEnumerator Start()
    {
        if (clearPrefsOnStart)
        {
            PlayerPrefs.DeleteAll();
            yield return null;
            OnLoad();
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if (scene == SceneManager.GetSceneByName("GameScene"))
                {
                    OnLoad();
                }
            };
        }
    }

    //save
    public void OnSave()
    {
        int totalKillCount = GameManager.Instance.player.totalKillCount;
        PlayerPrefs.SetInt("TotalKillCount",totalKillCount); //���� PlayerPrefs�� ĳ�ÿ� ���� �Է�, (Key string, Value int)
        PlayerPrefs.Save();
    }

    //���μ����� ����� �� ȣ��Ǵ� �޽��� �Լ�.
    private void OnApplicationQuit()
    {
        OnSave();
    }

    //Load
    public void OnLoad()
    {
        //�⺻�����δ� Ű�� �Է´��C ���� ������ �� ����
        //����̽��� ����� ���� �������� ���� Ű�� �ش��ϴ� ���� ������ (Key string, Value int)
        int totalKillCount = PlayerPrefs.GetInt("TotalKillCount", 0); //�⺻�� : 0
        GameManager.Instance.player.totalKillCount = totalKillCount;
    }

}
