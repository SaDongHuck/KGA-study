using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float maxhp;
    public float hp = 10f; // 채력
    public float damage = 5f;//공격력
    public float moveSpeed = 5f;//이동속도
    public Text killCountText;
    private int killCount = 0;


    public Projectile projectilePrefab; //투사체 프리팹
    public float hpamount { get { return hp / maxhp; } }
    public Image hpBar;

    void Start()
    {
        maxhp = hp;
        UpdateKillCountUI();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(x, y);

        Move(dir);

        if (Input.GetButtonDown("Fire1"))
        {
            //vector3 - > vector2로 캐스팅 할 때 : z값이 생략
            Vector2 mousePos = Input.mousePosition;
            Vector2 mouseScreenpos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 fireDir = mouseScreenpos - (Vector2)transform.position;

            Fire(fireDir);
        }
        hpBar.fillAmount = hpamount;
    }

    //이동
    //Transdform을 통해 게임 오브젝트를 움직이는 함수
    // <param name = 'dir'> 이동
    public void Move(Vector2 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    //투사체 발사
    public void Fire(Vector2 dir)
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position,
            Quaternion.identity);
        projectile.transform.up = dir;
        projectile.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            hp -= damage;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void IncrementKillCount()
    {
        killCount++;
        UpdateKillCountUI();
        Debug.Log("Kill Count: " + killCount);
    }

    // UI 업데이트 함수
    private void UpdateKillCountUI()
    {
        if (killCountText != null)
        {
            killCountText.text = "Score: " + killCount.ToString();
        }
    }

}