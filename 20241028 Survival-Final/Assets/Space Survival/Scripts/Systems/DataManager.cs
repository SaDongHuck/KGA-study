using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : SingletonManager<DataManager>
{
    //PlayerPrefs 클래스 : 디바이스에 저장된 게임 데이터를 불러오거나, 
    //디바이스에 게임 데이터를 저장하는 기능을 담당하는 클래스
    //주로 정적 함수를 호출하여 기능을 활용함

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
        PlayerPrefs.SetInt("TotalKillCount",totalKillCount); //먼저 PlayerPrefs의 캐시에 값을 입력, (Key string, Value int)
        PlayerPrefs.Save();
    }

    //프로세스가 종료될 때 호출되는 메시지 함수.
    private void OnApplicationQuit()
    {
        OnSave();
    }

    //Load
    public void OnLoad()
    {
        //기본적으로는 키만 입력대혿 값을 가져올 수 있음
        //디바이스에 저장된 여러 데이터중 같은 키에 해당하는 값을 가져옴 (Key string, Value int)
        int totalKillCount = PlayerPrefs.GetInt("TotalKillCount", 0); //기본값 : 0
        GameManager.Instance.player.totalKillCount = totalKillCount;
    }

}
