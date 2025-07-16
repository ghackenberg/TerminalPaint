using TerminalPaint.Tools;

namespace TerminalPaint
{
    /// <summary>
    /// Encapsulates core image data and methods.
    /// </summary>
    internal static class Image
    {
        /// <summary>
        /// The width of the image in pixel.
        /// </summary>
        public static int width = Console.WindowWidth - Util.BORDER_LEFT - Util.BORDER_RIGHT;
        /// <summary>
        /// The height of the image in pixels.
        /// </summary>
        public static int height = Console.WindowHeight - Util.BORDER_TOP - Util.BORDER_BOTTOM;

        /// <summary>
        /// The size of the image in pixels.
        /// </summary>
        public static int size = width * height;

        /// <summary>
        /// The colors of the pixels in the image.
        /// </summary>
        public static ConsoleColor[] data = new ConsoleColor[Image.size];

        /// <summary>
        /// Paints the entire image to the console window.
        /// </summary>
        public static void Paint()
        {
            // Initialize foreground color
            Console.ForegroundColor = ConsoleColor.White;

            // Process row by row
            for (int pixelY = 0; pixelY < height; pixelY++)
            {
                // Set cursor to start of row
                Console.SetCursorPosition(Util.BORDER_LEFT, Util.BORDER_TOP + pixelY);

                // Process column by column
                for (int pixelX = 0; pixelX < width; pixelX++)
                {
                    // Set background color
                    Console.BackgroundColor = data[pixelY * width + pixelX];

                    // Write symbol to console
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

            // Set cursor position to pointer current location
            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX + 1, Util.BORDER_TOP + Pointer.currentY);
        }
    }
}