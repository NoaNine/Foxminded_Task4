namespace GameGuessNumberTest
{
    [TestClass]
    public class ConsoleReaderTest
    {
        [TestMethod]
        public void ReadTest()
        {
            var userInteractionReaderMock = new Mock<IUserInteractionReader>();
            var consoleReader = userInteractionReaderMock.Object;
            var input = consoleReader.Read();
            userInteractionReaderMock.Verify();
        }
    }
}
