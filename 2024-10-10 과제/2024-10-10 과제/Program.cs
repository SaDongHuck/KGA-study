/*
k개의 공통된 배열 찾기
2. 입력
int[] arr1 = { 1,5,5,10};
int[] arr2 = { 3, 4, 5, 5, 10 };
int[] arr3 = { 5, 5, 10, 20 };

결과
5,10
*/
namespace _2024_10_10_과제
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            int[] arr1 = { 1, 5, 5, 10 };
            int[] arr2 = { 3, 4, 5, 5, 10 };
            int[] arr3 = { 5, 5, 10, 20 };
            HashSet<int> set1 = new HashSet<int>(arr1);
            HashSet<int> set2 = new HashSet<int>(arr3);
            HashSet<int> set3 = new HashSet<int>(arr2);

            set1.IntersectWith(set2);
            set1.IntersectWith(set3);

            foreach(var item  in set1)
            {
                Console.WriteLine(item);
            }
        }
    }
}
