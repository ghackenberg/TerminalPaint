namespace TerminalPaint.Tools
{
    /// <summary>
    /// Implementation of the rectangle drawing tool.
    /// </summary>
    internal static class Rectangle
    {
        /// <summary>
        /// The x coordinate, where rectangle drawing started.
        /// </summary>
        private static int startX;
        /// <summary>
        /// The y coordinate, where rectangle drawing started.
        /// </summary>
        private static int startY;

        /// <summary>
        /// Execute the rectangle drawing tool.
        /// </summary>
        public static void Execute()
        {
            // Remember, where rectangle drawing started

            startX = Pointer.currentX;
            startY = Pointer.currentY;

            // Update top and border borders of the application

            Util.PaintBorderTop();
            Util.PaintBorderBottom();

            Console.SetCursorPosition(1, 0);
            Console.Write("TerminalPaint | Pointer = Arrow Up/Down/Left/Right, Paint = Enter");

            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write("(c) 2025 Dr. Georg Hackenberg <georg.hackenberg@fh-wels.at> | Cancel = Escape");

            // Set cursor to current pointer location

            Console.BackgroundColor = Image.data[startY * Image.width + startX];
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(startX + Util.BORDER_LEFT, startY + Util.BORDER_TOP);
            Console.Write("X");

            // Read and process user inputs

            do
            {
                // Read next user input
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // Process user input
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
        /// Move the rectangle target location by a delta and update rectangle preview.
        /// </summary>
        /// <param name="dx">The delta in x direction.</param>
        /// <param name="dy">The delta in y direction.</param>
        private static void Move(int dx, int dy)
        {
            // Update current pointer location (= rectangle target location)

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

            // Calculate rectangle bounds

            int minX = Math.Min(startX, Pointer.currentX);
            int maxX = Math.Max(startX, Pointer.currentX);

            int minY = Math.Min(startY, Pointer.currentY);
            int maxY = Math.Max(startY, Pointer.currentY);

            // Update rectangle preview

            if (Pointer.previousX != Pointer.currentX)
            {
                // Case 1: Rectangle target has been moved left or right!

                if (Pointer.previousX < Pointer.currentX)
                {
                    // Case 1.1: Rectangle target has been moved left

                    if (Pointer.previousX != startX)
                    {
                        // Clear the vertical preview line at the previous target location

                        for (int y = minY; y <= maxY; y++)
                        {
                            Console.BackgroundColor = Image.data[y * Image.width + Pointer.previousX];

                            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.previousX, Util.BORDER_TOP + y);
                            Console.Write(Pointer.previousX > startX && (y == minY || y == maxY) ? '-' : ' ');
                        }
                    }

                    if (Pointer.currentX != startX)
                    {
                        // Paint the vertical preview line at the current target location

                        for (int y = minY; y <= maxY; y++)
                        {
                            Console.BackgroundColor = Image.data[y * Image.width + Pointer.currentX];

                            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + y);
                            Console.Write('|');
                        }
                    }
                }
                else
                {
                    // Case 1.2: Rectangle target has been moved right

                    if (Pointer.currentX != startX)
                    {
                        // Paint the vertical preview line at the current target location

                        for (int y = minY; y <= maxY; y++)
                        {
                            Console.BackgroundColor = Image.data[y * Image.width + Pointer.currentX];

                            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + y);
                            Console.Write('|');
                        }
                    }

                    if (Pointer.previousX != startX)
                    {
                        // Clear the vertical preview line at the previous target location

                        for (int y = minY; y <= maxY; y++)
                        {
                            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.previousX, Util.BORDER_TOP + y);

                            Console.BackgroundColor = Image.data[y * Image.width + Pointer.previousX];
                            Console.Write(Pointer.previousX < startX && (y == minY || y == maxY) ? '-' : ' ');
                        }
                    }
                }
            }

            if (Pointer.previousY != Pointer.currentY)
            {
                // Case 2: Rectangle target has been moved up or down

                if (Pointer.previousY < Pointer.currentY)
                {
                    // Case 2.1: Rectangle target has been moved down

                    if (Pointer.previousY != startY)
                    {
                        // Clear the horizontal preview line at the previous target location

                        Console.SetCursorPosition(Util.BORDER_LEFT + minX, Util.BORDER_TOP + Pointer.previousY);

                        for (int x = minX; x <= maxX; x++)
                        {
                            Console.BackgroundColor = Image.data[Pointer.previousY * Image.width + x];
                            Console.Write(Pointer.previousY > startY && (x == minX || x == maxX) ? '|' : ' ');
                        }
                    }
                    if (Pointer.currentY != startY)
                    {
                        // Paint the horizontal preview line at the current target location

                        Console.SetCursorPosition(Util.BORDER_LEFT + minX, Util.BORDER_TOP + Pointer.currentY);

                        for (int x = minX; x <= maxX; x++)
                        {
                            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + x];
                            Console.Write('-');
                        }
                    }
                }
                else
                {
                    // Case 2.2.: Rectangle target has been moved up

                    if (Pointer.currentY != startY)
                    {
                        // Paint the horizontal preview line at the current target location

                        Console.SetCursorPosition(Util.BORDER_LEFT + minX, Util.BORDER_TOP + Pointer.currentY);

                        for (int x = minX; x <= maxX; x++)
                        {
                            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + x];
                            Console.Write('-');
                        }
                    }

                    if (Pointer.previousY != startY)
                    {
                        // Clear the horizontal preview line at the previous target location

                        Console.SetCursorPosition(Util.BORDER_LEFT + minX, Util.BORDER_TOP + Pointer.previousY);

                        for (int x = minX; x <= maxX; x++)
                        {
                            Console.BackgroundColor = Image.data[Pointer.previousY * Image.width + x];
                            Console.Write(Pointer.previousY < startY && (x == minX || x == maxX) ? '|' : ' ');
                        }
                    }
                }
            }

            // Draw other corder markers

            Console.BackgroundColor = Image.data[startY * Image.width + Pointer.currentX];
            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + startY);
            Console.Write('+');

            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + startX];
            Console.SetCursorPosition(Util.BORDER_LEFT + startX, Util.BORDER_TOP + Pointer.currentY);
            Console.Write('+');

            // Draw start corner marker

            Console.BackgroundColor = Image.data[startY * Image.width + startX];
            Console.SetCursorPosition(Util.BORDER_LEFT + startX, Util.BORDER_TOP + startY);
            Console.Write('O');

            // Draw target corner marker (= current pointer location marker)

            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + Pointer.currentX];
            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + Pointer.currentY);
            Console.Write('X');

            // Update previous pointer location

            Pointer.previousX = Pointer.currentX;
            Pointer.previousY = Pointer.currentY;
        }

        /// <summary>
        /// Commit the rectangle drawing operation, clear the rectangle preview, and draw the rectangle with the current color.
        /// </summary>
        private static void Commit()
        {
            // Calculate rectangle bounds

            int minX = Math.Min(startX, Pointer.currentX);
            int maxX = Math.Max(startX, Pointer.currentX);

            int minY = Math.Min(startY, Pointer.currentY);
            int maxY = Math.Max(startY, Pointer.currentY);

            // Paint rectangle with current color

            Console.BackgroundColor = Color.current;

            for (int y = minY; y <= maxY; y++)
            {
                Console.SetCursorPosition(Util.BORDER_LEFT + minX, Util.BORDER_TOP + y);

                for (int x = minX; x <= maxX; x++)
                {
                    Image.data[y * Image.width + x] = Color.current;

                    Console.Write(' ');
                }
            }

            // Reset top and bottom borders of the application

            Util.PaintBorderTop();
            Util.PaintTextTop();

            Util.PaintBorderBottom();
            Util.PaintTextBottom();

            // Set cursor to current pointer location

            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + Pointer.currentX];
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + Pointer.currentY);
            Console.Write('X');
        }

        /// <summary>
        /// Cancel the rectangle drawing operation and clear the rectangle preview.
        /// </summary>
        private static void Cancel()
        {
            // Calculate rectangle bounds

            int minX = Math.Min(startX, Pointer.currentX);
            int maxX = Math.Max(startX, Pointer.currentX);

            int minY = Math.Min(startY, Pointer.currentY);
            int maxY = Math.Max(startY, Pointer.currentY);

            // Clear top horizonal line preview

            Console.SetCursorPosition(Util.BORDER_LEFT + minX, Util.BORDER_TOP + minY);

            for (int x = minX; x <= maxX; x++)
            {
                Console.BackgroundColor = Image.data[minY * Image.width + x];
                Console.Write(' ');
            }

            // Clear left and right vertical line previews

            for (int y = minY + 1; y <= maxY - 1; y++)
            {
                Console.BackgroundColor = Image.data[y * Image.width + minX];
                Console.SetCursorPosition(Util.BORDER_LEFT + minX, Util.BORDER_TOP + y);
                Console.Write(' ');

                Console.BackgroundColor = Image.data[y * Image.width + maxX];
                Console.SetCursorPosition(Util.BORDER_LEFT + maxX, Util.BORDER_TOP + y);
                Console.Write(' ');
            }

            // Clear bottom horizontal line preview

            Console.SetCursorPosition(Util.BORDER_LEFT + minX, Util.BORDER_TOP + maxY);

            for (int x = minX; x <= maxX; x++)
            {
                Console.BackgroundColor = Image.data[maxY * Image.width + x];
                Console.Write(' ');
            }

            // Reset top and bottom borders of the application

            Util.PaintBorderTop();
            Util.PaintTextTop();

            Util.PaintBorderBottom();
            Util.PaintTextBottom();

            // Set cursor to current pointer location

            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + Pointer.currentX];
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + Pointer.currentY);
            Console.Write('X');
        }
    }
}