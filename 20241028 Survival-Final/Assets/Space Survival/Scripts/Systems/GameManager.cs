using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    public void GameOver()
    {
        GameOverSceneCtrl.killCount = player.killCount;
        enemies.Clear();//���� �Ŵ����� DontDestroyOnLoad �����̱� ������ 
        //enemies ����Ʈ�� �� ������ ������ ��
        DataManager.Instance.OnSave();
        SceneManager.LoadScene("GameOverScene");
    }

    public void Restart()
    {
        UIManager.Instance.Restart();
        SceneManager.LoadScene("GameScene");
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