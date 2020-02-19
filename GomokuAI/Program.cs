using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GomokuAI
{
    internal class Program
    {
        public static Point[] GetNextWinningTurns(int[,] gameField, int gameFieldSize, int playerNumber,
            int searchOffset)
        {
            List<Point> winningPointsList = new List<Point>();

            for (int i = 1; i <= gameFieldSize; i++)
            {
                for (int j = 1; j <= gameFieldSize; j++)
                {
                    int horizontalComboPoint = 0;
                    int verticalComboPoint = 0;
                    int leftDiagonalComboPoint = 0;
                    int rightDiagonalComboPoint = 0;

                    if (gameField[i, j] != 0)
                        continue;

                    for (int offset = -searchOffset; offset <= searchOffset; offset++)
                    {
                        if (offset == 0)
                            continue;

                        if (i + offset >= 1 && i + offset <= gameFieldSize &&
                            gameField[i + offset, j] == playerNumber)
                            horizontalComboPoint++;
                        else
                            horizontalComboPoint = 0;

                        if (j + offset >= 1 && j + offset <= gameFieldSize &&
                            gameField[i, j + offset] == playerNumber)
                            verticalComboPoint++;
                        else
                            verticalComboPoint = 0;

                        if (i + offset >= 1 && i + offset <= gameFieldSize &&
                            j + offset >= 1 && j + offset <= gameFieldSize &&
                            gameField[i + offset, j + offset] == playerNumber)
                            leftDiagonalComboPoint++;
                        else
                            leftDiagonalComboPoint = 0;

                        if (i + offset >= 1 && i + offset <= gameFieldSize &&
                            j - offset >= 1 && j - offset <= gameFieldSize &&
                            gameField[i + offset, j - offset] == playerNumber)
                            rightDiagonalComboPoint++;
                        else
                            rightDiagonalComboPoint = 0;

                        if (horizontalComboPoint != searchOffset && verticalComboPoint != searchOffset &&
                            leftDiagonalComboPoint != searchOffset && rightDiagonalComboPoint != searchOffset)
                            continue;

                        winningPointsList.Add(new Point(i, j));
                    }
                }
            }

            return winningPointsList.ToArray();
        }

        public static Point? GetNextTurn(int[,] gameField, int gameFieldSize, int playerNumber, int searchOffset)
        {
            if (searchOffset == 4)
            {
                Point[] nextWinningTurns = GetNextWinningTurns(gameField, gameFieldSize, playerNumber, searchOffset);

                if (nextWinningTurns.Length > 0)
                    return nextWinningTurns.OrderBy(_ => Guid.NewGuid()).Last();
            }

            int minX = gameFieldSize;
            int minY = gameFieldSize;
            int maxX = 0;
            int maxY = 0;

            for (int x = 1; x <= gameFieldSize; x++)
            {
                for (int y = 1; y <= gameFieldSize; y++)
                {

                    if (gameField[x, y] == 0)
                        continue;

                    if (x <= minX)
                        minX = x > 1 ? x - 1 : x;

                    if (y <= minY)
                        minY = y > 1 ? y - 1 : y;

                    if (x >= maxX)
                        maxX = x < gameFieldSize ? x + 1 : x;

                    if (y >= maxY)
                        maxY = y < gameFieldSize ? y + 1 : y;
                }
            }

            var pointsList = new List<(Point Point, int WinningTurnsOpenCount)>();

            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    if (gameField[i, j] != 0)
                        continue;

                    gameField[i, j] = playerNumber;

                    Point[]  nextWinningTurns = GetNextWinningTurns(gameField, gameFieldSize, playerNumber, searchOffset);

                    if (nextWinningTurns.Length != 0)
                        pointsList.Add((new Point(i, j), nextWinningTurns.Length));

                    gameField[i, j] = 0;
                }
            }

            if (pointsList.Count == 0)
                return null;

            return pointsList.OrderByDescending(x => x.WinningTurnsOpenCount)
                             .ThenBy(_ => Guid.NewGuid())
                             .First()
                             .Point;
        }

        private static Point GetBallNextPosition(int[,] gameField, int gameFieldSize, int playerNumber)
        {
            for (int searchOffset = 4; searchOffset >= 1; searchOffset--)
            {
                for (int playerOffset = 0; playerOffset <= 1; playerOffset++)
                {
                    int currentPlayer = (playerNumber + playerOffset - 1) % 2 + 1;

                    Point? nextTurn = GetNextTurn(gameField, gameFieldSize, currentPlayer, searchOffset);

                    if (nextTurn.HasValue)
                        return nextTurn.Value;
                }
            }

            return new Point(7, 7);
        }

        public static void Main()
        {
            while (true)
            {
               
                int playerNumber = int.Parse(Console.ReadLine());
                int gameFieldSize = int.Parse(Console.ReadLine());
                int[,] gameField = new int[gameFieldSize + 1, gameFieldSize + 1];

                for (int i = 1; i <= gameFieldSize; i++)
                {
                    for (int j = 1; j <= gameFieldSize; j++)
                    {
                        gameField[i, j] = int.Parse(Console.ReadLine());
                    }
                }

                Point ballsNextPosition = GetBallNextPosition(gameField, gameFieldSize, playerNumber);

                Console.WriteLine("{0}:{1}", ballsNextPosition.X, ballsNextPosition.Y);
            }
        }
    }
}
