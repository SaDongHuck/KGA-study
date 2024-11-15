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
           print($"OnMoveEvent ȣ�� ��. context : {context.ReadValue<Vector2>()}");
           inputValue = context.ReadValue<Vector2>();
        }

        private void OnMove(InputValue value)
        {
            inputValue = value.Get<Vector2>();

            print($"�Ķ���� ȣ�� �� �Ķ���� : {inputValue}");

        }

        private void Update()
        {
            Vector3 inputMoveDir = new Vector3(inputValue.x, 0, inputValue.y) * walkSpeed;
            Vector3 actualMoveDir = transform.TransformDirection(inputMoveDir); 

            charCtrl.Move(actualMoveDir * Time.deltaTime);

            animator.SetFloat("Xdir", inputValue.x);
            animator.SetFloat("Ydir", inputValue.y);
            animator.SetFloat("Speed", inputValue.magnitude);
        }
    }
}
