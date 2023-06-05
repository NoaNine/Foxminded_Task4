namespace GameGuessNumberTest
{
    [TestClass]
    public class ConsoleReaderTest
    {
        [TestMethod]
        public void ReadTest()
        {
            var userInteractionReaderMock = new Mock<IUserInteractionReader>();
            var readString = "10";
            userInteractionReaderMock.Setup(m => m.Read()).Returns(() => readString);
            var result = userInteractionReaderMock.Object;
            Assert.AreEqual(readString, result.Read());
        }
    }
}
