using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class JsonTest : MonoBehaviour
{
    public EnemyDataSO testData;
    public EnemyData loadedData;


    private void Start()
    {
        //FromJson : Json�����͸� ��ü�� ��ȯ
        //ToJason : ��ü�� Json�����ͷ� ��ȯ
        string json = JsonUtility.ToJson(testData);
        
        //���� ���� ������ȭ �� ���ڿ��� ���� Ȯ���� �� ����
        //��ü�� �Էµ� ���� ��� string���� ��ȯ �ǹǷ�, �а� ���� ������ ȿ�������� ����
        Debug.Log(json);

        string path = $"{Application.streamingAssetsPath}/{testData.name}.json";

        File.WriteAllText(path, json);
    }

    public void Save()
    {
        //FromJson : Json�����͸� ��ü�� ��ȯ (Serialize, ����ȭ)
        //ToJason : ��ü�� Json�����ͷ� ��ȯ 
        string json = JsonUtility.ToJson(testData);

        //���� ���� ������ȭ �� ���ڿ��� ���� Ȯ���� �� ����
        //��ü�� �Էµ� ���� ��� string���� ��ȯ �ǹǷ�, �а� ���� ������ ȿ�������� ����
        //Debug.Log(json);
        //StreamingAssets ���� : ����� ���� ������ �״�� ����Ǿ� ���� ���Ͽ� ���ԵǾ�� �� ���ϵ��� �־���� ����
        //������ �״�� �����ǰ� �״�� �ε�ǹǷ� ���� �Ŀ��� ���� ������ �� ����
        //�÷��̾ ���� ���� ������ �� �ִٴ����� �������� ����
        string path = $"{Application.streamingAssetsPath}/{testData.name}.json";

        json = JsonConvert.SerializeObject(json);

        File.WriteAllText(path, json);
    }

    public void Load()
    {
        string path = $"{Application.streamingAssetsPath}/{testData.name}.json";
        string json = File.ReadAllText(path);

        //json �����͸� ��ü�� ��ȯ�� �� (Deserialize, ������ȭ)
        loadedData = JsonUtility.FromJson<EnemyData>(json);
        loadedData = JsonConvert.DeserializeObject<EnemyData>(json);
        //jsonUtility : C#���� ����ϴ� ���ͷ� ������ Ÿ���� ��κ� ����ȭ�� �����ϳ�,
        //�迭, ����Ʈ ���� �ݷ���Ÿ���� ����ȭ�� �Ұ�����
        Debug.Log(loadedData.enemyName);
        Debug.Log(loadedData.level);
        Debug.Log(loadedData.hp);
    }
}

//Json�� ���� ����ȭ �� ������ȭ �� ��ü
[Serializable]
public class EnemyData
{
    public string enemyName;
    public int level;
    public float hp;
    public float damage;
    public float moveSpeed;
}