using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CCPlayer : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.1f;
    public float jumpHeight = 2.0f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGreounded;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGreounded = controller.isGrounded;

        if(isGreounded && velocity.y < 0 )
        {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(move * speed * Time.deltaTime );

        if (Input.GetButtonDown("Jump") && isGreounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // 점프 속도 계산
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.layer == LayerMask.NameToLayer("Box"))
        {
            Debug.Log("Player와 Box가 충돌했습니다!");
        }
    }
}
