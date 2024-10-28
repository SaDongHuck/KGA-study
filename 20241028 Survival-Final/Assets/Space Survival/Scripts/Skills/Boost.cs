using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public float boostSpeed; //부스트 배율
    private float playerBoostSpeed; //부스트했을때 플레이어 속도
    private float beforeBoostSpeed; //부스트 전 원본속도

    private float preBoostTime; //마지막으로 부스트를 사용한 시간
    public float BoostInterval; //부스트 쿨타임
    public float boostDuration; //부스트 지속시간

    private IEnumerator Start()
    {
        yield return null; 
        playerBoostSpeed = GameManager.Instance.player.moveSpeed * boostSpeed;
        beforeBoostSpeed = GameManager.Instance.player.moveSpeed;
    }
    private void Update()
    {
        if (preBoostTime + BoostInterval > Time.time) { return; }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("GetKeyDown");
            StartCoroutine(BoostCoroutine(boostDuration));
            preBoostTime = Time.time;
        }
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    GameManager.Instance.player.moveSpeed = playerBoostSpeed;
        //    Debug.Log($"Player Speed : {GameManager.Instance.player.moveSpeed}\nisBoost : {isBoost}");
        //}
        //if (Input.GetKeyUp(KeyCode.LeftShift)) 
        //{
        //    Debug.Log($"Player Speed : {GameManager.Instance.player.moveSpeed}\nGetKeyUp" );
        //    GameManager.Instance.player.moveSpeed = beforeBoostSpeed;
        //}
        Debug.Log($"Player Speed : {GameManager.Instance.player.moveSpeed}");
    }

    private IEnumerator BoostCoroutine(float duration)
    {
        GameManager.Instance.player.moveSpeed = playerBoostSpeed; 
        yield return new WaitForSeconds(duration);
        GameManager.Instance.player.moveSpeed = beforeBoostSpeed;
    }
}
