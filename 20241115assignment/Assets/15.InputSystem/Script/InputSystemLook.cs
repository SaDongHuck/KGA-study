using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyProject
{
    public class InputSystemLook : MonoBehaviour
    {
        public Transform CameraRig;
        public float mouseSesivity;
        private float rigAngle;

        public void OnLookEvent(InputAction.CallbackContext context)
        {
            Look(context.ReadValue<Vector2>());
        }
        private void OnLook(InputValue value)
        {
            print($"OnLook È£Ãâ °ª: {value.Get<Vector2>()}");
            Vector2 MouseDelta = value.Get<Vector2>();
            transform.Rotate(0, MouseDelta.x * mouseSesivity * Time.deltaTime, 0);
            rigAngle = math.clamp(rigAngle, -90f, 90f);
            CameraRig.localEulerAngles = new Vector3(rigAngle, 0, 0);   
        }

        private void Look(Vector2 MouseDelta)
        {
            transform.Rotate(0, MouseDelta.x * mouseSesivity * Time.deltaTime, 0);
            rigAngle -= MouseDelta.y * mouseSesivity* Time.deltaTime;
            rigAngle = math.clamp(rigAngle, -90f, 90f);
            CameraRig.localEulerAngles = new Vector3(rigAngle, 0, 0);
        }
    }
}
