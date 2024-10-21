using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float maxhp;
    public float hp = 10f; // ä��
    public float damage = 5f;//���ݷ�
    public float moveSpeed = 5f;//�̵��ӵ�
    public Text killCountText;
    private int killCount = 0;


    public Projectile projectilePrefab; //����ü ������
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
            //vector3 - > vector2�� ĳ���� �� �� : z���� ����
            Vector2 mousePos = Input.mousePosition;
            Vector2 mouseScreenpos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 fireDir = mouseScreenpos - (Vector2)transform.position;

            Fire(fireDir);
        }
        hpBar.fillAmount = hpamount;
    }

    //�̵�
    //Transdform�� ���� ���� ������Ʈ�� �����̴� �Լ�
    // <param name = 'dir'> �̵�
    public void Move(Vector2 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    //����ü �߻�
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

    // UI ������Ʈ �Լ�
    private void UpdateKillCountUI()
    {
        if (killCountText != null)
        {
            killCountText.text = "Score: " + killCount.ToString();
        }
    }

}