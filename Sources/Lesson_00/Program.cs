namespace Lesson_00
{
    internal class Program
    {
        // FIELDS

        static readonly int imageOffsetX = 1;
        static readonly int imageOffsetY = 1;

        static readonly int imageWidth = Console.WindowWidth - 2;
        static readonly int imageHeight = Console.WindowHeight - 2;

        static int pointerX = imageWidth / 2;
        static int pointerY = imageHeight / 2;

        // METHODS

        static void Main(string[] args)
        {
            PaintFrame();
        }

        // - PHASES

        static void PaintFrame()
        {
            ClearScreen();
            PaintBorders();
            UpdateImagePixel(pointerX, pointerY);
        }

        // - HELPERS

        static void ClearScreen()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            Console.ReadKey(true);
        }

        static void PaintBorders()
        {
            PaintHorizontalBorder(0);
            PaintHorizontalBorder(Console.WindowHeight - 1);

            PaintVerticalBorder(0);
            PaintVerticalBorder(Console.WindowWidth - 1);
        }

        static void PaintHorizontalBorder(int row)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(0, row);

            for (int column = 0; column < Console.WindowWidth; column++)
            {
                Console.Write(' ');
            }

            Console.ReadKey(true);
        }

        static void PaintVerticalBorder(int column)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int row = 0; row < Console.WindowHeight; row++)
            {
                Console.SetCursorPosition(column, row);
                Console.Write(' ');
            }

            Console.ReadKey(true);
        }

        static void UpdateImagePixel(int x, int y)
        {
            Console.BackgroundColor = GetImagePixelBackgroundColor(x, y);
            Console.ForegroundColor = GetImagePixelForegroundColor(x, y);

            char symbol = GetImagePixelSymbol(x, y);

            Console.SetCursorPosition(imageOffsetX + x, imageOffsetY + y);
            Console.Write(symbol);

            Console.ReadKey(true);
        }

        static ConsoleColor GetImagePixelBackgroundColor(int x, int y)
        {
            return ConsoleColor.Black;
        }

        static ConsoleColor GetImagePixelForegroundColor(int x, int y)
        {
            return ConsoleColor.White;
        }

        static char GetImagePixelSymbol(int x, int y)
        {
            if (x == pointerX && y == pointerY)
            {
                return 'X';
            }
            else
            {
                return ' ';
            }
        }
    }
}
