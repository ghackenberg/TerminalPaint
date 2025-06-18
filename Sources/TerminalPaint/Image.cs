using TerminalPaint.Tools;

namespace TerminalPaint
{
    internal static class Image
    {
        public static int width = Console.WindowWidth - Util.BORDER_LEFT - Util.BORDER_RIGHT;
        public static int height = Console.WindowHeight - Util.BORDER_TOP - Util.BORDER_BOTTOM;

        public static int size = width * height;

        public static ConsoleColor[] data = new ConsoleColor[Image.size];

        public static void Paint()
        {
            for (int pixelX = 0; pixelX < Image.width; pixelX++)
            {
                for (int pixelY = 0; pixelY < Image.height; pixelY++)
                {
                    Console.BackgroundColor = Image.data[pixelY * Image.width + pixelX];
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(Util.BORDER_LEFT + pixelX, Util.BORDER_TOP + pixelY);
                    if (Pointer.currentX == pixelX && Pointer.currentY == pixelY)
                    {
                        Console.Write('X');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
            }

            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX + 1, Util.BORDER_TOP + Pointer.currentY);
        }
    }
}