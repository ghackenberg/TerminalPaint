namespace TerminalPaint.Tools
{
    /// <summary>
    /// Implementation of the line drawing tool.
    /// </summary>
    internal static class Line
    {
        /// <summary>
        /// The start x coordinate of the line.
        /// </summary>
        private static int startX;
        /// <summary>
        /// The start y coordinate of the line.
        /// </summary>
        private static int startY;

        /// <summary>
        /// Execute the line drawing operation.
        /// </summary>
        public static void Execute()
        {
            // Remember start coordinate of the line

            startX = Pointer.currentX;
            startY = Pointer.currentY;

            // Update border top and bottom of the application

            Util.PaintBorderTop();
            Util.PaintBorderBottom();

            Console.SetCursorPosition(1, 0);
            Console.Write("TerminalPaint | Pointer = Arrow Up/Down/Left/Right, Paint = Enter");

            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write("(c) 2025 Dr. Georg Hackenberg <georg.hackenberg@fh-wels.at> | Cancel = Escape");

            // Set the cursor to the current pointer location

            Console.BackgroundColor = Image.data[startY * Image.width + startX];
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(startX + Util.BORDER_LEFT, startY + Util.BORDER_TOP);
            Console.Write("X");

            // Read and process user input

            do
            {
                // Read next input key
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // Process input key
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    Move(-1, 0);
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    Move(+1, 0);
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    Move(0, -1);
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    Move(0, +1);
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Commit();
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Cancel();
                    break;
                }
            }
            while (true);
        }

        /// <summary>
        /// Move the line target by a defined delty in pixels and draw the line preview.
        /// </summary>
        /// <param name="dx">The target delta in x direction.</param>
        /// <param name="dy">The target detal in y direction.</param>
        private static void Move(int dx, int dy)
        {
            // Update current pointer location (= line target location)

            int newX = Pointer.currentX + dx;
            int newY = Pointer.currentY + dy;

            if (newX >= 0 && newX < Image.width)
            {
                Pointer.currentX = newX;
            }
            if (newY >= 0 && newY < Image.height)
            {
                Pointer.currentY = newY;
            }

            // Clear previous line preview and draw current line preview

            Draw(Pointer.previousX, Pointer.previousY, ' ');
            Draw(Pointer.currentX, Pointer.currentY, '+');

            // Draw line start location marker

            Console.BackgroundColor = Image.data[startY * Image.width + startX];
            Console.SetCursorPosition(Util.BORDER_LEFT + startX, Util.BORDER_TOP + startY);
            Console.Write('O');

            // Draw line target location marker

            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + Pointer.currentX];
            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + Pointer.currentY);
            Console.Write('X');

            // Update previous pointer location

            Pointer.previousX = Pointer.currentX;
            Pointer.previousY = Pointer.currentY;
        }

        /// <summary>
        /// Commit the line drawing operation, clear the line preview, and draw the line with the current color.
        /// </summary>
        private static void Commit()
        {
            // Paint line with current color

            Draw(Pointer.currentX, Pointer.currentY, ' ', Color.current);

            // Reset top and bottom borders

            Util.PaintBorderTop();
            Util.PaintTextTop();

            Util.PaintBorderBottom();
            Util.PaintTextBottom();

            // Set cursor to current pointer location

            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + Pointer.currentX];
            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + Pointer.currentY);
            Console.Write('X');
        }

        /// <summary>
        /// Cancel the line drawing operation and clear the line preview.
        /// </summary>
        private static void Cancel()
        {
            // Clear line preview

            Draw(Pointer.currentX, Pointer.currentX, ' ');

            // Reset top and bottom borders

            Util.PaintBorderTop();
            Util.PaintTextTop();

            Util.PaintBorderBottom();
            Util.PaintTextBottom();

            // Set cursor to current pointer location

            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + Pointer.currentX];
            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + Pointer.currentY);
            Console.Write('X');
        }

        /// <summary>
        /// Draw line or line preview from start location to end location.
        /// </summary>
        /// <param name="endX">End x coordinate.</param>
        /// <param name="endY">End y coordinate.</param>
        /// <param name="symbol">Symbol to paint.</param>
        /// <param name="color">Color to paint (or pixel color from image data, if null).</param>
        private static void Draw(int endX, int endY, char symbol, ConsoleColor? color = null)
        {
            // Compute line width and height in pixels

            int dX = endX - startX;
            int dY = endY - startY;

            // Compute number of line pixels

            int dM = Math.Max(Math.Abs(dX), Math.Abs(dY));

            // Process line pixel by pixel

            for (int i = 0; i <= dM; i++)
            {
                // Compute line pixel x and y coordinate

                int x = dM > 0 ? startX + (int)Math.Round(dX * i / (double)dM) : startX;
                int y = dM > 0 ? startY + (int)Math.Round(dY * i / (double)dM) : startY;

                // Update color (if requested)

                if (color != null)
                {
                    Image.data[y * Image.width + x] = color.Value;
                }

                // Update screen pixel

                Console.BackgroundColor = Image.data[y * Image.width + x];
                Console.SetCursorPosition(Util.BORDER_LEFT + x, Util.BORDER_TOP + y);
                Console.Write(symbol);
            }
        }
    }
}
