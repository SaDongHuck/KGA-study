using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTrst : MonoBehaviour
{
    //Resources 폴더 : 프로젝트 Assets폴더 내에 Resource라는 이름의 폴더를 생성할 경우
    //해당 폴더 내의 파일 중 유니티 리소스를 활용 가능한 (Spirte, Trxture, Mesh, Prefab 등) 
    //파일을 미리 메모리에 로드하여 런타임에서 생성할 수 있도록 하는 폴더
    //장점 : 미리 씬에서 해당 리소스를 바인딩 하지 않아도 런타임에서 로드가 가능함, 로드 속도가 빠름
    //단점 : 경우에 따라 불필요한 리소스가 메모리를 점유할 수 있으며, 개발자가 직접 제어하기가 어려움
    //빠르게 파일들을 로드하여 활용할 수 있으므로, 작은 프로젝트나, 프로토 타이핑에 주로 쓰이며
    //라이브서비스 게임에서는 기피하는 편.
    //대체 파일 시스템 : Asset Bundle <- 예전 모바일 게임에 많이 활용되던 방식 
    //  Addressablee Assets <- 최근에 등장한 기술로, 레퍼런스가 많지않고, 신경써야할 설정이 많아서 아직 점유율이 높지 않음

    //Resousces 폴더를 생성하는 방법 : 경로 무관하게 Assets폴더 어디든 생성할 수 있으며
    //하위 폴더를 "/"로 구분하여 경로를 참조

    //예시 : Assets/Texture/Resources/PlayerSprites/Player1.png
    //Recources.Load("PlayerSprites/Player1");

    public SpriteRenderer sp1;
    public SpriteRenderer sp2;
    public Texture texture;
    private void Start()
    {
        Sprite sprite1 = Resources.Load<Sprite>("spritE1"); //대소문자 틀렸는데 왜 되나요
        Sprite sprite2 = Resources.Load<Sprite>("spritE2");

        texture = Resources.Load<Texture>("sprite1");

        Enemy enemyResource = Resources.Load<Enemy>("Prefabs/EnemyResource");

        sp1.sprite = sprite1;
        sp2.sprite = sprite2;

        Instantiate(enemyResource);

    }
}
