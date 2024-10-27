using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //public float maxHp = 10f;//�ϼ�
    private float maxHp;
    public float hp = 10f; //ü��
    public float damage = 10f; //���ݷ�
    public float moveSpeed = 3f; //�̵� �ӵ�
    public ParticleSystem impactParticle;

    //�ʰ��
    public float hpAmount { get { return hp / maxHp; } } //���� ���Ǵ� �׸��� ������Ƽ�� �����
    //Getter/Setter

    private Transform target; //������ ���

    public Image hpBar;

    private Rigidbody2D rb;

    public int exp = 25;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //��ŸƮ �޽��� �Լ��� �ڷ�ƾ�� �� �� �ִ�.
    //�׸��� ��ŸƮ �Լ��� �ڷ�ƾ�̸� �˾Ƽ� StartCotoutine���� ����ȴ�.
    private IEnumerator Start()
    {
        GameManager.Instance.enemies.Add(this); //�� ����Ʈ�� �ڱ� �ڽ��� Add
        maxHp = hp;

        yield return null;//�������� ����.

        target = GameManager.Instance.player.transform;
    }

    private void Update()
    {

        //?. ?? null check ���� ������.
        //if (target == null) { return; } ������ �̷������� �ϴ°� �´µ�
        //�� �����ڸ� ���� target�� null�̸� ?? �ڿ��ִ� Vector2.zero�� ��ȯ��
        // ==> target != null ? target.position - transform.position : Vector2.zero;
        Vector2 moveDir = target?.position - transform.position ?? Vector2.zero;
        Move(moveDir.normalized);
        //print(moveDir.magnitude);//vector.magnitude:�ش� ���Ͱ� "���⺤��"�� ���ֵ� ��, ������ ����
        //print(moveDir.normalized);//������ ������ä ���̰� 1�� ������ ����.
        hpBar.fillAmount = hpAmount;
    }

    public void Move(Vector2 dir)//dir ���� Ŀ���� 1�� ������ �ϰ� �������=>normalized
    {
        //transform.Translate(dir * moveSpeed * Time.deltaTime);

        Vector2 movePos = rb.position + (dir * moveSpeed * Time.fixedDeltaTime);
        //transform.movePos
        rb.MovePosition(movePos);
    }

    //OnHit,
    public void TakeDamage(float damage)
    {
        hp -= damage;

        if(hp <= 0) //���� ���
        {
            Die();
        }

    }

    

    public void Die()
    {
        GameManager.Instance.enemies.Remove(this);
        GameManager.Instance.player.killCount++;
        GameManager.Instance.player.GainEXP(exp);
        Destroy(gameObject);
    }

    public float damageInterval; //������ ����
    private float preDamageTime; //������ �������� �� �ð�(Time.time)

    private void OnCollisionStay2D(Collision2D collision)
    {
        //�÷��̾�� ������ �ִ� ���� �����ϱ�

        //������ �������� �� ���� + ������ ���� = ���� �������� ����� ����
        //���� �������� ����� ������ > ���� �ð� : �������� �ָ� �ȵ���.
        if(preDamageTime + damageInterval > Time.time)
        {
            return;
        }


        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().TakeDamage(damage);
            var paticle = Instantiate(impactParticle, collision.GetContact(0).point, Quaternion.identity);
            paticle.Play();
            Destroy(paticle.gameObject, 2f);
            preDamageTime = Time.time;
        }
    }

    //public void Contact()
    //{
    //    print("���� �ε���.");
    //    GameManager.Instance.player.TakeDamage(damage);
    //}
}
