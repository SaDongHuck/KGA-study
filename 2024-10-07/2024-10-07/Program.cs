using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Linq;

//4.배열
//ㄴ학생성적 입력 받아 배열에 저장
//ㄴ입력된 성적 중 가장 높은 성적 과 가장 낮은 성적 출력
//ㄴ평균 성적 계산
//ㄴ오름차순 정렬
namespace _2024_10_07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] scores = new int[5];
            Console.WriteLine($"학생성적을 입력: ");
            for(int i = 0; i<scores.Length; i++)
            {
                scores[i] = Convert.ToInt32(Console.ReadLine());
            }
            int sum = 0;
            int avg = 0;
            Console.WriteLine("가장 높은 성적은 ");
            Console.WriteLine(scores.Max()); // 가장 높은 성적
            Console.WriteLine("가장 낮은 성적은 ");
            Console.WriteLine(scores.Min()); // 가장 낮은 성적
            foreach(int i in scores)
            {
                sum += i;
            }
            avg = sum / scores.Length; //평균 성적
            Console.WriteLine("학생 평균 성적은 ");
            Console.WriteLine(avg);
            Console.WriteLine("학생 정렬한 성적은 ");
            Array.Sort(scores); // 오름차순정렬
            Console.WriteLine(string.Join(",",scores));
        }
    }
}
