using GameGuessNumber.Interface;

namespace GameGuessNumber
{
    public class ConsoleWriter : IUserInteractionWriter
    {
        public void Write(string text) => Console.WriteLine(text);
    }
}
