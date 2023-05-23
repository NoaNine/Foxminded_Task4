namespace Communicator
{
    public class NumberGenerator : INumberGenerator
    {
        private Random _random = new Random();
        private int _minRange;
        private int _maxRange;

        public int Number { get; private set; }

        public NumberGenerator(int minRange, int maxRange)
        {
            ThrowArgumentException(minRange, maxRange);
            _minRange = minRange;
            _maxRange = maxRange + 1;
            ChooseNumber();
        }

        public void GenerateNumber()
        {
            Number = _random.Next(_minRange, _maxRange);
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
