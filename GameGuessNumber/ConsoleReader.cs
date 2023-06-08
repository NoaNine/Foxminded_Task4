using GameGuessNumber.Interface;

namespace GameGuessNumber
{
    public class ConsoleReader : IUserInteractionReader
    {
        public int Read() => Parse(Console.ReadLine());

        private int Parse(string input)
        {
            if (!int.TryParse(input, out int outputNamber))
            {
                throw new ArgumentException("Must be only a positive number");
            }
            return outputNamber;
        }
    }
}
