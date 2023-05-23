namespace Communicator
{
    public class Croupier
    {
        private readonly Func<string> _inputProvider;
        private readonly Action<string> _outputProvider;
        private NumberGenerator _generator;

        public bool Winner { get; private set; }
        public bool Retry { get; private set; } = true;

        public Croupier(Func<string> inputProvider, Action<string> outputProvider, NumberGenerator generator)
        {
            _inputProvider = inputProvider;
            _outputProvider = outputProvider;
            _generator = generator;
        }

        public void AskMeNumber()
        {
            string input = _inputProvider() ?? string.Empty;
            if (!int.TryParse(input, out int inputNamber))
            {
                InvalidNumber();
                return;
            }
            EqualsMyNumber(inputNamber);
        }

        public void PlayAgain()
        {
            _outputProvider(Resources.Messages.Again);
            string input = _inputProvider();
            switch (input)
            {
                case "+":
                    Winner = false;
                    break;
                case "-":
                    Retry = false;
                    break;
                default:
                    _outputProvider(Resources.Messages.Incorrect);
                    break;
            }
        }

        private void InvalidNumber()
        {
            _outputProvider(Resources.Messages.Invalid);
        }

        private void EqualsMyNumber(int inputNamber)
        {
            if (inputNamber == _generator.Number)
            {
                IsWinner();
            }
            IsBiggerOrSmaller(inputNamber);
        }

        private void IsBiggerOrSmaller(int inputNamber)
        {
            if (inputNamber < _generator.Number)
            {
                _outputProvider(Resources.Messages.Bigger);
            }
            if (inputNamber > _generator.Number)
            {
                _outputProvider(Resources.Messages.Smaller);
            }
        }

        private void IsWinner()
        {
            Winner = true;
            _outputProvider(Resources.Messages.Winner);
        }
    }
}