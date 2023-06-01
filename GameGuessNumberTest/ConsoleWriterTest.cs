namespace GameGuessNumberTest
{
    [TestClass]
    public class ConsoleWriterTest
    {
        [TestMethod]
        public void WriteTest()
        {
            var userInteractionWriterMock = new Mock<IUserInteractionWriter>();
            var consoleWriter = userInteractionWriterMock.Object;
            consoleWriter.Write("Hi");
            userInteractionWriterMock.Verify(m => m.Write("Hi"), Times.Once);
        }

        [TestMethod]
        public void WriteTest2()
        {
            var userInteractionWriterMock = new Mock<IUserInteractionWriter>();
            var consoleWriter = userInteractionWriterMock.Object;
            var expectedOutput = "Hi";
            var output = new StringWriter();
            Console.SetOut(output);
            consoleWriter.Write("Hi");
            Assert.AreEqual(expectedOutput, output);
        }
    }
}
