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

    //������ ��ġ���� ���� �ð� �Ŀ� ���� �������� ������ ������� �ְ� �����
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
            yield return null; //�����Ӵ� 1ȸ�� �ݺ�

            float currentTime = Time.time - startTime; //�� ������Ʈ�� ������ ���� ���ӵ� �ð�
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