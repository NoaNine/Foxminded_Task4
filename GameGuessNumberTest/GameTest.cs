using Microsoft.Extensions.Options;

namespace GameGuessNumberTest
{
    [TestClass]
    public class GameTest
    {
        private readonly Mock<INumberGenerator> numbergeneratorMock = new Mock<INumberGenerator>();
        private readonly Mock<IUserInteractionReader> userInteractionReaderMock = new Mock<IUserInteractionReader>();
        private readonly Mock<IUserInteractionWriter> userInteractionWriterMock = new Mock<IUserInteractionWriter>();
        private readonly Settings settings = new Settings() { MinValueOfHiddenNumber = 1, MaxValueOfHiddenNumber = 100, MaxNumberAttempts = 10 };

        [TestMethod]
        public void StartGameTest()
        {
            var optionsMonitorMock = Mock.Of<IOptionsMonitor<Settings>>(_ => _.CurrentValue == settings);
            var game = new Game
                (
                userInteractionReaderMock.Object,
                userInteractionWriterMock.Object,
                numbergeneratorMock.Object, 
                optionsMonitorMock
                );
            game.StartGame();
            var input = new StringReader(@"101");
            Console.SetIn(input);
            var output = new StringWriter();
            Console.SetOut(output);


            
            var expectedOutput = @"Hidden number is smaller, try again.";



            Assert.AreEqual(expectedOutput, output.ToString());
        }
    }
}