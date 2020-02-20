using System.Drawing;
using NUnit.Framework;
using static GomokuAI.Program;

namespace GomokuAI
{
    [TestFixture]
    internal class GomokuTests
    {
        [TestCase("1 1", "1 2", "1 3", "1 4", ExpectedResult = "1 5")]
        [TestCase("1 1", "2 1", "3 1", "4 1", ExpectedResult = "5 1")]
        [TestCase("1 1", "1 2", "1 4", "1 5", ExpectedResult = "1 3")]
        [TestCase("1 1", "2 1", "4 1", "5 1", ExpectedResult = "3 1")]
        [TestCase("1 1", "2 2", "3 3", "4 4", ExpectedResult = "5 5")]
        [TestCase("2 1", "3 2", "4 3", "5 4", ExpectedResult = "6 5")]
        [TestCase("12 1", "13 1", "14 1", "15 1", ExpectedResult = "11 1")]
        [TestCase("14 1", "13 2", "12 3", "11 4", ExpectedResult = "10 5")]
        [TestCase("15 1", "14 2", "12 4", "11 5", ExpectedResult = "13 3")]
        [TestCase("13 12", "12 13", "11 14", "10 15", ExpectedResult = "14 11")]
        [TestCase("12 11", "13 12", "14 13", "15 14", ExpectedResult = "11 10")]
        
        public string TestGetNextTurn(params string[] points)
        {
            const int playerNumber = 1;
            const int gameFieldSize = 15;
            
            int[,] gameField = new int[gameFieldSize + 1, gameFieldSize + 1];

            foreach (string point in points)
            {
                string[] s = point.Split();

                gameField[int.Parse(s[0]), int.Parse(s[1])] = playerNumber;
            }

            Point? nextTurn = GetBallNextPosition(gameField, gameFieldSize, playerNumber);

            return nextTurn.HasValue
                ? $"{nextTurn.Value.X} {nextTurn.Value.Y}"
                : "";
        }
    }
}
