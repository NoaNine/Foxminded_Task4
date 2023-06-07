using Microsoft.Extensions.Options;

namespace GameGuessNumberTest
{
    [TestClass]
    public class GameTest
    {
        private readonly Mock<INumberGenerator> _numbergeneratorMock = new Mock<INumberGenerator>();
        private readonly Mock<IUserInteractionReader> _userInteractionReaderMock = new Mock<IUserInteractionReader>();
        private readonly Mock<IUserInteractionWriter> _userInteractionWriterMock = new Mock<IUserInteractionWriter>();
        private readonly Settings _settings = new Settings() { MinValueOfHiddenNumber = 1, MaxValueOfHiddenNumber = 100, MaxNumberAttempts = 2 };

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
        }

        [DataTestMethod]
        [DataRow(50, 49, "Hidden number is bigger, try again.")]
        [DataRow(50, 51, "Hidden number is smaller, try again.")]
        [DataRow(50, 101, "You entered the incorrect data, must be a number from 0 to 100.")]
        public void StartGame_LostAndCorrectAnswer(int hiddenNumber, int inputNumber, string expectedResult)
        {
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
                Assert.AreEqual(expectedResult, value);
            });
        }

        [TestMethod]
        public void StartGame_AttemptLimit()
        {
            var optionsMonitorMock = Mock.Of<IOptionsMonitor<Settings>>(_ => _.CurrentValue == _settings);
            var hiddenNumber = 50;
            var expectedResult = @"Hidden number is bigger, try again.
Hidden number is bigger, try again.
Number of attempts exceeded.";
            var game = new Game(
                _userInteractionReaderMock.Object,
                _userInteractionWriterMock.Object,
                _numbergeneratorMock.Object,
                optionsMonitorMock
            );
            _numbergeneratorMock.Setup(g => g.Generate(_settings.MinValueOfHiddenNumber, _settings.MaxValueOfHiddenNumber)).Returns(hiddenNumber);
            _userInteractionReaderMock.SetupSequence(r => r.Read())
                .Returns(hiddenNumber - 1)
                .Returns(hiddenNumber - 1)
                .Returns(hiddenNumber - 1);
            _userInteractionWriterMock.Setup(w => w.Write(It.IsAny<string>())).Callback((string value) =>
            {
                Assert.AreEqual(expectedResult, value);
            });
        }
    }
}