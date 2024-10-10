//월남뽕
// 1. ◆, ♠, ♥, ♣ 의 문양을 가진 카드가 있다.
// 2.  각각 13장이 있으며 1 = A, 11 = J, 12 = Q, 13 = K 로 표시한다
// 3. 두장의 카드를 확인한다.
// 4. 세번째 카드를 뽑기 전에 배팅을 건다. (최소 금액이 있음)
// 5. 세번째 카드가 두 장의 카드 사이의 숫자면 배팅 금액의 2배를 얻는다
// 6. 두 장의 카드 사이의 숫자가 아니라면 현재 금액에서 배팅금 차감.
using System;
using System.Security.Cryptography.X509Certificates;
namespace exam2
{
    internal class Program
    {
        class Card
        {
            private Random rand = new Random();
            private int[] cards = new int[52];
            private string[] shapes = { "diamond", "spades", "heart", "clubs" };

            public Card()
            {
                shuffle();
            }

            private void shuffle()
            {
                for (int i = 0; i < 52; i++)
                {
                    cards[i] = (i % 13) + 1;
                }
                for (int i = 0; i < 100; i++)
                {
                    int index1 = rand.Next(52);
                    int index2 = rand.Next(52);
                    int temp = cards[index1];
                    cards[index1] = cards[index2];
                    cards[index2] = temp;
                } // 카드를 섞습니다.
            }

            public (int cardNumber, string shape) DrawCard()
            {
                int index = rand.Next(52);
                string shape = shapes[index % 4];
                int cardNumber = cards[index];
                return (cardNumber, shape);
            }
        }

        struct Money
        {
            public int play_money;
            public int batting; // 현재 잔액과 배팅 할 금액 변수를 선언합니다.

            public Money(int amount)
            {
                play_money = amount;
                batting = 0;
            }

            public void win_batt()
            {
                play_money += batting * 2;
                Console.WriteLine("이겼습니다^^");
                Console.WriteLine($"당신은 {batting * 2}원을 땄습니다");
            }

            public void lose_batt()
            {
                play_money -= batting;
                Console.WriteLine("졌습니다ㅠㅠ"); // 수정된 메시지
                Console.WriteLine($"당신은 {batting}원을 잃었습니다");
            }
        }

        static void Main(string[] args)
        {
            Card c = new Card();
            Money playerMoney = new Money(10000);
            Console.WriteLine("==============================================");
            Console.WriteLine("월남뽕 게임을 시작 합니다");
            Console.WriteLine("==============================================");

            while (true)
            {
                Console.WriteLine($"사용자는 {playerMoney.play_money} 원을 가지고 있습니다");

                var (cardNumber1, shape1) = c.DrawCard();
                var (cardNumber2, shape2) = c.DrawCard();

                if (cardNumber1 > cardNumber2)
                {
                    (cardNumber1, cardNumber2) = (cardNumber2, cardNumber1);
                    (shape1, shape2) = (shape2, shape1);
                }

                Console.WriteLine($"첫 번째 카드는 {shape1} / {cardNumber1} 두 번째 카드는 {shape2} / {cardNumber2}");

                Console.WriteLine("얼마를 배팅 하시겠습니까?");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int batting))
                {
                    Console.WriteLine("유효한 숫자를 입력하세요.");
                    continue;
                }

                if (batting > playerMoney.play_money)
                {
                    Console.WriteLine("배팅 금액이 너무 큽니다. 다시 입력 하세요.");
                    continue;
                }
                else if (batting < 1000)
                {
                    Console.WriteLine("배팅 금액이 너무 작습니다. 다시 입력 하세요.");
                    continue;
                }

                playerMoney.batting = batting;

                var (cardNumber3, shape3) = c.DrawCard();
                Console.WriteLine($"세 번째 카드는 {shape3} / {cardNumber3} 입니다");

                if (cardNumber3 > cardNumber1 && cardNumber3 < cardNumber2)
                {
                    playerMoney.win_batt();
                }
                else
                {
                    playerMoney.lose_batt();
                }

                if (playerMoney.play_money <= 0)
                {
                    Console.WriteLine("파산 되었습니다");
                    Console.WriteLine("게임이 종료 합니다.");
                    break;
                }
                else if (playerMoney.play_money >= 30000)
                {
                    Console.WriteLine("게임에 승리 하였습니다");
                    Console.WriteLine("게임을 종료 합니다.");
                    break;
                }
            }
        }
    }
}

