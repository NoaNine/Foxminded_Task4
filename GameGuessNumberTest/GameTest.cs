using Microsoft.Extensions.Options;

namespace GameGuessNumberTest
{
    [TestClass]
    public class GameTest
    {
        private readonly Mock<INumberGenerator> _numbergeneratorMock = new Mock<INumberGenerator>();
        private readonly Mock<IUserInteractionReader> _userInteractionReaderMock = new Mock<IUserInteractionReader>();
        private readonly Mock<IUserInteractionWriter> _userInteractionWriterMock = new Mock<IUserInteractionWriter>();
        private readonly Settings _settings = new Settings() { MinValueOfHiddenNumber = 1, MaxValueOfHiddenNumber = 100, MaxNumberAttempts = 1 };

        [TestMethod]
        public void StartGame_WinTheFirstTimeAround()
        {
            var optionsMonitorMock = Mock.Of<IOptionsMonitor<Settings>>(_ => _.CurrentValue == _settings);
            var hiddenNumber = 50;
            var expectedResult = "You WON, congratulation!";
            var game = new Game(
                _userInteractionReaderMock.Object,
                _userInteractionWriterMock.Object,
                _numbergeneratorMock.Object,
                optionsMonitorMock
            );
            _numbergeneratorMock.Setup(g => g.Generate(_settings.MinValueOfHiddenNumber, _settings.MaxValueOfHiddenNumber)).Returns(hiddenNumber);
            _userInteractionReaderMock.Setup(r => r.Read()).Returns(hiddenNumber);
            _userInteractionWriterMock.Setup(w => w.Write(It.IsAny<string>())).Callback((string value) =>
            {
                Assert.AreEqual(expectedResult, value);
            });
            game.StartGame();
        }

        [DataTestMethod]
        [DataRow(50, 49, "Hidden number is bigger, try again.")]
        [DataRow(50, 51, "Hidden number is smaller, try again.")]
        [DataRow(50, 101, "You entered the incorrect data, must be a number from 0 to 100.")]
        public void StartGame_LostAndCorrectAnswer(int hiddenNumber, int inputNumber, string expectedMessage)
        {
            var expected = new List<string>() { expectedMessage, "Number of attempts exceeded."};
            var result = new List<string>();
            var optionsMonitorMock = Mock.Of<IOptionsMonitor<Settings>>(_ => _.CurrentValue == _settings);
            var game = new Game(
                _userInteractionReaderMock.Object,
                _userInteractionWriterMock.Object,
                _numbergeneratorMock.Object,
                optionsMonitorMock
            );
            _numbergeneratorMock.Setup(g => g.Generate(_settings.MinValueOfHiddenNumber, _settings.MaxValueOfHiddenNumber)).Returns(hiddenNumber);
            _userInteractionReaderMock.Setup(r => r.Read()).Returns(inputNumber);
            _userInteractionWriterMock.Setup(w => w.Write(It.IsAny<string>())).Callback((string value) =>
            {
                result.Add(value);
            });
            game.StartGame();
            for(int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }

        [TestMethod]
        public void StartGame_AttemptLimit()
        {
            var result = 0;
            var optionsMonitorMock = Mock.Of<IOptionsMonitor<Settings>>(_ => _.CurrentValue == _settings);
            var game = new Game(
                _userInteractionReaderMock.Object,
                _userInteractionWriterMock.Object,
                _numbergeneratorMock.Object,
                optionsMonitorMock
            );
            _numbergeneratorMock.Setup(g => g.Generate(_settings.MinValueOfHiddenNumber, _settings.MaxValueOfHiddenNumber)).Returns(50);
            _userInteractionReaderMock.Setup(r => r.Read()).Returns(40);
            _userInteractionWriterMock.Setup(w => w.Write(It.IsAny<string>())).Callback((string value) =>
            {
                if (value != "Number of attempts exceeded.")
                {
                    result++;
                }
            });
            game.StartGame();
            Assert.AreEqual(_settings.MaxNumberAttempts, result);
        }
    }
}