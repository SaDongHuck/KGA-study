using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int level = 0; //레벨
    public int exp = 0; //경험치

    //현업에서 개발되는 대부분의 게임은 레벨업시 exp값을 빼지 않음.
    //계속 exp를 누적하는 대신, 현재 exp를 레벨로 환산하면 레벨이 몇이 되는지 계산
    //레벨을 가지고 foreach문을 도는 알고리즘 구현?

    private int[] levelupSteps = { 100, 200, 300, 400 }; //최대 레벨 5까지의 경험치 단계
    private int currentMaxExp; //현재 레벨에서 다음 레벨까지 필요한 경험치량

    private float maxHp;
    public float hp = 100f; //체력
    public float damage = 5f; //공격력
    public float moveSpeed = 5f; //이동속도

    //public Projectile projectilePrefab; //투사체 프리팹

    public float HpAmount { get => hp / maxHp; } //현재 체력 비율

    public int killCount = 0;
    public int totalkillCount = 0; //이전 게임에서의 누적 카운트

    //public Text killCountText;
    //public Image hpBarImage;
   // public Text levelText;
    //public Text expText;

    private Transform moveDir;
    private Transform fireDir;

    private Rigidbody2D rb;

    public Animator tailfireAnimCtrl;

    //플레이어가 Fire 기능을 사용하는 대신
    //Skill등을 관리하여 공격기능을 수행하도록

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
        //리턴이 있는 함수를 호출할 때, 리턴을 사용하지 않는다면.
        //아예 반환을 위한 메모리를 점유하지 않고 함수만 호출
       // _ = StartCoroutine(FireCoroutine());

        foreach (Skill skill in skills)
        {
            GameObject skillobj = Instantiate(skill.skillprefabs[skill.skillLevel], transform, false);
            skillobj.name = skill.skillName; //오브젝트 이름 변경
            skillobj.transform.localPosition = Vector3.zero; //스킬 위치를 플레이어의 위치로 가져옴
            
            if(skill.isTargeting)
            {
                skillobj.transform.SetParent(fireDir); //항상
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

        //마우스 위치로 사격 방향을 향해야 할때
        //Vector3 -> Vector2로 캐스팅 할 때 : z값이 생략
        //Vector2 mousePos = Input.mousePosition;
        //Vector2 mouseScreenPos = Camera.main.ScreenToWorldPoint(mousePos);
        //Vector2 fireDir = mouseScreenPos - (Vector2)transform.position;

        //가장 가까운 적을 탐색하여 사격 방향을 정할때

        Enemy targetEnemey = null; //대상으로 지정된 적
        float targetDistance = float.MaxValue; //대상과의 거리

        //게임매니저의 적 리스트의 길이가 0이면 사격 중지
        if (GameManager.Instance.enemies.Count <= 0) isFiring = false;
        else isFiring = true;

        //foreach문으로 리스트를 순회함
        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            //Distance : 두 오브젝트간의 거리를 구해주는 메서드
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < targetDistance) //이전에 비교한 적보다 가까우면
            {
                //가장 가까운 거리를 구함
                targetDistance = distance;
                //가장 가까운 거리에 위치한 적을 타겟으로 지정
                targetEnemey = enemy;
            }
        }
        //사격 방향 = ze
        Vector2 fireDir = Vector2.zero;
        if (targetEnemey != null) //타겟이 있다면
            fireDir = targetEnemey.transform.position - transform.position;

        Move(moveDir);

        //마우스 좌클릭 또는 왼쪽 ctrl키로 발사
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    Fire();
        //}

        UIManager.Instance.killcountText.text = killCount.ToString();
        UIManager.Instance.TotlaKillCountText.text = killCount.ToString();
        UIManager.Instance.hpBar.fillAmount = HpAmount;

        //transform.up/right/forward 에 방향 벡터를 대입할 때는 방향벡터의 magnitude를 굳이 1로 제한 하지 않아도 됨. 
        if (moveDir.magnitude > 0.1f)
        {
            this.moveDir.up = moveDir;
        }
        this.fireDir.up = fireDir;

        // print(this.moveDir.up); //normalized 되어 magnitude가 1로 고정된 방향 벡터가 반환됨
    }

    /// <summary>
    /// Transform을 통해 게임 오브젝트를 움직이는 함수.
    /// </summary>
    /// <param name="dir">이동 방향</param>
    public void Move(Vector2 dir)
    {
        //transform.Translate(dir * moveSpeed * Time.deltaTime);
        Vector2 movePos = rb.position + (dir * moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(movePos);
    }

    /// <summary>
    /// 투사체를 발사.
    /// </summary>
   /* public void Fire()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        projectile.transform.up = fireDir.up;
        projectile.damage = damage;

    }*/

    public float fireInterval;
    public bool isFiring;

    //자동으로 투사체를 발사하는 코루트
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
        if (damage < 0) //대신 힐 하도록 처리
        {
            TakeHeal(-damage);
            //혹은 데미지 0처리
            damage = 0;
        }

        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            //게임오버 처리
        }
    }

    //경험치 습득시마다 호출
    public void GainEXP(int exp)
    {
        this.exp += exp; //습득한 경험치를 더해줌
        if (this.exp >= currentMaxExp && this.level < levelupSteps.Length) //경험치 습득 후, 목표 경험치량 이상이면
        {//최대 레벨에 도달하지 않았다면 
            //레벨업
            OnLevelUP();
            //레벨을 0부터 시작한 이유 
            //레벨업 하면 레벨업 이펙트도 넣어줘야 하고,
            //UI도 띄워줘야 되고,
            //레벨업 결과 얻게된 스킬도 올려줘야 되고
            //DoLevelUp();
        }
        //경험치를 얻을때마다 경험치 UI를 갱신
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
        //아이템이랑 상호작용 할건데....
        //아이템이 bomb도 있고 heal도 있어서
        //전부 각 객체별로 행동을 정의했더니
        //소스코드도 길어지고... 이건 좀 아닌거 같고...

        //if (other.TryGetComponent<Bomb>(out Bomb bomb))
        //{
        //    //만약 상호작용한 트리거에 Bomb 컴포넌트가 있을 경우
        //    bomb.Contact();
        //}

        //if(other.TryGetComponent<Heal>(out Heal heal))
        //{
        //    heal.Contact();
        //}

        //이럴떄 개발자가 "다형성"을 구현하여
        //소스코드를 효율적으로 작성할 수 있는 방법 3가지.

        //1. 부모 클래스를 상속
        //2. 인터페이스를 구현
        //3. 유니티의 SendMessage 사용

        //1. 부모 클래스를 상속했을 경우
        //if (other.TryGetComponent<Item>(out Item item))
        //{
        //    item.Contact(); 
        //    //부딛힌 객체가 정확히 어떤 타입일지는 모르겠으나
        //    //Item이라는 클래스를 상속한것은 확실하고
        //    //그렇다면 Contact() 함수를 가지고 있으므로 호출할 수 있다.
        //}

        //2. 만약 특정 클래스를 상속하지 않고, 공통점이 없는 여러 객체들이 경우에 따라
        //같은 행동을 해야 할경우. Interface를 사용할 수 있음.

        //if (other.TryGetComponent<IContactable>(out var contact))
        //{
        //    contact.Contact();
        //    //부딪힌 객체가 Enemy인지 Item인지조차 모르겠으나
        //    //어쨌든 IContactable 인터페이스를 구현했다면
        //    //Contact() 함수를 가지고 있을 것이므로 호출할 수 있다.
        //}

        //3. 게임오브젝트는 모두 SendMessage를 가지고 있는 컴포넌트의 특정 이름을 가진 함수를
        //호출하도록 하는 기능을 지원함. Unity Engine의 내장 기능

        other.SendMessage("Contact", SendMessageOptions.DontRequireReceiver);

        //SendMessage를 사용할때의 주의점
        //1. 문자열로 함수를 호출하므로 함수 이름 변경 또는 오타 발생 시 트러블 슈팅이 힘들다.
        //2. 해당 객체에 있는 모든 컴포넌트들이 Contact라는 함수를 가지고 있는지 탐색을 수행하기 때문에
        //  퍼포먼스가 효율적이라고 보기 힘들다.
        //3. 호출할 함수의 파라미터는 0개 또는 1개로 제한됨.
        //빠른 개발과 프로토타이핑에서 쓰기는 좋으나, 구조적으로 좋은 방식은 아니므로 팀원이 많은 개발
        //  팀이나 일정 규모 이상의 기업에서는 쓰지 않는편.
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

    //파라미터로 넘어온 스킬의 레벨을 상승 시키고, 다음레벨의 프리렙으로 교체
    public void OnSkillLevelUP(Skill skill)
    {
        if(skill.skillLevel >= skill.skillprefabs.Length - 1)
        {
            Debug.LogWarning($"최대 레벨에 도달한 스킬 레벵럽을 시도 함{skill.skillName}");
            return;
        }

        skill.skillLevel++; //스킬레벨 상승

        Destroy(skill.currentSkillObject); //기존에 있던 스킬 오브젝트를 제거
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
플레이어 스킬 : 
1. 투사체 히히발싸
    대미지 계수, 투사체 크기, 투사체 속도, 공격 속도, 투사체 개수, 투사체 관통 횟수
2. 랜덤 위치에서폭발하는 범위형 수킬
    데미지 계수, 투사체 크기, 개수, 속도, 공격 간격
3. 플레이어 주위들 돌면서 접촉한 적에게 대미지를 주는 스킬
    밀어내는 기능도 있으면 좋을거같아요

 
 
 
 
 
 
 
 
 
 
 
 
 */