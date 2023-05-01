using Communicator;

namespace GuessNumberApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Croupier croupier = new Croupier(Console.ReadLine, Console.WriteLine);
            Console.WriteLine("Guess what number from 0 to 100");
            while (true)
            {
                croupier.AskMeNumber();
            }
        }
    }
}