//1.
//- 공격, 방어, 움직임 등등 인터페이스를 만들고
//- 특정 캐릭터(3개)가 상속을 받는다.
//ㄴ 각각의 캐릭터들이 행동을 수행하는 결과를 출력한다.
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using static System.Net.Mime.MediaTypeNames;

namespace exam1
{
    internal class Program
    {
        public interface IAttack
        {
            void Attack();
        }

        public interface IMove
        {
            void Move();
        }

        public interface IDefence
        {
            void Defence();
        }


        public class player : IAttack, IMove, IDefence
        {
            private string name;
            private int hp;
            private int damage;
            private int defense;
            public player()
            {
                name = "전사";
                hp = 100;
                damage = 5;
                defense = 2;
            }
            public void Attack()
            {
                Console.WriteLine($"{name}이 공격 합니다");
                Console.WriteLine($"{damage} 만큼 공격 합니다");
            }
            public void Move()
            {
                Console.WriteLine($"{name}이 움직입니다");
            }

            public void Defence()
            {
                
                Console.WriteLine($"{name}이 방어 합니다");
                Console.WriteLine($"{defense}만큼 방어 합니다");
            }
           
        }

        public class monster : IAttack, IMove, IDefence
        {
            private string name;
            private int hp;
            private int attack;
            private int defense;
            public monster()
            {
                name = "시그너스";
                hp = 100;
                attack = 8;
                defense = 8;
            }
            public void Attack()
            {
                Console.WriteLine($"{name}이 공격 합니다");
                Console.WriteLine($"{attack} 만큼 공격 합니다");
            }
            public void Move()
            {
                Console.WriteLine($"{name}이 움직입니다");
            }

            public void Defence()
            {

                Console.WriteLine($"{name}이 방어 합니다");
                Console.WriteLine($"{defense}만큼 방어 합니다");
            }

        }


        static void Main(string[] args)
        {
            player p = new player();
            monster m = new monster();
            m.Move();
            p.Move();
            m.Attack();
            p.Defence();
            p.Attack(); 
            m.Defence();
        }
    }
}
