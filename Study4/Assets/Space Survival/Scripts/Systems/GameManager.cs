using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ��ü ������ �Ѱ��ϴ� ������Ʈ.
public class GameManager : MonoBehaviour
{
    //���� ��ü�� �ϳ��� �����ؾ� �Ѵ�.
    private static GameManager instance;
    public static GameManager Instance => instance;

    internal List<Enemy> enemies = new List<Enemy>(); //���� �����ϴ� ��ü �� List
    internal Player player; //���� �����ϴ� �÷��̾� ��ü

    //����Ƽ���� �̱��� ������ �����ϴ� ���
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { DestroyImmediate(this); return; }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //MyClass myClass = MyClass.GetMyClass(); //�⺻ �����ڰ� private�̹Ƿ� GetMyClass�θ� �ν��Ͻ��� ������ �� ����.
        //���� MyClass�� �ʿ� �������� null�� �����ϴ� �� ������ ������
        //GC�� ���� ��ü�� �����.
        //var a = new GameManager();
    }

    //��ź �������� ȣ���Ͽ� ��� ���� ����(Enemy.Die())
    public void RemoveAllEnemies()
    {
        List<Enemy> removeTargets = new List<Enemy>(enemies); //enemies ����Ʈ�� ����

        foreach (Enemy removeTarget in removeTargets)
        {
            removeTarget.Die();
        }
    }
}

//�������� C#�� �̱��� ��ü ����
public class DefaultSingleton
{
    //���� ���μ��� ���� ������ ���� å���� �� �ν��Ͻ��� ������ ����
    private static DefaultSingleton instance;

    //�ܺο��� �����ڸ� ȣ���� �� ������ �⺻ ������ ������ ���´�.
    private DefaultSingleton() {}

    //�ܺϿ����� ���� ������ �ν��Ͻ��� �����Ͽ� ���� ������ �� �� ����(�ٸ� ������ ������ �Ұ�)
    public static DefaultSingleton Instance 
    { 
        get 
        {
            if (instance == null) instance = new DefaultSingleton();
            return instance; 
        } 
    }
}

//�⺻���� ��ü������ ���� �̱��� ��ü�� ����� ���
public class MyClass
{
    private static MyClass nonCollectableMyClass; //������ ������ �ȵǴ� myclass �ν��Ͻ��� ����.

    private MyClass() {}

    public int processCount; //��������(non-static)

    public static MyClass GetMyClass()
    {
        if (nonCollectableMyClass == null) { //GetMyclass()�� ���� ȣ�� ���� ��쿡�� true
            nonCollectableMyClass = new MyClass();
            return nonCollectableMyClass;
        }
        else 
            return nonCollectableMyClass;
    }
}