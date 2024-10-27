using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;


//����Ƽ���� Instantiate -> Destroy�� ����ϰ� ȣ�� �� ���
//���� �󵵷� �޸��� �Ҵ�/������ �Ͼ�ԵǹǷ� GC ���� ����ϰ� ȣ���
//�����ս� �� ȿ�������� ���� ������ �����ϰ� ��

//�׷��� ���� �ذ��� : ������Ʈ Ǯ��
//����ϰ� ���� �� �����Ǵ� ��ü�� Instantiate -> Destroy ���
//Ȱ��ȭ, ��Ȱ��ȭ�� ���� ���������ν� GC�� ���ҽ� �������� ���� �� ����
//���μ����� �����ϴ� �޸� ���� �����ϴ� ȿ���� ����

//������
//1. ��ü�� ���� �� ������ �Ͼ�� �����Ƿ� ������ ���� �ʱ�ȭ �Ǿ�� �� ���� ����
//   ���� ���� �����ؾ� �� �ʿ䰡 ���� (����-> Start, OnDestroy �޽��� �Լ��� �ǹ̰� ������)
//2. ����ϰ� ���� �� ������ �Ͼ�� �ʰų�, ũ�Ⱑ ū ������ ��ü�� 
//   ������ �޸� �������� �ø� �� ����
//   (���� -> ���� ����, ���� ��Ȱ��ȭ�ص� �޸� �Դ°� �Ȱ����� �ƿ� �����°� �� ����)

//������ ������Ʈ Ǯ�� ���ٴ� ���� ���� �󸶳� �����ս��� ȿ�������� ����غ��� ��� �� �ʿ䰡 ���� 

public class ProjectilePool : MonoBehaviour
{
    //public �޼���� 2��

    public static ProjectilePool pool;
    public Projectile projPrefab;

    private void Awake()
    {
        pool = this;
    }

    List<Projectile> poolList = new(); //��Ȱ��ȭ �� ��ü ����Ʈ

    /// <summary>
    /// ����ü ��������
    /// </summary>
    /// <returns>������ ����ü</returns>
    public Projectile Pop()
    {
        if (poolList.Count <= 0) //���� ��ü�� ������?
            Push(Instantiate(projPrefab)); //���� ��ü�� �ϳ� ������ ����Ʈ�� �־���

        Projectile proj = poolList[0];

        poolList.Remove(proj);

        proj.gameObject.SetActive(true);
        proj.transform.SetParent(null);

        return proj;
    }

    /// <summary>
    /// �� �� ����ü ���� ����ֱ�
    /// </summary>
    /// <param name="proj">�� �� ����ü</param>
    public void Push(Projectile proj)
    {
        poolList.Add(proj);
        proj.gameObject.SetActive(false);
        proj.transform.SetParent(transform, false);
    }
    /// <summary>
    /// �� �� ����ü ��� �Ŀ� ����ֱ�
    /// </summary>
    /// <param name="proj"></param>
    /// <param name="delay">�����ð�</param>
    public void Push(Projectile proj, float delay) //Destroy�� ��ü�� Push �����ε�
    {
        StartCoroutine(PushCoroutine(proj, delay));
    }

    IEnumerator PushCoroutine(Projectile proj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Push(proj);
    }
}
