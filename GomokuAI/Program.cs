using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace GomokuAI
{
    class Program
    {
        public static Point[] GetNextWinningTurn(int[,] gameField, int gameFieldSize, int playerNumber,
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

        private static Point? GetNextTurnIfNotCanWin(int[,] gameField, int gameFieldSize, int playerNumber,
            int searchOffset)
        {
            List<Point> pointsList = new List<Point>();

            int minX = 15;
            int minY = 15;
            int maxX = 0;
            int maxY = 0;

            for (int x = 1; x <= gameFieldSize; x++)
                for (int y = 1; y <= gameFieldSize; y++)
                {

                    if (gameField[x, y] == 0)
                        continue;

                    if (x < minX)
                        minX = x > 1
                            ? x - 1
                            : x;

                    if (y < minY)
                        minY = y > 1
                            ? y - 1
                            : y;

                    if (x > maxX)
                        maxX = x < 15
                            ? x + 1
                            : x;

                    if (y > maxY)
                        maxY = y < 15
                            ? y + 1
                            : y;
                }

            for (int i = minX; i <= maxX; i++)
                for (int j = minY; j <= maxY; j++)
                {
                    if (gameField[i, j] == 0)
                        gameField[i, j] = playerNumber;
                    else
                        continue;

                    if (GetNextWinningTurn(gameField, gameFieldSize, playerNumber, searchOffset).Length == 0)
                    {
                        gameField[i, j] = 0;
                        continue;
                    }

                    pointsList.Add(new Point(i, j));
                    gameField[i, j] = 0;
                }

            if (pointsList.Count == 0)
                return null;

            Point? nextBallPosition = null;
            int maxWinningTurns = 0;

            foreach (Point point in pointsList)
            {
                gameField[point.X, point.Y] = playerNumber;

                if (GetNextWinningTurn(gameField, gameFieldSize, playerNumber, searchOffset).Length > maxWinningTurns)
                {
                    nextBallPosition = point;
                    maxWinningTurns = GetNextWinningTurn(gameField, gameFieldSize, playerNumber, searchOffset).Length;
                }

                gameField[point.X, point.Y] = 0;
            }

            return nextBallPosition;
        }

        public static void Main()
        {
            while (true)
            {
                int[,] gameField = new int[100, 100];

                int playerNumber = int.Parse(Console.ReadLine());
                int gameFieldSize = int.Parse(Console.ReadLine());
                int opponentNumber = playerNumber == 1 ? 2 : 1;

                for (int i = 1; i <= gameFieldSize; i++)
                {
                    for (int j = 1; j <= gameFieldSize; j++)
                    {
                        gameField[i, j] = int.Parse(Console.ReadLine());
                    }
                }

                Point? ballsNextPosition = null;

                if (GetNextWinningTurn(gameField, gameFieldSize, playerNumber, 4).Length > 0)
                    ballsNextPosition = GetNextWinningTurn(gameField, gameFieldSize, playerNumber, 4).First();

                if (!ballsNextPosition.HasValue && GetNextWinningTurn(gameField, gameFieldSize, opponentNumber, 4).Length > 0)
                    ballsNextPosition = GetNextWinningTurn(gameField, gameFieldSize, opponentNumber, 4).First();

                if (!ballsNextPosition.HasValue)
                    ballsNextPosition = GetNextTurnIfNotCanWin(gameField, gameFieldSize, opponentNumber, 3) ??
                                        GetNextTurnIfNotCanWin(gameField, gameFieldSize, playerNumber, 3) ??
                                        GetNextTurnIfNotCanWin(gameField, gameFieldSize, playerNumber, 2) ??
                                        GetNextTurnIfNotCanWin(gameField, gameFieldSize, opponentNumber, 2) ??
                                        GetNextTurnIfNotCanWin(gameField, gameFieldSize, playerNumber, 1) ??
                                        GetNextTurnIfNotCanWin(gameField, gameFieldSize, opponentNumber, 1) ??
                                        new Point(7, 7);

                Console.WriteLine("{0}:{1}", ballsNextPosition.Value.X, ballsNextPosition.Value.Y);
            }
        }
    }
}
