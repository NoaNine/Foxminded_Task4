namespace GameGuessNumberTest
{
    [TestClass]
    public class ConsoleWriterTest
    {
        [TestMethod]
        public void Write_WriteToConsole()
        {
            var userInteractionWriterMock = new Mock<IUserInteractionWriter>();
            var consoleWriter = userInteractionWriterMock.Object;
            consoleWriter.Write("Hi");
            userInteractionWriterMock.Verify(m => m.Write("Hi"), Times.Once);
        }
    }
}
