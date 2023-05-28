using GameGuessNumber.Interface;

namespace GameGuessNumber
{
    public class ConsoleReader : IUserInteractionReader
    {
        public string Read() => Console.ReadLine();
    }
}
