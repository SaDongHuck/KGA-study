using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Play : MonoBehaviour
{
    public float movespeed = 5f;
    public GameObject bullet;
    public GameObject bullet1;
    public Transform bulletLocation;
    public Transform bulletLocation1;
    public float bulletDely;
    public bool isTouchTop;
    public bool isTouchRight;
    public bool isTouchLeft;
    private bool bulletState;
    private Rigidbody2D rb;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        bulletState = true;
    }

    void Update()
    {
        bullet_shoot();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        if ((isTouchRight && x == 1) || (isTouchLeft && x == -1))
            x = 0;
        float y = Input.GetAxisRaw("Vertical");
        if(isTouchTop && y == 1)
            y = 0;
        rb.velocity = new Vector2(x * movespeed, y * movespeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            Vector2 pushDirection = (transform.position - other.transform.position).normalized;
            rb.AddForce(pushDirection * 10f, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            switch(collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void bullet_shoot()
    {
        if(bulletState)
        {
            if(Input.GetKey(KeyCode.T))
            {
                StartCoroutine(bulletcontrol());
                Instantiate(bullet, bulletLocation.position, bulletLocation.rotation);
                Instantiate(bullet1, bulletLocation1.position, bulletLocation1.rotation);
            }
        }
    }

    private IEnumerator bulletcontrol()
    {
        bulletState = false;
        yield return new WaitForSeconds(bulletDely);
        bulletState = true;
    }


    


}
