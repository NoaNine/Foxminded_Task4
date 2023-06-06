using GameGuessNumber.Interface;

namespace GameGuessNumber
{
    public class ConsoleReader : IUserInteractionReader
    {
        public int Read()
        {
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int outputNamber))
            {
                throw new ArgumentException("Must be only a positive number");
            }
            return outputNamber;
        }
    }
}
