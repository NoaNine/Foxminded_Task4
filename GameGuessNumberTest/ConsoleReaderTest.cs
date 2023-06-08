namespace GameGuessNumberTest
{
    [TestClass]
    public class ConsoleReaderTest
    {
        private readonly Mock<IUserInteractionReader> _userInteractionReaderMock = new Mock<IUserInteractionReader>();
        [TestMethod]
        public void Read_CorrectValueOutput()
        {
            var expected = 10;
            _userInteractionReaderMock.Setup(m => m.Read()).Returns(() => expected);
            var readerObject = _userInteractionReaderMock.Object;
            Assert.AreEqual(expected, readerObject.Read());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Read_ThrowException()
        {
            _userInteractionReaderMock.Setup(m => m.Read()).Throws(new ArgumentException());
            var readerObject = _userInteractionReaderMock.Object;
            readerObject.Read();
        }
    }
}
