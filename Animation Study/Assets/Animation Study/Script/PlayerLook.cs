using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform cameraRig;

    public float mouseSensivity;

    private float rigAngle = 0f;

    private void Update()
    {
        //MousePosition과 같은것임
        float mouseX = Input.GetAxis("Mouse X"); //mouse가 움직인 delta
        float mouseY = Input.GetAxis("Mouse Y");

        //마우스의 좌우 움직임에 맞춰 캐릭터의 Transform을 Rotate
        //근데 캐릭터가 Y축 기준으로 움직일대만 화면이 좌우로 움직인다.
        transform.Rotate(0, mouseX * mouseSensivity * Time.deltaTime, 0);

        //마우스의 상하 움직임에 맞춰 카메라 리그의 Transform을 Rotate
        //상하는 x축 기준으로 움직일때 움직인다. 근데x값이 커지면 내려가니까 -를 붙혀준다.
        cameraRig.Rotate(-mouseY * mouseSensivity * Time.deltaTime, 0, 0);


        rigAngle -= mouseY * mouseSensivity * Time.deltaTime;

        //카메라릭의 x축 각도를 제한
        rigAngle = Mathf.Clamp(rigAngle, -90f, 90f);

        //제한된 각도만큼 카메라릭 x축 각도를 변경
        cameraRig.localEulerAngles = new Vector3(rigAngle, 0, 0);

    }
}
