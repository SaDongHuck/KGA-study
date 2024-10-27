using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;


//유니티에서 Instantiate -> Destroy가 빈번하게 호출 될 경우
//같은 빈도로 메모리의 할당/해제가 일어나게되므로 GC 또한 빈번하게 호출됨
//퍼포먼스 및 효율적이지 못한 연산을 수행하게 됨

//그래서 나온 해결방안 : 오브젝트 풀링
//빈번하게 생성 및 삭제되는 객체는 Instantiate -> Destroy 대신
//활성화, 비활성화를 통해 재사용함으로써 GC의 리소스 점유율을 줄일 수 있음
//프로세스가 점유하는 메모리 또한 감소하는 효과가 있음

//주의점
//1. 객체의 생성 및 삭제가 일어나지 않으므로 재사용을 위해 초기화 되어야 할 변수 등을
//   빠짐 없이 관리해야 할 필요가 있음 (예시-> Start, OnDestroy 메시지 함수의 의미가 없어짐)
//2. 빈번하게 생성 및 삭제가 일어나지 않거나, 크기가 큰 복잡한 객체는 
//   오히려 메모리 점유율을 늘릴 수 있음
//   (예시 -> 보스 몬스터, 괜히 비활성화해도 메모리 먹는건 똑같으니 아예 날리는게 더 좋음)

//무지성 오브젝트 풀링 보다는 실제 사용시 얼마나 퍼포먼스에 효과적인지 계산해보고 사용 할 필요가 있음 

public class ProjectilePool : MonoBehaviour
{
    //public 메서드는 2개

    public static ProjectilePool pool;
    public Projectile projPrefab;

    private void Awake()
    {
        pool = this;
    }

    List<Projectile> poolList = new(); //비활성화 된 객체 리스트

    /// <summary>
    /// 투사체 꺼내오기
    /// </summary>
    /// <returns>꺼내온 투사체</returns>
    public Projectile Pop()
    {
        if (poolList.Count <= 0) //꺼낼 객체가 없으면?
            Push(Instantiate(projPrefab)); //새로 객체를 하나 생성후 리스트에 넣어줌

        Projectile proj = poolList[0];

        poolList.Remove(proj);

        proj.gameObject.SetActive(true);
        proj.transform.SetParent(null);

        return proj;
    }

    /// <summary>
    /// 다 쓴 투사체 도로 집어넣기
    /// </summary>
    /// <param name="proj">다 쓴 투사체</param>
    public void Push(Projectile proj)
    {
        poolList.Add(proj);
        proj.gameObject.SetActive(false);
        proj.transform.SetParent(transform, false);
    }
    /// <summary>
    /// 다 쓴 투사체 잠시 후에 집어넣기
    /// </summary>
    /// <param name="proj"></param>
    /// <param name="delay">지연시간</param>
    public void Push(Projectile proj, float delay) //Destroy를 대체할 Push 오버로드
    {
        StartCoroutine(PushCoroutine(proj, delay));
    }

    IEnumerator PushCoroutine(Projectile proj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Push(proj);
    }
}
