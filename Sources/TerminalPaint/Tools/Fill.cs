namespace TerminalPaint.Tools
{
    /// <summary>
    /// Implements the image fill with color tool.
    /// </summary>
    internal static class Fill
    {
        /// <summary>
        /// Execute the fill operation.
        /// </summary>
        public static void Execute()
        {
            // Only execute, of the current color at the current pointer location is different from the target color
            // Note: Without this check the recursive implementation might end up in an endless loop.
            if (Image.data[Pointer.currentY * Image.width + Pointer.currentX] != Color.current)
            {
                // Fill the image using a recursive implementaetion

                ExecuteRecursive(Pointer.currentX, Pointer.currentY, Image.data[Pointer.currentY * Image.width + Pointer.currentX]);

                // Set the cursor to the current pointer location

                Console.BackgroundColor = Color.current;
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + Pointer.currentY);
                Console.Write('X');
            }
        }

        /// <summary>
        /// Recursive implementation of the fill operation.
        /// </summary>
        /// <param name="x">The current pixel x coordinate.</param>
        /// <param name="y">The current pixel y coordinate.</param>
        /// <param name="originalColor">The original color of the pixel, where recursion started.</param>
        private static void ExecuteRecursive(int x, int y, ConsoleColor originalColor)
        {
            // Check if image edges have been reached
            if (x >= 0 && x < Image.width && y >= 0 && y < Image.height)
            {
                // Check if current pixel color is equal to the color of the pixel, where recursion started
                // Note: This check prevents previously visited pixels from being processed again.
                if (Image.data[y * Image.width + x] == originalColor)
                {
                    // Update color of current pixel

                    Image.data[y * Image.width + x] = Color.current;

                    Console.BackgroundColor = Color.current;
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(Util.BORDER_LEFT + x, Util.BORDER_TOP + y);
                    Console.Write(' ');

                    // Process to top, left, right, and bottom pixels

                    ExecuteRecursive(x + 1, y, originalColor);
                    ExecuteRecursive(x - 1, y, originalColor);
                    ExecuteRecursive(x, y + 1, originalColor);
                    ExecuteRecursive(x, y - 1, originalColor);
                }
            }
        }
    }
}