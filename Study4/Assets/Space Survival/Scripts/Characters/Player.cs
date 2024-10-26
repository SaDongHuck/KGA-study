using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int level = 0; //����
    public int exp = 0; //����ġ

    //�������� ���ߵǴ� ��κ��� ������ �������� exp���� ���� ����.
    //��� exp�� �����ϴ� ���, ���� exp�� ������ ȯ���ϸ� ������ ���� �Ǵ��� ���
    //������ ������ foreach���� ���� �˰��� ����?

    private int[] levelupSteps = { 100, 200, 300, 400 }; //�ִ� ���� 5������ ����ġ �ܰ�
    private int currentMaxExp; //���� �������� ���� �������� �ʿ��� ����ġ��

    private float maxHp;
    public float hp = 100f; //ü��
    public float damage = 5f; //���ݷ�
    public float moveSpeed = 5f; //�̵��ӵ�

    //public Projectile projectilePrefab; //����ü ������

    public float HpAmount { get => hp / maxHp; } //���� ü�� ����

    public int killCount = 0;
    public int totalkillCount = 0; //���� ���ӿ����� ���� ī��Ʈ

    //public Text killCountText;
    //public Image hpBarImage;
   // public Text levelText;
    //public Text expText;

    private Transform moveDir;
    private Transform fireDir;

    private Rigidbody2D rb;

    public Animator tailfireAnimCtrl;

    //�÷��̾ Fire ����� ����ϴ� ���
    //Skill���� �����Ͽ� ���ݱ���� �����ϵ���

    public List<Skill> skills;


    private void Awake()
    {
        moveDir = transform.Find("MoveDir");
        fireDir = transform.Find("FireDir");
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        maxHp = hp;
        currentMaxExp = levelupSteps[0];

        GameManager.Instance.player = this;
        UIManager.Instance.levelText.text = (level + 1).ToString();
        UIManager.Instance.expText.text = exp.ToString();
        //������ �ִ� �Լ��� ȣ���� ��, ������ ������� �ʴ´ٸ�.
        //�ƿ� ��ȯ�� ���� �޸𸮸� �������� �ʰ� �Լ��� ȣ��
       // _ = StartCoroutine(FireCoroutine());

        foreach (Skill skill in skills)
        {
            GameObject skillobj = Instantiate(skill.skillprefabs[skill.skillLevel], transform, false);
            skillobj.name = skill.skillName; //������Ʈ �̸� ����
            skillobj.transform.localPosition = Vector3.zero; //��ų ��ġ�� �÷��̾��� ��ġ�� ������
            
            if(skill.isTargeting)
            {
                skillobj.transform.SetParent(fireDir); //�׻�
            }
            skill.currentSkillObject = skillobj;    
        }
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 moveDir = new Vector2(x, y);
        
        //this.moveDir.gameObject.SetActive(moveDir != Vector2.zero);

        tailfireAnimCtrl.SetBool("isMoving", moveDir.magnitude > 0.1f);

        //���콺 ��ġ�� ��� ������ ���ؾ� �Ҷ�
        //Vector3 -> Vector2�� ĳ���� �� �� : z���� ����
        //Vector2 mousePos = Input.mousePosition;
        //Vector2 mouseScreenPos = Camera.main.ScreenToWorldPoint(mousePos);
        //Vector2 fireDir = mouseScreenPos - (Vector2)transform.position;

        //���� ����� ���� Ž���Ͽ� ��� ������ ���Ҷ�

        Enemy targetEnemey = null; //������� ������ ��
        float targetDistance = float.MaxValue; //������ �Ÿ�

        //���ӸŴ����� �� ����Ʈ�� ���̰� 0�̸� ��� ����
        if (GameManager.Instance.enemies.Count <= 0) isFiring = false;
        else isFiring = true;

        //foreach������ ����Ʈ�� ��ȸ��
        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            //Distance : �� ������Ʈ���� �Ÿ��� �����ִ� �޼���
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < targetDistance) //������ ���� ������ ������
            {
                //���� ����� �Ÿ��� ����
                targetDistance = distance;
                //���� ����� �Ÿ��� ��ġ�� ���� Ÿ������ ����
                targetEnemey = enemy;
            }
        }
        //��� ���� = ze
        Vector2 fireDir = Vector2.zero;
        if (targetEnemey != null) //Ÿ���� �ִٸ�
            fireDir = targetEnemey.transform.position - transform.position;

        Move(moveDir);

        //���콺 ��Ŭ�� �Ǵ� ���� ctrlŰ�� �߻�
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    Fire();
        //}

        UIManager.Instance.killcountText.text = killCount.ToString();
        UIManager.Instance.TotlaKillCountText.text = killCount.ToString();
        UIManager.Instance.hpBar.fillAmount = HpAmount;

        //transform.up/right/forward �� ���� ���͸� ������ ���� ���⺤���� magnitude�� ���� 1�� ���� ���� �ʾƵ� ��. 
        if (moveDir.magnitude > 0.1f)
        {
            this.moveDir.up = moveDir;
        }
        this.fireDir.up = fireDir;

        // print(this.moveDir.up); //normalized �Ǿ� magnitude�� 1�� ������ ���� ���Ͱ� ��ȯ��
    }

    /// <summary>
    /// Transform�� ���� ���� ������Ʈ�� �����̴� �Լ�.
    /// </summary>
    /// <param name="dir">�̵� ����</param>
    public void Move(Vector2 dir)
    {
        //transform.Translate(dir * moveSpeed * Time.deltaTime);
        Vector2 movePos = rb.position + (dir * moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(movePos);
    }

    /// <summary>
    /// ����ü�� �߻�.
    /// </summary>
   /* public void Fire()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        projectile.transform.up = fireDir.up;
        projectile.damage = damage;

    }*/

    public float fireInterval;
    public bool isFiring;

    //�ڵ����� ����ü�� �߻��ϴ� �ڷ�Ʈ
    /*private IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireInterval);
            if(isFiring) Fire();
        }
    }*/

    public void TakeDamage(float damage)
    {
        if (damage < 0) //��� �� �ϵ��� ó��
        {
            TakeHeal(-damage);
            //Ȥ�� ������ 0ó��
            damage = 0;
        }

        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            //���ӿ��� ó��
        }
    }

    //����ġ ����ø��� ȣ��
    public void GainEXP(int exp)
    {
        this.exp += exp; //������ ����ġ�� ������
        if (this.exp >= currentMaxExp && this.level < levelupSteps.Length) //����ġ ���� ��, ��ǥ ����ġ�� �̻��̸�
        {//�ִ� ������ �������� �ʾҴٸ� 
            //������
            OnLevelUP();
            //������ 0���� ������ ���� 
            //������ �ϸ� ������ ����Ʈ�� �־���� �ϰ�,
            //UI�� ������ �ǰ�,
            //������ ��� ��Ե� ��ų�� �÷���� �ǰ�
            //DoLevelUp();
        }
        //����ġ�� ���������� ����ġ UI�� ����
        UIManager.Instance.levelText.text = (level + 1).ToString();
        UIManager.Instance.expText.text = this.exp.ToString();
        
    }

    public void TakeHeal(float heal)
    {
        hp += heal;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //�������̶� ��ȣ�ۿ� �Ұǵ�....
        //�������� bomb�� �ְ� heal�� �־
        //���� �� ��ü���� �ൿ�� �����ߴ���
        //�ҽ��ڵ嵵 �������... �̰� �� �ƴѰ� ����...

        //if (other.TryGetComponent<Bomb>(out Bomb bomb))
        //{
        //    //���� ��ȣ�ۿ��� Ʈ���ſ� Bomb ������Ʈ�� ���� ���
        //    bomb.Contact();
        //}

        //if(other.TryGetComponent<Heal>(out Heal heal))
        //{
        //    heal.Contact();
        //}

        //�̷��� �����ڰ� "������"�� �����Ͽ�
        //�ҽ��ڵ带 ȿ�������� �ۼ��� �� �ִ� ��� 3����.

        //1. �θ� Ŭ������ ���
        //2. �������̽��� ����
        //3. ����Ƽ�� SendMessage ���

        //1. �θ� Ŭ������ ������� ���
        //if (other.TryGetComponent<Item>(out Item item))
        //{
        //    item.Contact(); 
        //    //�ε��� ��ü�� ��Ȯ�� � Ÿ�������� �𸣰�����
        //    //Item�̶�� Ŭ������ ����Ѱ��� Ȯ���ϰ�
        //    //�׷��ٸ� Contact() �Լ��� ������ �����Ƿ� ȣ���� �� �ִ�.
        //}

        //2. ���� Ư�� Ŭ������ ������� �ʰ�, �������� ���� ���� ��ü���� ��쿡 ����
        //���� �ൿ�� �ؾ� �Ұ��. Interface�� ����� �� ����.

        //if (other.TryGetComponent<IContactable>(out var contact))
        //{
        //    contact.Contact();
        //    //�ε��� ��ü�� Enemy���� Item�������� �𸣰�����
        //    //��·�� IContactable �������̽��� �����ߴٸ�
        //    //Contact() �Լ��� ������ ���� ���̹Ƿ� ȣ���� �� �ִ�.
        //}

        //3. ���ӿ�����Ʈ�� ��� SendMessage�� ������ �ִ� ������Ʈ�� Ư�� �̸��� ���� �Լ���
        //ȣ���ϵ��� �ϴ� ����� ������. Unity Engine�� ���� ���

        other.SendMessage("Contact", SendMessageOptions.DontRequireReceiver);

        //SendMessage�� ����Ҷ��� ������
        //1. ���ڿ��� �Լ��� ȣ���ϹǷ� �Լ� �̸� ���� �Ǵ� ��Ÿ �߻� �� Ʈ���� ������ �����.
        //2. �ش� ��ü�� �ִ� ��� ������Ʈ���� Contact��� �Լ��� ������ �ִ��� Ž���� �����ϱ� ������
        //  �����ս��� ȿ�����̶�� ���� �����.
        //3. ȣ���� �Լ��� �Ķ���ʹ� 0�� �Ǵ� 1���� ���ѵ�.
        //���� ���߰� ������Ÿ���ο��� ����� ������, ���������� ���� ����� �ƴϹǷ� ������ ���� ����
        //  ���̳� ���� �Ը� �̻��� ��������� ���� �ʴ���.
    }

    public void OnLevelUP()
    {
        level++;
        this.exp -= currentMaxExp;
        if (level < levelupSteps.Length)
        {
            currentMaxExp = levelupSteps[level];
            UIManager.Instance.levelupPanel.LevelUpPanelOpen(skills, OnSkillLevelUP);
      

            //int skillNUm = UnityEngine.Random.Range(0, skills.Count);

            //OnSkillLevelUP(skills[skillNUm]);
        }
    }

    //�Ķ���ͷ� �Ѿ�� ��ų�� ������ ��� ��Ű��, ���������� ���������� ��ü
    public void OnSkillLevelUP(Skill skill)
    {
        if(skill.skillLevel >= skill.skillprefabs.Length - 1)
        {
            Debug.LogWarning($"�ִ� ������ ������ ��ų �������� �õ� ��{skill.skillName}");
            return;
        }

        skill.skillLevel++; //��ų���� ���

        Destroy(skill.currentSkillObject); //������ �ִ� ��ų ������Ʈ�� ����
        skill.currentSkillObject = Instantiate(skill.skillprefabs[skill.skillLevel], transform, false);
        skill.currentSkillObject.name = skill.skillprefabs[skill.skillLevel].name;
        skill.currentSkillObject.transform.localPosition = Vector2.zero;
        if (skill.isTargeting)
        {
            skill.currentSkillObject.transform.SetParent(fireDir);
        }


    }
}
/*
�÷��̾� ��ų : 
1. ����ü �����߽�
    ����� ���, ����ü ũ��, ����ü �ӵ�, ���� �ӵ�, ����ü ����, ����ü ���� Ƚ��
2. ���� ��ġ���������ϴ� ������ ��ų
    ������ ���, ����ü ũ��, ����, �ӵ�, ���� ����
3. �÷��̾� ������ ���鼭 ������ ������ ������� �ִ� ��ų
    �о�� ��ɵ� ������ �����Ű��ƿ�

 
 
 
 
 
 
 
 
 
 
 
 
 */