namespace TerminalPaint.Tools
{
    internal static class Pointer
    {
        /// <summary>
        /// The previous pointer x coorindate.
        /// </summary>
        public static int previousX = Image.width / 2;
        /// <summary>
        /// The previous pointer y coordinate.
        /// </summary>
        public static int previousY = Image.height / 2;

        /// <summary>
        /// The current pointer x coordinate.
        /// </summary>
        public static int currentX = Image.width / 2;
        /// <summary>
        /// The current pointer y coordinate.
        /// </summary>
        public static int currentY = Image.height / 2;

        /// <summary>
        /// Move the pointer by a defined delta.
        /// </summary>
        /// <param name="dx">The delta in x direction.</param>
        /// <param name="dy">The delta in y direction.</param>
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

        /// <summary>
        /// Update the color of the pixel at the current pointer location.
        /// </summary>
        public static void Brush()
        {
            // Update the image data

            Image.data[currentY * Image.width + currentX] = Color.current;

            // Update the screen pixel

            Move(0, 0);
        }
    }
}
