namespace GameGuessNumberTest
{
    [TestClass]
    public class ConsoleReaderTest
    {
        [TestMethod]
        public void Read_CorrectValueOutput()
        {
            var userInteractionReaderMock = new Mock<IUserInteractionReader>();
            var expected = 10;
            userInteractionReaderMock.Setup(m => m.Read()).Returns(() => expected);
            var result = userInteractionReaderMock.Object;
            Assert.AreEqual(expected, result.Read());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Read_ThrowException()
        {

        }
    }
}
