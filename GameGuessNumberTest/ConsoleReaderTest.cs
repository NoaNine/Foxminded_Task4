namespace GameGuessNumberTest
{
    [TestClass]
    public class ConsoleReaderTest
    {
        [TestMethod]
        public void ReadTest()
        {
            var userInteractionReaderMock = new Mock<IUserInteractionReader>();
            var expected = "10";
            userInteractionReaderMock.Setup(m => m.Read()).Returns(() => expected);
            var result = userInteractionReaderMock.Object;
            Assert.AreEqual(expected, result.Read());
        }
    }
}
