using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjuvochpolisfinal
{
    internal class Draw
    {
        // Rita upp staden
        static void DrawBorder(int width, int height)
        {
            Console.Clear();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (y == 0 || y == height - 1 || x == 0 || x == width - 1)
                        Console.Write("#");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(2, 0);
            Console.Write("City");

            Console.SetCursorPosition(0, 25);
            Console.Write("News Feed ============================");
        }
        static void DrawPrisonBorder(int prisonWidth, int prisonHeight, int startY, int startX)
        {
            for (int y = 0; y < prisonHeight; y++)
            {
                for (int x = 0; x < prisonWidth; x++)
                {
                    Console.SetCursorPosition(startX + x, y + startY);
                    if (y == 0 || y == prisonHeight - 1 || x == 0 || x == prisonWidth - 1)
                        Console.Write("#");
                    else
                        Console.Write(" ");
                }
            }

            Console.SetCursorPosition(startX, startY);
            Console.Write("Prison");
        }
    }
}
