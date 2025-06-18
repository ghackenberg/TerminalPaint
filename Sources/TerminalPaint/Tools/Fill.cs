namespace TerminalPaint.Tools
{
    internal static class Fill
    {
        public static void FillImage()
        {
            if (Data.imageData[Data.currentPointerY * Data.imageWidth + Data.currentPointerX] != Data.COLORS[Data.currentColor])
            {
                FillImageRecursive(Data.currentPointerX, Data.currentPointerY, Data.imageData[Data.currentPointerY * Data.imageWidth + Data.currentPointerX]);

                Console.BackgroundColor = Data.COLORS[Data.currentColor];
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(Data.BORDER_LEFT + Data.currentPointerX, Data.BORDER_TOP + Data.currentPointerY);
                Console.Write('X');
            }
        }

        private static void FillImageRecursive(int x, int y, ConsoleColor originalColor)
        {
            if (x >= 0 && x < Data.imageWidth && y >= 0 && y < Data.imageHeight && Data.imageData[y * Data.imageWidth + x] == originalColor)
            {
                Data.imageData[y * Data.imageWidth + x] = Data.COLORS[Data.currentColor];

                Console.BackgroundColor = Data.COLORS[Data.currentColor];
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(Data.BORDER_LEFT + x, Data.BORDER_TOP + y);
                Console.Write(' ');

                FillImageRecursive(x + 1, y, originalColor);
                FillImageRecursive(x - 1, y, originalColor);
                FillImageRecursive(x, y + 1, originalColor);
                FillImageRecursive(x, y - 1, originalColor);
            }
        }
    }
}