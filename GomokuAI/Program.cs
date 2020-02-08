using System;
using System.Drawing;

namespace GomokuAI
{
    class Program
    {
        public static Point GetNextWinningTurn(int[,] gameField, int gameFieldSize, int playerNumber)
        {
            Point ballsNextPosition = Point.Empty;
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

                    for (int offset = -4; offset <= 4; offset++)
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

                        if (horizontalComboPoint == 4 || verticalComboPoint == 4 ||
                            leftDiagonalComboPoint == 4 || rightDiagonalComboPoint == 4)
                        {
                            ballsNextPosition.X = i;
                            ballsNextPosition.Y = j;

                            break;
                        }
                    }
                }
            }

            return ballsNextPosition;
        }

        public static void Main()
        {
            Random random = new Random();

            while (true)
            {
                Point ballsNextPosition = Point.Empty;
                int player, n;

                int[,] map = new int[100, 100];

                player = int.Parse(Console.ReadLine());
                n = int.Parse(Console.ReadLine());

                for (int i = 1; i <= n; ++i)
                {
                    for (int j = 1; j <= n; ++j)
                    {
                        map[i, j] = int.Parse(Console.ReadLine());
                    }
                }

                ballsNextPosition = GetNextWinningTurn(map, n, player);

                if (ballsNextPosition == Point.Empty)
                {
                    do
                    {
                        ballsNextPosition.X = random.Next(4, 11);
                        ballsNextPosition.Y = random.Next(4, 11);
                    } while (map[ballsNextPosition.X, ballsNextPosition.Y] != 0);
                }

                Console.WriteLine("{0}:{1}", ballsNextPosition.X, ballsNextPosition.Y);
            }
        }
    }
}
