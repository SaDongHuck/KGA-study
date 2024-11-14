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
            float mouseX = Input.GetAxis("Mouse X"); //mouse�� ������ delta
            float mouseY = Input.GetAxis("Mouse Y");

            //���콺�� �¿� �����ӿ� ���� ĳ������ Tranform�� Rotate
            transform.Rotate(0, mouseX * mouseSensivity * Time.deltaTime, 0 );

            rigAngle -= -mouseY * mouseSensivity * Time.deltaTime;

            //ī�޶�ƽ�� x�� ������ ����
            rigAngle = Mathf.Clamp(rigAngle, -90f, 90f);

            //���ѵ� ������ŭ ī�޶�ƽ�� x�� ������ ����
            camraRig.localEulerAngles = new Vector3(rigAngle, 0, 0);

            //���콺�� ���� �����ӿ� ���� ī�޶� ���� transform�� Rotate
            /*camraRig.Rotate(-mouseY * mouseSensivity * Time.deltaTime, 0, 0);

            Vector3 cameraRigRotatioEuler = camraRig.eulerAngles;

            cameraRigRotatioEuler.y = math.clamp(cameraRigRotatioEuler.x, 0, 180);

            camraRig.eulerAngles = cameraRigRotatioEuler;*/
        }
    }
}
