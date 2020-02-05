using System;
using System.Drawing;

namespace GomokuAI
{
    class Program
    {
        public static Point GetNextTurnIfVerticalOpenFour(int[,] gameField, int gameFieldSize, int playerNumber)
        {
            Point ballsNextPosition = Point.Empty;

            for (int i = 1; i <= gameFieldSize; ++i)
            {
                int comboPointCount = 0;
                for (int j = 1; j <= gameFieldSize; ++j)
                {
                    if (gameField[i, j] == playerNumber)
                    {
                        comboPointCount++;

                        if (comboPointCount == 4)
                        {
                            if (j - 4 > 0 && gameField[i, j - 4] == 0)
                            {
                                ballsNextPosition.X = i;
                                ballsNextPosition.Y = j - 4;
                            }

                            if (j + 1 <= gameFieldSize && gameField[i, j + 1] == 0)
                            {
                                ballsNextPosition.X = i;
                                ballsNextPosition.Y = j + 1;
                            }
                        }
                    }
                    else
                        comboPointCount = 0;
                }
            }

            return ballsNextPosition;
        }

        public static Point GetNextTurnIfHorizontalOpenFour(int[,] gameField, int gameFieldSize, int playerNumber)
        {
            Point ballsNextPosition = Point.Empty;

            for (int j = 1; j <= gameFieldSize; ++j)
            {
                int comboPointCount = 0;
                for (int i = 1; i <= gameFieldSize; ++i)
                {
                    if (gameField[i, j] == playerNumber)
                    {
                        comboPointCount++;

                        if (comboPointCount == 4)
                        {
                            if (i - 4 > 0 && gameField[i - 4, j] == 0)
                            {
                                ballsNextPosition.X = i - 4;
                                ballsNextPosition.Y = j;
                            }

                            if (i + 1 <= gameFieldSize && gameField[i + 1, j] == 0)
                            {
                                ballsNextPosition.X = i + 1;
                                ballsNextPosition.Y = j;
                            }
                        }
                    }
                    else
                        comboPointCount = 0;
                }
            }

            return ballsNextPosition;
        }

        public static Point GetNextTurnIfLeftDiagonalOpenFour(int[,] gameField, int gameFieldSize, int playerNumber)
        {
            Point ballsNextPosition = Point.Empty;

            for (int diagonalOffset = 1 - gameFieldSize; diagonalOffset < gameFieldSize; diagonalOffset++)
            {
                int comboPointCount = 0;

                for (int i = 1; i <= gameFieldSize - Math.Abs(diagonalOffset); i++)
                {
                    if (diagonalOffset >= 0)
                    {
                        if (gameField[i + diagonalOffset, i] == playerNumber)
                        {
                            comboPointCount++;

                            if (comboPointCount == 4)
                            {
                                if (i - 4 > 0 && gameField[i + diagonalOffset - 4, i - 4] == 0)
                                {
                                    ballsNextPosition.X = i + diagonalOffset - 4;
                                    ballsNextPosition.Y = i - 4;
                                }

                                if (i + diagonalOffset + 1 <= gameFieldSize && gameField[i + diagonalOffset + 1, i + 1] == 0)
                                {
                                    ballsNextPosition.X = i + diagonalOffset + 1;
                                    ballsNextPosition.Y = i + 1;
                                }
                            }
                        }
                        else
                            comboPointCount = 0;
                    }
                    else
                    {
                        if (gameField[i, i + Math.Abs(diagonalOffset)] == playerNumber)
                        {
                            comboPointCount++;

                            if (comboPointCount == 4)
                            {
                                if (i - 4 > 0 && gameField[i - 4, i + Math.Abs(diagonalOffset) - 4] == 0)
                                {
                                    ballsNextPosition.X = i - 4;
                                    ballsNextPosition.Y = i + Math.Abs(diagonalOffset) - 4;
                                }

                                if (i + diagonalOffset + 1 <= gameFieldSize && gameField[i + 1, i + Math.Abs(diagonalOffset) + 1] == 0)
                                {
                                    ballsNextPosition.X = i  + 1;
                                    ballsNextPosition.Y = i + Math.Abs(diagonalOffset) + 1;
                                }
                            }
                        }
                        else
                            comboPointCount = 0;
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

                ballsNextPosition = GetNextTurnIfVerticalOpenFour(map, n, player);

                if (ballsNextPosition == Point.Empty)
                    ballsNextPosition = GetNextTurnIfHorizontalOpenFour(map, n, player);

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
