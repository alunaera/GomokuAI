using NUnit.Framework;

namespace GomokuAI
{
    [TestFixture]
    internal class GomokuTests
    {
        [Test]
        public void MyFirstTest()
        {
            int x = 2;
            int y = 2;
            int result = x * y;
            
            Assert.That(result, Is.EqualTo(4));
        }
    }
}
