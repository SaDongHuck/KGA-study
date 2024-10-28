using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : MonoBehaviour
{
    public float damage;
    public float moveSpeed;
    public float duration;

    public Vector3 rendererStartPos;
    private CircleCollider2D coll;
    private Transform rendererTransform;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        coll.enabled = false;
        rendererTransform = transform.Find("Renderer");
    }

    //생성된 위치에서 일정 시간 후에 일정 범위내의 적에게 대미지를 주고 사라짐
    private void Start()
    {
        StartCoroutine(Explosion());
        //StartCoroutine(DisableCoroutine());
        Destroy(gameObject, duration + 0.1f);
    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(duration);
    }

    IEnumerator Explosion()
    {
        float startTime = Time.time;
        float endTime = startTime + duration;
        rendererTransform.localPosition = rendererStartPos;

        while (Time.time < endTime)
        {
            yield return null; //프레임당 1회씩 반복

            float currentTime = Time.time - startTime; //이 오브젝트가 생성된 이후 지속된 시간
            float duration = this.duration;
            float t = currentTime / duration;
            Vector2 curRendPos = Vector2.Lerp(rendererStartPos, Vector2.zero, t);
            rendererTransform.localPosition = curRendPos;

        }

        Collider2D[] contactedColls = Physics2D.OverlapCircleAll(transform.position, coll.radius);

        foreach (Collider2D contactedColl in contactedColls)
        {
            //Debug.Log($"Contacted name :  {contactedColl.name}");
            //Debug.Log($"Missiles radius : {coll.radius}");
            if (contactedColl.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}