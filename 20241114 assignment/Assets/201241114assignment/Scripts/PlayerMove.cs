using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class PlayerMove : MonoBehaviour
    {
        #region private Components
        CharacterController charCtrl;
        Animator animator;
        #endregion

        #region public Fiedls

        public float walkSpeed;
        public float runSpeed;

        #endregion

        #region private Fiedls

        private float currentSpeed;

        #endregion
        private void Awake()
        {
            charCtrl = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector3 inputVale = Vector3.zero;
            inputVale.x = Input.GetAxis("Horizontal");
            inputVale.z = Input.GetAxis("Vertical");

            inputVale = Vector3.ClampMagnitude(inputVale, 1);

            float runVlaue = Input.GetAxis("Fire3");

            //내 캐릭터가 걷고 있는 속도 + 내 캐릭터가 뛰고 있을 때 속도
            currentSpeed = (inputVale.magnitude * walkSpeed) + (runVlaue * (runSpeed - walkSpeed));   

            Vector3 inputmoveDir = inputVale * currentSpeed;

            Vector3 actualMoveDir = transform.TransformDirection(inputmoveDir);

            charCtrl.Move(actualMoveDir * Time.deltaTime);

            animator.SetFloat("Xdir", inputVale.x);
            animator.SetFloat("Ydir", inputVale.z);
            animator.SetFloat("Speed", inputVale.magnitude+runVlaue);
        }

        

    }
}
