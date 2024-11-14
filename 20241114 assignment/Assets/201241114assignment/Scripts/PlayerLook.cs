using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace MyProject
{
    public class PlayerLook : MonoBehaviour
    {
        public Transform camraRig;

        public float mouseSensivity;

        private float rigAngle = 0f;
        public void Update()
        {
            float mouseX = Input.GetAxis("Mouse X"); //mouse가 움직인 delta
            float mouseY = Input.GetAxis("Mouse Y");

            //마우스의 좌우 움직임에 맞춰 캐릭터의 Tranform을 Rotate
            transform.Rotate(0, mouseX * mouseSensivity * Time.deltaTime, 0 );

            rigAngle -= -mouseY * mouseSensivity * Time.deltaTime;

            //카메라틱의 x축 각도를 제한
            rigAngle = Mathf.Clamp(rigAngle, -90f, 90f);

            //제한된 각도만큼 카메라틱의 x축 각도를 변경
            camraRig.localEulerAngles = new Vector3(rigAngle, 0, 0);

            //마우스의 상하 움직임에 맞춰 카메라 리그 transform을 Rotate
            /*camraRig.Rotate(-mouseY * mouseSensivity * Time.deltaTime, 0, 0);

            Vector3 cameraRigRotatioEuler = camraRig.eulerAngles;

            cameraRigRotatioEuler.y = math.clamp(cameraRigRotatioEuler.x, 0, 180);

            camraRig.eulerAngles = cameraRigRotatioEuler;*/
        }
    }
}
