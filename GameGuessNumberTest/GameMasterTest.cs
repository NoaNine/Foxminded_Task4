using Microsoft.Extensions.Options;

namespace GameGuessNumberTest
{
    [TestClass]
    public class GameMasterTest
    {
        private readonly Mock<INumberGenerator> numbergeneratorMock = new Mock<INumberGenerator>();
        private readonly Mock<IUserInteractionReader> userInteractionReaderMock = new Mock<IUserInteractionReader>();
        private readonly Mock<IUserInteractionWriter> userInteractionWriterMock = new Mock<IUserInteractionWriter>();
        private readonly Mock<IOptionsMonitor<Settings>> optionsMonitorMock = new Mock<IOptionsMonitor<Settings>>();

        [TestMethod]
        public void StartGameTest()
        {
            var settings = new Settings();
            settings.MinValueOfHiddenNumber = 0;
            settings.MaxValueOfHiddenNumber = 100;
            settings.MaxNumberAttempts = 1;
            var gameMaster = new GameMaster
                (
                userInteractionReaderMock.Object,
                userInteractionWriterMock.Object,
                numbergeneratorMock.Object, 
                optionsMonitorMock.Object
                );
            var output = new StringWriter();
            Console.SetOut(output);
            var input = new StringReader(@"101");
            Console.SetIn(input);
            var expectedOutput = @"Hidden number is smaller, try again.";


            gameMaster.StartGame();

            Assert.AreEqual(expectedOutput, output.ToString());
        }
    }
}