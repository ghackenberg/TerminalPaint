namespace TerminalPaint.Tools
{
    internal static class Fill
    {
        public static void Execute()
        {
            if (Image.data[Pointer.currentY * Image.width + Pointer.currentX] != Color.current)
            {
                ExecuteRecursive(Pointer.currentX, Pointer.currentY, Image.data[Pointer.currentY * Image.width + Pointer.currentX]);

                Console.BackgroundColor = Color.current;
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + Pointer.currentY);
                Console.Write('X');
            }
        }

        private static void ExecuteRecursive(int x, int y, ConsoleColor originalColor)
        {
            if (x >= 0 && x < Image.width && y >= 0 && y < Image.height && Image.data[y * Image.width + x] == originalColor)
            {
                Image.data[y * Image.width + x] = Color.current;

                Console.BackgroundColor = Color.current;
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(Util.BORDER_LEFT + x, Util.BORDER_TOP + y);
                Console.Write(' ');

                ExecuteRecursive(x + 1, y, originalColor);
                ExecuteRecursive(x - 1, y, originalColor);
                ExecuteRecursive(x, y + 1, originalColor);
                ExecuteRecursive(x, y - 1, originalColor);
            }
        }
    }
}