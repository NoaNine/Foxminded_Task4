using Communicator;

namespace GuessNumberApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Croupier croupier = new Croupier(Console.ReadLine, Console.WriteLine);
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
                if(!croupier.Winner && croupier.Retry) 
                {
                    Console.WriteLine(Resources.Messages.Instruction);
                    croupier.AskMeNumber();
                }
            }
        }
    }
}