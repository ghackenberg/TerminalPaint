namespace TerminalPaint.Tools
{
    internal static class Color
    {
        public static void ChangeColor()
        {
            // Repaint previous color

            int previousColorX = Console.WindowWidth - 3;
            int previousColorY = 2 + Data.previousColor * 2;

            if (previousColorY < Console.WindowHeight - 1)
            {
                Console.BackgroundColor = Data.COLORS[Data.previousColor];
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(previousColorX, previousColorY);
                Console.Write(' ');
            }

            // Repaint current color

            int currentColorX = Console.WindowWidth - 3;
            int currentColorY = 2 + Data.currentColor * 2;

            if (currentColorY < Console.WindowHeight - 1)
            {
                Console.BackgroundColor = Data.COLORS[Data.currentColor];
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(currentColorX, currentColorY);
                Console.Write('X');
            }

            Data.previousColor = Data.currentColor;
        }
    }
}