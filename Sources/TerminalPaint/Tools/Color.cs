namespace TerminalPaint.Tools
{
    internal static class Color
    {
        private static readonly ConsoleColor[] COLORS =
            {
                ConsoleColor.Red,
                ConsoleColor.DarkRed,
                ConsoleColor.Green,
                ConsoleColor.DarkGreen,
                ConsoleColor.Blue,
                ConsoleColor.DarkBlue,
                ConsoleColor.Yellow,
                ConsoleColor.DarkYellow,
                ConsoleColor.Cyan,
                ConsoleColor.DarkCyan,
                ConsoleColor.Magenta,
                ConsoleColor.DarkMagenta,
                ConsoleColor.White,
                ConsoleColor.Gray,
                ConsoleColor.DarkGray,
                ConsoleColor.Black
            };

        private static int previousIndex = 0;
        private static int currentIndex = 0;

        public static ConsoleColor current = COLORS[currentIndex];

        public static void PaintPalette()
        {
            for (int color = 0; color < COLORS.Length; color++)
            {
                int colorX = Console.WindowWidth - 3;
                int colorY = 2 + color * 2;

                if (colorY < Console.WindowHeight - 1)
                {
                    Console.BackgroundColor = COLORS[color];
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(colorX, colorY);
                    if (color == currentIndex)
                    {
                        Console.Write('X');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
            }
        }

        public static void Change(int d)
        {
            int newColor = currentIndex + d;

            if (newColor >= 0 && newColor < COLORS.Length)
            {
                currentIndex = newColor;
                current = COLORS[newColor];
            }

            // Repaint previous color

            int previousColorX = Console.WindowWidth - 3;
            int previousColorY = 2 + previousIndex * 2;

            if (previousColorY < Console.WindowHeight - 1)
            {
                Console.BackgroundColor = COLORS[previousIndex];
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(previousColorX, previousColorY);
                Console.Write(' ');
            }

            // Repaint current color

            int currentColorX = Console.WindowWidth - 3;
            int currentColorY = 2 + currentIndex * 2;

            if (currentColorY < Console.WindowHeight - 1)
            {
                Console.BackgroundColor = COLORS[currentIndex];
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(currentColorX, currentColorY);
                Console.Write('X');
            }

            // Update previous color

            previousIndex = currentIndex;
        }
    }
}