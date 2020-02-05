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
            Point nextTurn = GetNextTurnIfVerticalOpenFour(gameField, gameFieldSize, playerNumber);

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
            Point nextTurn = GetNextTurnIfVerticalOpenFour(gameField, gameFieldSize, playerNumber);

            Assert.That(result, Is.EqualTo(nextTurn));
        }

        [Test]
        public void TestHorizontalOpenFourBlockedLeft()
        {
            int playerNumber = 1,
                gameFieldSize = 15;
            int[,] gameField = new int[100, 100];

            gameField[1, 1] = playerNumber;
            gameField[2, 1] = playerNumber;
            gameField[3, 1] = playerNumber;
            gameField[4, 1] = playerNumber;
            gameField[5, 1] = 0;

            Point result = new Point(5, 1);
            Point nextTurn = GetNextTurnIfHorizontalOpenFour(gameField, gameFieldSize, playerNumber);

            Assert.That(result, Is.EqualTo(nextTurn));
        }

        [Test]
        public void TestHorizontalOpenFourBlockedRight()
        {
            int playerNumber = 1,
                gameFieldSize = 15;
            int[,] gameField = new int[100, 100];

            gameField[11, 1] = 0;
            gameField[12, 1] = playerNumber;
            gameField[13, 1] = playerNumber;
            gameField[14, 1] = playerNumber;
            gameField[15, 1] = playerNumber;

            Point result = new Point(11, 1);
            Point nextTurn = GetNextTurnIfHorizontalOpenFour(gameField, gameFieldSize, playerNumber);

            Assert.That(result, Is.EqualTo(nextTurn));
        }

        [Test]
        public void TestLeftDiagonalOpenFourBlockedBottom()
        {
            int playerNumber = 1,
                gameFieldSize = 15;
            int[,] gameField = new int[100, 100];

            gameField[11, 10] = 0;
            gameField[12, 11] = playerNumber;
            gameField[13, 12] = playerNumber;
            gameField[14, 13] = playerNumber;
            gameField[15, 14] = playerNumber;

            Point result = new Point(11, 10);
            Point nextTurn = GetNextTurnIfLeftDiagonalOpenFour(gameField, gameFieldSize, playerNumber);

            Assert.That(result, Is.EqualTo(nextTurn));
        }

        [Test]
        public void TestLeftDiagonalOpenFourBlockedTop()
        {
            int playerNumber = 1,
                gameFieldSize = 15;
            int[,] gameField = new int[100, 100];

            gameField[2, 1] = playerNumber;
            gameField[3, 2] = playerNumber;
            gameField[4, 3] = playerNumber;
            gameField[5, 4] = playerNumber;
            gameField[6, 5] = 0;

            Point result = new Point(6, 5);
            Point nextTurn = GetNextTurnIfLeftDiagonalOpenFour(gameField, gameFieldSize, playerNumber);

            Assert.That(result, Is.EqualTo(nextTurn));
        }
    }
}
