using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public float boostSpeed; //�ν�Ʈ ����
    private float playerBoostSpeed; //�ν�Ʈ������ �÷��̾� �ӵ�
    private float beforeBoostSpeed; //�ν�Ʈ �� �����ӵ�

    private float preBoostTime; //���������� �ν�Ʈ�� ����� �ð�
    public float BoostInterval; //�ν�Ʈ ��Ÿ��
    public float boostDuration; //�ν�Ʈ ���ӽð�

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
