//1.리스트 할용
//캐릭터 클래스 생성
//리스트로 관리
//캐릭터 추가, 삭제, 검색
namespace _2024_10_11_과제
{
    internal class Program
    {
        public abstract class Character
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public int Attack { get; set; }

            protected Character(string name, int health, int attack)
            {
                Name = name;
                Health = health;
                Attack = attack;
            }
        }

        public class thief : Character
        {
            public thief(string name, int health, int attack)
                : base(name, health, attack) { }
        }

        public class archer : Character
        {
            public archer(string name, int health, int attack)
                : base(name, health, attack) { }
        }


        static void Main(string[] args)
        {
           List<Character> player = new List<Character>();
           thief t = new thief("전사",100,20);
           archer a = new archer("궁수", 100, 20);
           player.Add(t);
           player.Add(a);
            //추가
           foreach (Character ch in player)
           {
                Console.WriteLine("추가 후");
                Console.WriteLine($"캐릭터 이름 :{ch.Name}, 캐릭터 채력 :{ch.Health}, 캐릭터 공격력:{ch.Attack}");
           }
           //삭제
           player.Remove(t);
           foreach (Character ch in player)
           {
                Console.WriteLine("제거 후");
                Console.WriteLine($"캐릭터 이름 :{ch.Name}, 캐릭터 채력 :{ch.Health}, 캐릭터 공격력:{ch.Attack}");
           }

            //검색
            string seachname = "궁수";
            Character foundCharacter = player.FirstOrDefault(ch => ch.Name == seachname);

            if (foundCharacter != null)
            {
                Console.WriteLine($"검색 결과: 캐릭터 이름 :{foundCharacter.Name}, 캐릭터 채력 :{foundCharacter.Health}, 캐릭터 공격력:{foundCharacter.Attack}");
            }
            else
            {
                Console.WriteLine("캐릭터를 찾을 수 없습니다.");
            }
        }
    }
}
