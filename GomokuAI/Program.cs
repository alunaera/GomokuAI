using System;
using System.Drawing;

namespace GomokuAI
{
    class Program
    {
        public static Point? GetNextWinningTurn(int[,] gameField, int gameFieldSize, int playerNumber, int searchOffset)
        {
            Point? ballsNextPosition = null;
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

                        ballsNextPosition = new Point(i, j);

                        break;
                    }
                }
            }

            return ballsNextPosition;
        }

        private static Point? GetNextTurnIfNotCanWin(int[,] gameField, int gameFieldSize, int playerNumber,
            int searchOffset)
        {
            Point? ballsNextPosition = null;

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

            for (int i = minY; i <= maxY; i++)
                for (int j = minX; j <= maxX; j++)
                {
                    int[,] supposedGameField = CopyArray(gameField, gameFieldSize);

                    if (gameField[i, j] == 0)
                        supposedGameField[i, j] = playerNumber;
                    else
                        continue;

                    if (!GetNextWinningTurn(supposedGameField, gameFieldSize, playerNumber, searchOffset).HasValue)
                        continue;

                    ballsNextPosition =
                        GetNextWinningTurn(supposedGameField, gameFieldSize, playerNumber, searchOffset);

                    supposedGameField[ballsNextPosition.Value.X, ballsNextPosition.Value.Y] = playerNumber == 1 
                        ? 2
                        : 1;

                    if (!GetNextWinningTurn(supposedGameField, gameFieldSize, playerNumber, searchOffset).HasValue)
                        continue;

                    i = gameFieldSize + 1;
                    j = gameFieldSize + 1;

                }

            return ballsNextPosition;

            int[,] CopyArray(int[,] sourceArray, int sourceArraySize)
            {
                int[,] destinationArray = new int[100, 100];

                for (int i = 1; i <= sourceArraySize; i++)
                    for (int j = 1; j <= sourceArraySize; j++)
                        destinationArray[i, j] = sourceArray[i, j];

                return destinationArray;
            }
        }

        public static void Main()
        {
            Random random = new Random();

            while (true)
            {
                int playerNumber, opponentNumber, gameFieldSize;

                int[,] gameField = new int[100, 100];

                playerNumber = int.Parse(Console.ReadLine());
                gameFieldSize = int.Parse(Console.ReadLine());

                opponentNumber = playerNumber == 1 ? 2 : 1;


                for (int i = 1; i <= gameFieldSize; i++)
                {
                    for (int j = 1; j <= gameFieldSize; j++)
                    {
                        gameField[i, j] = int.Parse(Console.ReadLine());
                    }
                }

                Point? ballsNextPosition = GetNextWinningTurn(gameField, gameFieldSize, playerNumber, 4) ??
                                           GetNextWinningTurn(gameField, gameFieldSize, opponentNumber, 4) ??
                                           GetNextTurnIfNotCanWin(gameField, gameFieldSize, playerNumber, 3) ??
                                           GetNextTurnIfNotCanWin(gameField, gameFieldSize, opponentNumber, 3) ??
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
