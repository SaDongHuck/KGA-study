using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingleTonManager<DataManager>
{
    //playerp

    public bool clearprefsOnStart;
    
    IEnumerator Start()
    {
        if (clearprefsOnStart) PlayerPrefs.DeleteAll(); //모든 저장 데이터 삭제.
        yield return null;
        OnLoad();
    }

    //Load
    public void OnLoad()
    {
        //디바이스에 저장된 여러 데이터 중 같은 키에 해당하는 값을 사져옴(키 : string, 기본값: int)
        int totalkillCount = PlayerPrefs.GetInt("TotalkillCount", 0);
        GameManager.Instance.player.totalkillCount = totalkillCount;   
    }

    public void OnSave()
    {
        int totalkillCount = GameManager.Instance.player.totalkillCount;

        PlayerPrefs.SetInt("TotalKillCount", totalkillCount); //먼저 Playerperfs의 캐시 값을 입력 ( 키: string, 값 : int)

        //마지막에 꼭 Save() 호출해야 저장이 완료
        PlayerPrefs.Save();
    }

    //프로세스가 종료될때 호출 되는 메시지 함수
    private void OnApplicationQuit()
    {
        OnSave();
    }
}
