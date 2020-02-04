using System.Drawing;
using NUnit.Framework;
using static GomokuAI.Program;

namespace GomokuAI
{
    [TestFixture]
    internal class GomokuTests
    {
        [Test]
        public void TestVerticalOpenFourBlockedTop()
        {
            int playerNumber = 1,
                gameFieldSize = 15;
            int[,] gameField = new int[100, 100];

            gameField[1, 1] = playerNumber + 1;
            gameField[1, 2] = playerNumber;
            gameField[1, 3] = playerNumber;
            gameField[1, 4] = playerNumber;
            gameField[1, 5] = playerNumber;

            Point result = new Point(1, 6);
            Point nextTurn = GetNextTurnIfVerticalOpenFour(Point.Empty, gameField, gameFieldSize, playerNumber);

             Assert.That(result, Is.EqualTo(nextTurn));
        }

        [Test]
        public void TestVerticalOpenFourBlockedBottom()
        {
            int playerNumber = 1,
                gameFieldSize = 15;
            int[,] gameField = new int[100, 100];

            gameField[1, 11] = 0;
            gameField[1, 12] = playerNumber;
            gameField[1, 13] = playerNumber;
            gameField[1, 14] = playerNumber;
            gameField[1, 15] = playerNumber;

            Point result = new Point(1, 11);
            Point nextTurn = GetNextTurnIfVerticalOpenFour(Point.Empty, gameField, gameFieldSize, playerNumber);

            Assert.That(result, Is.EqualTo(nextTurn));
        }
    }
}
