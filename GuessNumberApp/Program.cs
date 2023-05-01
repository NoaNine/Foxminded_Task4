using Communicator;

namespace GuessNumberApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Croupier croupier = new Croupier(Console.ReadLine, Console.WriteLine);
            Console.WriteLine(Resources.Messages.Greeting);
            Console.WriteLine(Resources.Messages.Instruction);
            while (true)
            {
                croupier.AskMeNumber();
            }
        }
    }
}