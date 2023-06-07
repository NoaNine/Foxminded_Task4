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
        private int _hiddenNumber;

        public Game(IUserInteractionReader reader, IUserInteractionWriter writer, INumberGenerator generator, IOptionsMonitor<Settings> settings)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _generator = generator ?? throw new ArgumentNullException(nameof(generator));
            _settings = settings.CurrentValue ?? throw new ArgumentNullException(nameof(settings));
            _settings.ValidSettings();
        }

        public void StartGame()
        {
            GenerateHiddenNumber(_generator);
            int countRetry = 0;
            do
            {
                int answer = _reader.Read();
                if (IsWinner(answer))
                {
                    _writer.Write(Messages.Winner);
                    break;
                }
                HandleAnswer(answer);
                countRetry++;
            }
            while (countRetry < _settings.MaxNumberAttempts);
            CheckAttempts(countRetry);
        }

        private void GenerateHiddenNumber(INumberGenerator generator) =>
            _hiddenNumber = generator.Generate(_settings.MinValueOfHiddenNumber, _settings.MaxValueOfHiddenNumber);

        private bool IsWinner(int answer) => _hiddenNumber == answer ? true : false;

        private void CheckAttempts(int countRetry)
        {
            if (countRetry == _settings.MaxNumberAttempts)
            {
                _writer.Write(Messages.AttemptLimit);
            }
        }
        private void HandleAnswer(int answer)
        {
            if (answer < _settings.MinValueOfHiddenNumber || answer > _settings.MaxValueOfHiddenNumber)
            {
                _writer.Write(Messages.Invalid);
            }
            if (answer < _hiddenNumber)
            {
                _writer.Write(Messages.Bigger);
            }
            if (answer > _hiddenNumber)
            {
                _writer.Write(Messages.Smaller);
            }
        }
    }
}