using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTrst : MonoBehaviour
{
    //Resources ���� : ������Ʈ Assets���� ���� Resource��� �̸��� ������ ������ ���
    //�ش� ���� ���� ���� �� ����Ƽ ���ҽ��� Ȱ�� ������ (Spirte, Trxture, Mesh, Prefab ��) 
    //������ �̸� �޸𸮿� �ε��Ͽ� ��Ÿ�ӿ��� ������ �� �ֵ��� �ϴ� ����
    //���� : �̸� ������ �ش� ���ҽ��� ���ε� ���� �ʾƵ� ��Ÿ�ӿ��� �ε尡 ������, �ε� �ӵ��� ����
    //���� : ��쿡 ���� ���ʿ��� ���ҽ��� �޸𸮸� ������ �� ������, �����ڰ� ���� �����ϱⰡ �����
    //������ ���ϵ��� �ε��Ͽ� Ȱ���� �� �����Ƿ�, ���� ������Ʈ��, ������ Ÿ���ο� �ַ� ���̸�
    //���̺꼭�� ���ӿ����� �����ϴ� ��.
    //��ü ���� �ý��� : Asset Bundle <- ���� ����� ���ӿ� ���� Ȱ��Ǵ� ��� 
    //  Addressablee Assets <- �ֱٿ� ������ �����, ���۷����� �����ʰ�, �Ű����� ������ ���Ƽ� ���� �������� ���� ����

    //Resousces ������ �����ϴ� ��� : ��� �����ϰ� Assets���� ���� ������ �� ������
    //���� ������ "/"�� �����Ͽ� ��θ� ����

    //���� : Assets/Texture/Resources/PlayerSprites/Player1.png
    //Recources.Load("PlayerSprites/Player1");

    public SpriteRenderer sp1;
    public SpriteRenderer sp2;
    public Texture texture;
    private void Start()
    {
        Sprite sprite1 = Resources.Load<Sprite>("spritE1"); //��ҹ��� Ʋ�ȴµ� �� �ǳ���
        Sprite sprite2 = Resources.Load<Sprite>("spritE2");

        texture = Resources.Load<Texture>("sprite1");

        Enemy enemyResource = Resources.Load<Enemy>("Prefabs/EnemyResource");

        sp1.sprite = sprite1;
        sp2.sprite = sprite2;

        Instantiate(enemyResource);

    }
}
