//2.클래스를 활용하여 대전게임을 만들어라.(프로퍼티 활용)
using System;
using System.Threading;
using System.Diagnostics;
namespace exam2
{
    internal class Program
    {
        public abstract class Character
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public int Attack { get; set; }
           

            protected Character(string name, int health, int attack, int defence)
            {
                Name = name;
                Health = health;
                Attack = attack;
            }

            public virtual void Attack1(Character target)
            {
                Console.WriteLine($"{Name}이 공격 합니다");
                target.Health -= Attack;
                Console.WriteLine($"{Name}은 {Attack} 만큼 공격 했습니다");
            }

            public virtual void status()
            {
                Console.WriteLine($"플레이 이름 : {Name}");
                Console.WriteLine($"플레이 채력 : {Health}");
                Console.WriteLine($"플레이 공격력 : {Attack}");
            }


        }

        public class Thief : Character
        {
            public Thief(string name, int health, int attack, int defence)
                : base(name, health, attack, defence) { }

            public override void Attack1(Character target)
            {
                Console.WriteLine($"{Name}이 공격 합니다");
                target.Health -= Attack;
                Console.WriteLine($"{Name}은 {Attack} 만큼 공격 했습니다");
            }

            public void skill(Character target)
            {
                string schoice;
                int choice;
                Console.WriteLine("스킬을 선택 하시오");
                Console.WriteLine("1.럭키세븐(30)");
                Console.WriteLine("2.트리플 스로우(20)");
                Console.WriteLine("3.슈리켄 첼린지(25)");
                Console.WriteLine("4.써든 레이드(40)");
                schoice = Console.ReadLine();
                choice = Convert.ToInt32(schoice);
                int skillDamage = 0;
                switch (choice)
                {
                    case 1:
                        skillDamage = 7;
                        break;
                    case 2:
                        skillDamage = 8;
                        break;
                    case 3:
                        skillDamage = 6;
                        break;
                    case 4:
                        skillDamage = 9;
                        break;
                }
                Console.WriteLine($"{Name}이 스킬을 사용 합니다");
                target.Health -= skillDamage;
                Console.WriteLine($"{target.Name}의 채력은 {target.Health}입니다");
            }

            public override void status()
            {
                Console.WriteLine($"플레이 이름 : {Name}");
                Console.WriteLine($"플레이 채력 : {Health}");
                Console.WriteLine($"플레이 공격력 : {Attack}");
            }
           
          

        }

        public class Monster : Character
        {
            public int f_attack {  get; set; }
            public Monster(string name, int health, int attack, int defence, int attack2)
                : base(name, health, attack, defence)
            {
                f_attack = attack2;
            }

            public override void Attack1(Character target)
            {
                Console.WriteLine($"{Name}이 공격 합니다");
                target.Health -= Attack;
                Console.WriteLine($"{Name}은 {Attack} 만큼 공격 했습니다");
            }

            public void Attack2(Character target)
            {
                Console.WriteLine($"{Name}이 강력한 공격 합니다");
                target.Health -= f_attack;
                Console.WriteLine($"{target.Name}의 채력은 {target.Health}입니다");
            }

            public override void status()
            {
                Console.WriteLine($"적 이름 : {Name}");
                Console.WriteLine($"적 채력 : {Health}");
                Console.WriteLine($"적 공격력 : {Attack}");
            }


        }

        static void Main(string[] args)
        {
            Thief t = new Thief("전사", 100, 5, 8);
            Monster m = new Monster("시그너스", 100, 6, 5, 9);
            while(true)
            {
                Console.WriteLine("플레이어 순서 입니다");
                t.status();
                m.status();
                Console.WriteLine("1.공격 2.스킬");
                string schoice;
                int choice;
                schoice = Console.ReadLine();
                choice = Convert.ToInt32(schoice);
                switch(choice)
                {
                    case 1:
                        t.Attack1(m);
                        break;
                    case 2:
                        t.skill(m);
                        break;
                    default:
                        Console.WriteLine("잘못 입력 하셨습니다");
                        Console.WriteLine("다시 입력 하세요");
                        continue;
                }
                if(m.Health <= 0)
                {
                    Console.WriteLine("이겼습니다");
                    Console.WriteLine("배틀을 종료 합니다");
                    break;
                }
                Thread.Sleep(3000);
                Console.WriteLine("몬스터 순서 입니다");
                Random rand = new Random();
                int m_input = rand.Next(1, 3);
                switch(m_input)
                {
                    case 1:
                        m.Attack1(t);
                        break;
                    case 2:
                        m.Attack2(t);
                        break;
                    default:
                        Console.WriteLine("잘못 입력 하셨습니다");
                        Console.WriteLine("다시 입력 하세요");
                        continue;
                }
                if (t.Health <= 0)
                {
                    Console.WriteLine("졌습니다");
                    Console.WriteLine("배틀을 종료 합니다");
                    break;
                }
            }
        }
    }
}
