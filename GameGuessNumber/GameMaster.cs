using GameGuessNumber.Interface;
using GameGuessNumber.Resources;
using Microsoft.Extensions.Options;

namespace GameGuessNumber
{
    public class GameMaster
    {
        private readonly IUserInteractionReader _reader;
        private readonly IUserInteractionWriter _writer;
        private readonly INumberGenerator _generator;
        private readonly Settings _settings;

        private int HiddenNumber { get; set; }

        public GameMaster(IUserInteractionReader input, IUserInteractionWriter output, INumberGenerator generator, IOptionsMonitor<Settings> settings)
        {
            _reader = input ?? throw new ArgumentNullException(nameof(input));
            _writer = output ?? throw new ArgumentNullException(nameof(output));
            _generator = generator ?? throw new ArgumentNullException(nameof(generator));
            _settings = settings.CurrentValue ?? throw new ArgumentNullException(nameof(settings));
            GenerateHiddenNumber(_generator);
        }

        public void StartGame()
        {
            int countRetry = 1;
            while (countRetry <= _settings.MaxNumberAttempts)
            {
                string input = _reader.Read();
                if (!int.TryParse(input, out int inputNamber))
                {
                    _writer.Write(Messages.Invalid);
                }
                EqualsNumber(inputNamber);
                countRetry++;
            }
            if (countRetry == _settings.MaxNumberAttempts)
            {
                _writer.Write(Messages.Limit);
            }
        }

        private void GenerateHiddenNumber(INumberGenerator generator) => 
            HiddenNumber = generator.GenerateNumber(_settings.MinValueOfHiddenNumber, _settings.MaxValueOfHiddenNumber);

        private void EqualsNumber(int inputNamber)
        {
            if (inputNamber == HiddenNumber)
            {
                _writer.Write(Messages.Winner);
            }
            IsBiggerOrSmaller(inputNamber);
        }

        private void IsBiggerOrSmaller(int inputNamber)
        {
            if (inputNamber < HiddenNumber)
            {
                _writer.Write(Messages.Bigger);
            }
            if (inputNamber > HiddenNumber)
            {
                _writer.Write(Messages.Smaller);
            }
        }
    }
}