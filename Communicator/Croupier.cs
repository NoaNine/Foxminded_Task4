using System.Security.Cryptography.X509Certificates;

namespace Communicator
{
    public class Croupier
    {
        private readonly Func<string> _inputProvider;
        private readonly Action<string> _outputProvider;
        private Random _random = new Random();
        private int _number;
        private int _tryCounter = 0;

        public Croupier(Func<string> inputProvider, Action<string> outputProvider, int minRange = 0, int maxRange = 100)
        {
            if (minRange > maxRange)
            {
                throw new ArgumentException();
            }
            _inputProvider = inputProvider;
            _outputProvider = outputProvider;
            _number = _random.Next(minRange, maxRange + 1);
        }

        public void AskMeNumber()
        {
            string input = _inputProvider() ?? string.Empty;
            _tryCounter++;
            if (!int.TryParse(input, out int inputNamber))
            {
                InvalidNumber();
                return;
            }
            EqualsMyNumber(inputNamber);
        }

        private void InvalidNumber()
        {
            _outputProvider(Resources.Messages.Invalid);
        }

        private void EqualsMyNumber(int inputNamber)
        {
            if (inputNamber == _number)
            {
                IsWinner();
            }
            IsBiggerOrSmaller(inputNamber);
        }

        private void IsBiggerOrSmaller(int inputNamber)
        {
            if (inputNamber < _number)
            {
                _outputProvider(Resources.Messages.Bigger);
            }
            if (inputNamber > _number)
            {
                _outputProvider(Resources.Messages.Smaller);
            }
        }

        private void IsWinner()
        {
            _outputProvider(Resources.Messages.Winner + "\n" + Resources.Messages.Again);
        }
    }
}