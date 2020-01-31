using System;

namespace GomokuAI
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            while (true)
            {
                int x = -1;
                int y = -1;
                int player, n;

                int[,] map = new int[100, 100];

                player = int.Parse(Console.ReadLine());
                n = int.Parse(Console.ReadLine());

                for (int i = 1; i <= n; ++i)
                {
                    int comboPointCount = 0;
                    for (int j = 1; j <= n; ++j)
                    {
                        map[i, j] = int.Parse(Console.ReadLine());

                        if (comboPointCount == 4 && j < 15 && map[i, j] == 0)
                        {
                            x = i;
                            y = j;
                        }

                        if (map[i, j] == player)
                            comboPointCount++;
                        else
                            comboPointCount = 0;
                    }
                }

                for (int j = 1; j <= n; j++)
                {
                    int comboPointCount = 0;
                    for (int i = 1; i <= n; i++)
                    {
                        if (comboPointCount == 4 && i < 15 && map[i, j] == 0)
                        {
                            x = i;
                            y = j;
                        }

                        if (map[i, j] == player)
                            comboPointCount++;
                        else
                            comboPointCount = 0;
                    }
                }

                if (x == -1 || y == -1)
                {
                    do
                    {
                        x = random.Next(4, 11);
                        y = random.Next(4, 11);
                    } 
                    while (map[x, y] != 0);
                }


                Console.WriteLine("{0}:{1}", x, y);
            }
        }
    }
}
