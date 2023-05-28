using GameGuessNumber.Interface;

namespace GameGuessNumber
{
    public class NumberGenerator : INumberGenerator
    {
        private readonly Random _random = new Random();

        public int GenerateNumber(int minRange, int maxRange)
        {
            ThrowArgumentException(minRange, maxRange);
            return _random.Next(minRange, maxRange + 1);
        }

        private void ThrowArgumentException(int minRange, int maxRange)
        {
            if (minRange > maxRange)
            {
                throw new ArgumentException();
            }
        }
    }
}
