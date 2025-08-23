using System.Security.Cryptography;

namespace _2048
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                    game.Keys(keyInfo);

                }
            }
        }
    }
}
