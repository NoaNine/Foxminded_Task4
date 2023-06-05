namespace GameGuessNumberTest
{
    [TestClass]
    public class NumberGeneratorTest
    {
        private readonly Mock<INumberGenerator> numbergeneratorMock = new Mock<INumberGenerator>();

        [TestMethod]
        public void GenerateTest()
        {
            var expected = 50;
            numbergeneratorMock.Setup(g => g.Generate(0, 100)).Returns(() => expected);
            var generator = numbergeneratorMock.Object;
            var result = generator.Generate(0, 100);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArgumentExceptionTest()
        {
            numbergeneratorMock.Setup(g => g.Generate(2, 1)).Throws(new ArgumentException());
            var result = numbergeneratorMock.Object;
            result.Generate(2, 1);
        }
    }
}
