namespace TerminalPaint.Tools
{
    internal static class Rectangle
    {
        private static int startX;
        private static int startY;

        public static void Execute()
        {
            startX = Pointer.currentX;
            startY = Pointer.currentY;

            Util.PaintBorderTop();
            Util.PaintBorderBottom();

            Console.SetCursorPosition(1, 0);
            Console.Write("TerminalPaint | Pointer = Arrow Up/Down/Left/Right, Paint = Enter");

            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write("(c) 2025 Dr. Georg Hackenberg <georg.hackenberg@fh-wels.at> | Cancel = Escape");

            Console.BackgroundColor = Image.data[startY * Image.width + startX];
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(startX + Util.BORDER_LEFT, startY + Util.BORDER_TOP);
            Console.Write("X");

            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

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
                    Fill();
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

        private static void Move(int dx, int dy)
        {
            // Update current pointer location

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

            // Update rectangle bounds

            int minX = Math.Min(startX, Pointer.currentX);
            int maxX = Math.Max(startX, Pointer.currentX);

            int minY = Math.Min(startY, Pointer.currentY);
            int maxY = Math.Max(startY, Pointer.currentY);

            if (Pointer.previousX != Pointer.currentX)
            {
                if (Pointer.previousX < Pointer.currentX)
                {
                    if (Pointer.previousX != startX)
                    {
                        for (int y = minY; y <= maxY; y++)
                        {
                            Console.BackgroundColor = Image.data[y * Image.width + Pointer.previousX];

                            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.previousX, Util.BORDER_TOP + y);
                            Console.Write(Pointer.previousX > startX && (y == minY || y == maxY) ? '-' : ' ');
                        }
                    }

                    if (Pointer.currentX != startX)
                    {
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
                    if (Pointer.currentX != startX)
                    {
                        for (int y = minY; y <= maxY; y++)
                        {
                            Console.BackgroundColor = Image.data[y * Image.width + Pointer.currentX];

                            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + y);
                            Console.Write('|');
                        }
                    }

                    if (Pointer.previousX != startX)
                    {
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
                if (Pointer.previousY < Pointer.currentY)
                {
                    if (Pointer.previousY != startY)
                    {
                        Console.SetCursorPosition(Util.BORDER_LEFT + minX, Util.BORDER_TOP + Pointer.previousY);

                        for (int x = minX; x <= maxX; x++)
                        {
                            Console.BackgroundColor = Image.data[Pointer.previousY * Image.width + x];
                            Console.Write(Pointer.previousY > startY && (x == minX || x == maxX) ? '|' : ' ');
                        }
                    }
                    if (Pointer.currentY != startY)
                    {
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
                    if (Pointer.currentY != startY)
                    {
                        Console.SetCursorPosition(Util.BORDER_LEFT + minX, Util.BORDER_TOP + Pointer.currentY);

                        for (int x = minX; x <= maxX; x++)
                        {
                            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + x];
                            Console.Write('-');
                        }
                    }

                    if (Pointer.previousY != startY)
                    {
                        Console.SetCursorPosition(Util.BORDER_LEFT + minX, Util.BORDER_TOP + Pointer.previousY);

                        for (int x = minX; x <= maxX; x++)
                        {
                            Console.BackgroundColor = Image.data[Pointer.previousY * Image.width + x];
                            Console.Write(Pointer.previousY < startY && (x == minX || x == maxX) ? '|' : ' ');
                        }
                    }
                }
            }

            // Other corners

            Console.BackgroundColor = Image.data[startY * Image.width + Pointer.currentX];
            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + startY);
            Console.Write('+');

            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + startX];
            Console.SetCursorPosition(Util.BORDER_LEFT + startX, Util.BORDER_TOP + Pointer.currentY);
            Console.Write('+');

            // Start corner

            Console.BackgroundColor = Image.data[startY * Image.width + startX];
            Console.SetCursorPosition(Util.BORDER_LEFT + startX, Util.BORDER_TOP + startY);
            Console.Write('O');

            // Current pointer

            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + Pointer.currentX];
            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + Pointer.currentY);
            Console.Write('X');

            // Update previous pointer location

            Pointer.previousX = Pointer.currentX;
            Pointer.previousY = Pointer.currentY;
        }

        private static void Fill()
        {
            int minX = Math.Min(startX, Pointer.currentX);
            int maxX = Math.Max(startX, Pointer.currentX);

            int minY = Math.Min(startY, Pointer.currentY);
            int maxY = Math.Max(startY, Pointer.currentY);

            // Rectangle

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

            Util.PaintBorderTop();
            Util.PaintTextTop();

            Util.PaintBorderBottom();
            Util.PaintTextBottom();

            // Current pointer

            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + Pointer.currentX];
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + Pointer.currentY);
            Console.Write('X');
        }

        private static void Cancel()
        {
            int minX = Math.Min(startX, Pointer.currentX);
            int maxX = Math.Max(startX, Pointer.currentX);

            int minY = Math.Min(startY, Pointer.currentY);
            int maxY = Math.Max(startY, Pointer.currentY);

            // Top row

            Console.SetCursorPosition(Util.BORDER_LEFT + minX, Util.BORDER_TOP + minY);

            for (int x = minX; x <= maxX; x++)
            {
                Console.BackgroundColor = Image.data[minY * Image.width + x];
                Console.Write(' ');
            }

            // Intermediate rows

            for (int y = minY + 1; y <= maxY - 1; y++)
            {
                Console.BackgroundColor = Image.data[y * Image.width + minX];
                Console.SetCursorPosition(Util.BORDER_LEFT + minX, Util.BORDER_TOP + y);
                Console.Write(' ');

                Console.BackgroundColor = Image.data[y * Image.width + maxX];
                Console.SetCursorPosition(Util.BORDER_LEFT + maxX, Util.BORDER_TOP + y);
                Console.Write(' ');
            }

            // Bottom row

            Console.SetCursorPosition(Util.BORDER_LEFT + minX, Util.BORDER_TOP + maxY);

            for (int x = minX; x <= maxX; x++)
            {
                Console.BackgroundColor = Image.data[maxY * Image.width + x];
                Console.Write(' ');
            }

            Util.PaintBorderTop();
            Util.PaintTextTop();

            Util.PaintBorderBottom();
            Util.PaintTextBottom();

            // Current pointer

            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + Pointer.currentX];
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + Pointer.currentY);
            Console.Write('X');
        }
    }
}