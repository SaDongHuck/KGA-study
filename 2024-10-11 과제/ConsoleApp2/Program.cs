using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

//캐릭터+아이템 장착 해제 시스템 구현

namespace ConsoleApp2
{
    internal class Program
    {
        public abstract class Character
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public int Attack { get; set; }
            public Item wear { get; set; }

            protected Character(string name, int health, int attack)
            {
                Name = name;
                Health = health;
                Attack = attack;
            }
            public void Equiment(Item item)
            { 
                wear = item;
                Health += item.HealthBonus;
                Attack += item.AttackBonus;
                Console.WriteLine($"{Name}은 {item.Name}을 착용 중 입니다 공격력은 {Attack} 입니다" +
                    $" 채력은 {Health}");
            }
            public void NEquiment(Item item)
            {
                if (wear == null)
                {
                    Console.WriteLine($"{Name}은 아이템 미착용");
                    return;
                }
                wear = item;
                Health -= item.HealthBonus;
                Attack -= item.AttackBonus;
                Console.WriteLine($"{Name}은 {item.Name}을 장착을 해제 했습니다 공격력은 {Attack} 입니다" +
                    $" 채력은 {Health}");
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

        public class Item
        {
            public string Name { get; set; }
            public int HealthBonus {  get; set; }
            public int AttackBonus { get; set; }

            public Item(String name, int healthBonus, int attackBonus)
            {
                Name = name;
                HealthBonus = healthBonus;
                AttackBonus = attackBonus;
            }

        }

        

        
        static void Main(string[] args)
        {
            List<Character> player = new List<Character>();

            thief t = new thief("전사", 100, 20);
            archer a = new archer("궁수", 100, 20);
            player.Add(t);
            player.Add(a);
            foreach (Character ch in player)
            {
                Console.WriteLine($"캐릭터 이름 :{ch.Name}, 캐릭터 채력 :{ch.Health}, 캐릭터 공격력:{ch.Attack}");
            }
            Item Hat = new Item("모자", 0, 10);
            Item Ring = new Item("반지", 0, 10);
            Item white_potion = new Item("하얀포션", 10, 0);
            string schoice;
            int choice;
            while (true)
            {
                Console.WriteLine("아이템 창작 해제 골라라");
                Console.WriteLine("1.아이템 창작");
                Console.WriteLine("2.아이템 해제");
                schoice = Console.ReadLine();
                choice = Convert.ToInt32(schoice);
                switch(choice)
                {
                    case 1:
                        Console.WriteLine("착용할 아이템을 골라라");
                        Console.WriteLine("1.모자");
                        Console.WriteLine("2.반지");
                        Console.WriteLine("3.하얀포션");
                        schoice = Console.ReadLine();
                        choice = Convert.ToInt32(schoice);
                        switch(choice)
                        {
                            case 1:
                                t.Equiment(Hat);
                                a.Equiment(Hat);
                                continue;
                            case 2:
                                t.Equiment(Ring);
                                a.Equiment(Ring);
                                continue;
                            case 3:
                                t.Equiment(white_potion);
                                a.Equiment(white_potion);   
                                continue;
                            default:
                                Console.WriteLine("다시 입력");
                                break;
                        }
                        break;
                    case 2:
                        Console.WriteLine("해제할 아이템을 골라라");
                        Console.WriteLine("1.모자");
                        Console.WriteLine("2.반지");
                        Console.WriteLine("3.하얀포션");
                        schoice = Console.ReadLine();
                        choice = Convert.ToInt32(schoice);
                        switch (choice)
                        {
                            case 1:
                                t.NEquiment(Hat);
                                a.NEquiment(Hat);
                                continue;
                            case 2:
                                t.NEquiment(Ring);
                                a.NEquiment(Ring);
                                continue;
                            case 3:
                                t.NEquiment(white_potion);
                                a.NEquiment(white_potion);
                                continue;
                            default:
                                Console.WriteLine("다시 입력");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("다시 입력");
                        break;
                }
            }

        }
    }
}
