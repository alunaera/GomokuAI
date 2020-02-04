using System;
using System.Drawing;

namespace GomokuAI
{
    class Program
    {
        public static Point GetNextTurnIfVerticalOpenFour(Point ballsNextPosition, int[,] gameField, int gameFieldSize,
            int playerNumber)
        {
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

                ballsNextPosition = GetNextTurnIfVerticalOpenFour(ballsNextPosition, map, n, player);

                //for (int j = 1; j <= n; j++)
                //{
                //    int comboPointCount = 0;
                //    for (int i = 1; i <= n; i++)
                //    {
                //        if (comboPointCount == 4 && i < 15 && map[i, j] == 0)
                //        {
                //            ballsNextPosition.X = i;
                //            ballsNextPosition.Y = j;
                //        }

                //        if (map[i, j] == player)
                //            comboPointCount++;
                //        else
                //            comboPointCount = 0;
                //    }
                //}

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
