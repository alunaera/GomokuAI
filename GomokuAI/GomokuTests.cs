using System.Drawing;
using NUnit.Framework;
using static GomokuAI.Program;

namespace GomokuAI
{
    [TestFixture]
    internal class GomokuTests
    {

        [Test, TestCaseSource(nameof(PointCases))]
        public void TestGetNextWinningTurn(Point firstPoint, Point secondPoint, Point thirdPoint, Point fourthPoint, Point resultPoint)
        {
            int playerNumber = 1,
                gameFieldSize = 15;
            int[,] gameField = new int[100, 100];

            gameField[firstPoint.X, firstPoint.Y] = playerNumber;
            gameField[secondPoint.X, secondPoint.Y] = playerNumber;
            gameField[thirdPoint.X, thirdPoint.Y] = playerNumber;
            gameField[fourthPoint.X, fourthPoint.Y] = playerNumber;
            gameField[resultPoint.X, resultPoint.Y] = 0;

            Point? nextTurn = GetNextWinningTurn(gameField, gameFieldSize, playerNumber);

             Assert.That(resultPoint, Is.EqualTo(nextTurn));
        }

        private static readonly object[] PointCases =
        {
            new[] {new Point(1, 1), new Point(1, 2), new Point(1, 3), new Point(1, 4), new Point(1, 5)},
            new[] {new Point(1, 1), new Point(2, 1), new Point(3, 1), new Point(4, 1), new Point(5, 1)},
            new[] {new Point(12, 1), new Point(13, 1), new Point(14, 1), new Point(15, 1), new Point(11, 1)},
            new[] {new Point(12, 11), new Point(13, 12), new Point(14, 13), new Point(15, 14), new Point(11, 10)},
            new[] {new Point(2, 1), new Point(3, 2), new Point(4, 3), new Point(5, 4), new Point(6, 5)},
            new[] {new Point(14, 1), new Point(13, 2), new Point(12, 3), new Point(11, 4), new Point(10, 5)},
            new[] {new Point(13, 12), new Point(12, 13), new Point(11, 14), new Point(10, 15), new Point(14, 11)},
            new[] {new Point(1, 1), new Point(1, 2), new Point(1, 4), new Point(1, 5), new Point(1, 3)},
            new[] {new Point(1, 1), new Point(2, 1), new Point(4, 1), new Point(5, 1), new Point(3, 1)},
            new[] {new Point(1, 1), new Point(2, 2), new Point(4, 4), new Point(5, 5), new Point(3, 3)},
            new[] {new Point(15, 1), new Point(14, 2), new Point(12, 4), new Point(11, 5), new Point(13, 3)}

        };
    }
}
