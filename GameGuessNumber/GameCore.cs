using GameGuessNumber.Interface;
using Microsoft.Extensions.Options;

namespace GameGuessNumber
{
    public enum GameStatus
    {
        Win,
        Bigger,
        Smaller,
        LetsTry,
        Start,
        Limit
    }

    public delegate void Notification(GameCore sender);

    public class GameCore : IGameNotification
    {
        private int _hiddenNumber, _userNumber;
        private readonly INumberGenerator _generator;
        public readonly Settings Settings;
        public event Notification? Notify;

        public GameStatus Status { get; private set; }

        public GameCore(INumberGenerator generator, IOptionsMonitor<Settings> settings)
        {
            _generator = generator ?? throw new ArgumentNullException(nameof(generator));
            Settings = settings.CurrentValue ?? throw new ArgumentNullException(nameof(settings));
            Settings.ValidSettings();
        }

        public void StartGame()
        {
            GenerateHiddenNumber(_generator);
            int attempts = Settings.MaxNumberAttempts;
            InvokeNotify(GameStatus.Start);
            do
            {
                InvokeNotify(GameStatus.LetsTry);
                GameStatus result = Compare();
                InvokeNotify(result);
                if (Status == GameStatus.Win)
                {
                    return;
                }
                attempts--;
            }
            while (attempts > 0);
            if (attempts == 0)
            {
                InvokeNotify(GameStatus.Limit);
            }
        }

        public void SetUserNumber(int number)
        {
            if (number < Settings.MinValueOfHiddenNumber || number > Settings.MaxValueOfHiddenNumber)
            {
                throw new ArgumentException("Incorrect input value");
            }
            _userNumber = number;
        }

        private void GenerateHiddenNumber(INumberGenerator generator) =>
            _hiddenNumber = generator.Generate(Settings.MinValueOfHiddenNumber, Settings.MaxValueOfHiddenNumber);

        private GameStatus Compare()
        {
            if (_hiddenNumber > _userNumber)
            {
                return GameStatus.Bigger;
            }
            if (_hiddenNumber < _userNumber)
            {
                return GameStatus.Smaller;
            }
            return GameStatus.Win;
        }

        private void InvokeNotify(GameStatus status)
        {
            Status = status;
            Notify?.Invoke(this);
        }
    }
}
