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
                int x = 2;
                int y = 1;
                int player, n;
                int[,] map = new int[100, 100];
                player = int.Parse(Console.ReadLine());
                n = int.Parse(Console.ReadLine());
                for (int i = 1; i <= n; ++i)
                    for (int j = 1; j <= n; ++j)
                    {
                        map[i, j] = int.Parse(Console.ReadLine());

                        if (map[i, j] == 0)
                        {
                            x = i;
                            y = j;
                        }
                        else
                        {
                            x = random.Next(1, 15);
                            y = random.Next(1, 15);
                        }
                    }

                Console.WriteLine("{0}:{1}",x,y);
            }
        }
    }
}
