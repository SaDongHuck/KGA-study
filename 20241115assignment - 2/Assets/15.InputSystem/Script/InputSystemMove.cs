using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



namespace MyProject
{
    [RequireComponent(typeof(CharacterController), typeof(Animator), typeof(PlayerInput))]
    public class InputSystemMove : MonoBehaviour
    {
        CharacterController charCtrl;
        Animator animator;

        public float walkSpeed;
        public float runSpeed;

        Vector2 inputValue;
        public InputActionAsset controlDefine;
        InputAction moveAction;

        public float smoothTime = 0.1f;
        Vector2 smoothValue;
        Vector2 currnetVelocity;

        private void Awake()
        {
            charCtrl = GetComponent<CharacterController>(); 
            animator = GetComponent<Animator>();
            controlDefine = GetComponent<PlayerInput>().actions;
            moveAction = controlDefine.FindAction("Move");
        }

        private void OnEnable()
        { 
            moveAction.performed += OnMoveEvent; //xxDown
            moveAction.canceled -= OnMoveEvent; // xxUP
        }

        private void OnDisable()
        {
            moveAction.performed -= OnMoveEvent;
            moveAction.canceled -= OnMoveEvent;
        }

        public void OnMoveEvent(InputAction.CallbackContext context)
        {
           print($"OnMoveEvent 호출 됨. context : {context.ReadValue<Vector2>()}");
           inputValue = context.ReadValue<Vector2>();
        }

        private void OnMove(InputValue value)
        {
            inputValue = value.Get<Vector2>();

            print($"파라미터 호출 됨 파라미터 : {inputValue}");

        }

        private void Update()
        {
            smoothValue = Vector2.SmoothDamp(smoothValue, inputValue, ref currnetVelocity, smoothTime);

            Vector3 inputMoveDir = new Vector3(inputValue.x, 0, inputValue.y) * walkSpeed;
            Vector3 actualMoveDir = transform.TransformDirection(inputMoveDir); 

            charCtrl.Move(actualMoveDir * Time.deltaTime);

            animator.SetFloat("Xdir", inputValue.x);
            animator.SetFloat("Ydir", inputValue.y);
            animator.SetFloat("Speed", inputValue.magnitude);
        }
    }
}
