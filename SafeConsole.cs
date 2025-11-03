using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjuvochpolisfinal
{
    internal static class SafeConsole
    {
        internal static void SetCursor(int x, int y)
        {
            try
            {
                if (x < 0 || y < 0) return;

                int maxX = Console.BufferWidth - 1;
                int maxY = Console.BufferHeight - 1;

                if (x > maxX || y > maxY) return;

                Console.SetCursorPosition(x, y);
            }
            catch
            {
                // Ignorera fel (sker vid resize av konsollen)
            }
        }

        internal static void WriteAt(int x, int y, string text)
        {
            SetCursor(x, y);
            Console.Write(text);
        }

        internal static void WriteChar(int x, int y, char c)
        {
            SetCursor(x, y);
            Console.Write(c);
        }
    }
}
