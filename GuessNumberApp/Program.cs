using Communicator;

namespace GuessNumberApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Generator generator = new Generator(0, 100);
            Croupier croupier = new Croupier(Console.ReadLine, Console.WriteLine, generator);
            Console.WriteLine(Resources.Messages.Greeting);
            while (true)
            {
                if (croupier.Winner)
                {
                    croupier.PlayAgain();
                }
                if (!croupier.Retry)
                {
                    break;
                }
                if (!croupier.Winner && croupier.Retry)
                {
                    Console.WriteLine(Resources.Messages.Instruction);
                    croupier.AskMeNumber();
                }
            }
        }
    }
}