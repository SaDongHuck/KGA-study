using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingleTonManager<DataManager>
{
    //playerp

    public bool clearprefsOnStart;
    
    IEnumerator Start()
    {
        if (clearprefsOnStart) PlayerPrefs.DeleteAll(); //��� ���� ������ ����.
        yield return null;
        OnLoad();
    }

    //Load
    public void OnLoad()
    {
        //����̽��� ����� ���� ������ �� ���� Ű�� �ش��ϴ� ���� ������(Ű : string, �⺻��: int)
        int totalkillCount = PlayerPrefs.GetInt("TotalkillCount", 0);
        GameManager.Instance.player.totalkillCount = totalkillCount;   
    }

    public void OnSave()
    {
        int totalkillCount = GameManager.Instance.player.totalkillCount;

        PlayerPrefs.SetInt("TotalKillCount", totalkillCount); //���� Playerperfs�� ĳ�� ���� �Է� ( Ű: string, �� : int)

        //�������� �� Save() ȣ���ؾ� ������ �Ϸ�
        PlayerPrefs.Save();
    }

    //���μ����� ����ɶ� ȣ�� �Ǵ� �޽��� �Լ�
    private void OnApplicationQuit()
    {
        OnSave();
    }
}
