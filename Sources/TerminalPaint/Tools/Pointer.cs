namespace TerminalPaint.Tools
{
    internal static class Pointer
    {
        public static int previousX = Image.width / 2;
        public static int previousY = Image.height / 2;

        public static int currentX = Image.width / 2;
        public static int currentY = Image.height / 2;

        public static void Move(int dx, int dy)
        {
            // Update current pointer location

            int newX = currentX + dx;
            int newY = currentY + dy;

            if (newX >= 0 && newX < Image.width)
            {
                currentX = newX;
            }
            if (newY >= 0 && newY < Image.height)
            {
                currentY = newY;
            }

            // Repaint previous pointer location

            Console.BackgroundColor = Image.data[previousY * Image.width + previousX];
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(Util.BORDER_LEFT + previousX, Util.BORDER_TOP + previousY);
            Console.Write(' ');

            // Repaint current pointer location

            Console.BackgroundColor = Image.data[currentY * Image.width + currentX];
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(Util.BORDER_LEFT + currentX, Util.BORDER_TOP + currentY);
            Console.Write('X');

            // Update previous pointer location

            previousX = currentX;
            previousY = currentY;
        }

        public static void Brush()
        {
            Image.data[currentY * Image.width + currentX] = Color.current;

            Move(0, 0);
        }
    }
}
