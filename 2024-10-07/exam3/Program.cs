//5.문자열 내 단어 뒤집기
//ㄴ문자열을 입력받고 해당 문자열의 각 단어를 뒤집는다.
//ㄴ입력예시)"abcd efgh"
//ㄴ출력예시)dcba hgfe
namespace exam3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str;
            str = Console.ReadLine();
            string[] names = str.Split();
            foreach(string name in names)
            {
                string newstr = new string(name.Reverse().ToArray());
                Console.WriteLine(newstr);
            }
        }
    }
}
