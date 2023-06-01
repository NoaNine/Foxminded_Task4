using GameGuessNumber.Interface;
using GameGuessNumber.Resources;
using Microsoft.Extensions.Options;

namespace GameGuessNumber
{
    public class Game
    {
        private readonly IUserInteractionReader _reader;
        private readonly IUserInteractionWriter _writer;
        private readonly INumberGenerator _generator;
        private readonly Settings _settings;
        private int HiddenNumber;

        public Game(IUserInteractionReader input, IUserInteractionWriter output, INumberGenerator generator, IOptionsMonitor<Settings> settings)
        {
            _reader = input ?? throw new ArgumentNullException(nameof(input));
            _writer = output ?? throw new ArgumentNullException(nameof(output));
            _generator = generator ?? throw new ArgumentNullException(nameof(generator));
            _settings = settings.CurrentValue ?? throw new ArgumentNullException(nameof(settings));
            ValidSettings();
        }

        public void StartGame()
        {
            GenerateHiddenNumber(_generator);
            int countRetry = 0;
            do
            {
                countRetry++;
                string input = _reader.Read();
                if (!int.TryParse(input, out int inputNamber))
                {
                    _writer.Write(Messages.Invalid);
                    continue;
                }
                HandleAnswer(inputNamber);
            }
            while (countRetry < _settings.MaxNumberAttempts);
            if (countRetry == _settings.MaxNumberAttempts)
            {
                _writer.Write(Messages.Limit);
            }
        }

        private void GenerateHiddenNumber(INumberGenerator generator) => 
            HiddenNumber = generator.Generate(_settings.MinValueOfHiddenNumber, _settings.MaxValueOfHiddenNumber);

        private void HandleAnswer(int inputNamber)
        {
            if (inputNamber == HiddenNumber)
            {
                _writer.Write(Messages.Winner);
            }
            if (inputNamber < HiddenNumber)
            {
                _writer.Write(Messages.Bigger);
            }
            if (inputNamber > HiddenNumber)
            {
                _writer.Write(Messages.Smaller);
            }
        }

        private void ValidSettings()
        {
            throw new ArgumentException();
        }
    }
}