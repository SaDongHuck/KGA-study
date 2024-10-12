using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Globalization;
//2.딕셔너리 할용
//캐릭터 클래스 생성
//딕셔너리로 관리
//캐릭터 추가, 삭제, 검색


namespace ConsoleApp1
{
    internal class Program
    {
        public abstract class Item
        {
            public string Name { get; set; }
            public int ID { get; set; }
            public int Price { get; set; }
            protected Item(string name, int id, int price)
            {
                Name = name;
                ID = id;
                Price = price;
            }

            public override string ToString()
            {
                return $"{Name} (ID: {ID}, Price: {Price})";
            }

        }

        public class Helmet : Item
        {
            public Helmet(string name, int id, int price) 
                : base(name, id, price) { }
        }

        public class Dart : Item
        {
            public Dart(string name, int id, int price)
                : base(name, id, price) { }
        }

        public class Arrow : Item
        {
            public Arrow(string name, int id, int price)
                : base(name, id, price) { }
        }

        public class Ring : Item
        {
            public Ring(string name, int id, int price)
                : base(name, id, price) { }
        }

        static void Main(string[] args)
        {
          Dictionary<int, Item> items = new Dictionary<int, Item>();
            Helmet h = new Helmet("헬멧", 1, 2000);
            Dart d = new Dart("표창", 2, 3000);
            Arrow a = new Arrow("화살", 3, 4000);
            Ring r = new Ring("반지", 4, 5000);

            //아이템 추가
            items.Add(1, h);
            items.Add(2, d);
            items.Add(3, a);
            items.Add(4, r);
            Console.WriteLine("현재 아이템 목록:");
            foreach (var item in items)
            {
                Console.WriteLine(item.Value);
            }

            //아이템 삭제
            items.Remove(2);
            Console.WriteLine("삭제후 아이템 목록:");
            foreach (var item in items)
            {
                Console.WriteLine(item.Value);
            }

            //아이템 검색
            int searchId = 1;
            if (items.TryGetValue(searchId, out Item value))
            {
                Console.WriteLine($"찾은 아이템: {value}");
            }
            else
            {
                Console.WriteLine("아이템을 찾을 수 없습니다.");
            }


        }
    }
}
