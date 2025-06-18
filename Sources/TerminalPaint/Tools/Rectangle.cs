namespace TerminalPaint.Tools
{
    internal static class Rectangle
    {
        public static void DrawRectangle()
        {
            Util.PaintBorderTop();
            Util.PaintBorderBottom();

            Console.SetCursorPosition(1, 0);
            Console.Write("TerminalPaint | Pointer = Arrow Up/Down/Left/Right, Paint = Enter");

            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write("(c) 2025 Dr. Georg Hackenberg <georg.hackenberg@fh-wels.at> | Cancel = Escape");

            int startX = Data.currentPointerX;
            int startY = Data.currentPointerY;

            Console.BackgroundColor = Data.imageData[startY * Data.imageWidth + startX];
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(startX + Data.BORDER_LEFT, startY + Data.BORDER_TOP);
            Console.Write("X");

            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (Data.currentPointerX > 0)
                    {
                        Data.currentPointerX--;
                        MoveRectanglePointer(startX, startY);
                    }
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (Data.currentPointerX < Data.imageWidth - 1)
                    {
                        Data.currentPointerX++;
                        MoveRectanglePointer(startX, startY);
                    }
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (Data.currentPointerY > 0)
                    {
                        Data.currentPointerY--;
                        MoveRectanglePointer(startX, startY);
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (Data.currentPointerY < Data.imageHeight - 1)
                    {
                        Data.currentPointerY++;
                        MoveRectanglePointer(startX, startY);
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    int minX = Math.Min(startX, Data.currentPointerX);
                    int maxX = Math.Max(startX, Data.currentPointerX);

                    int minY = Math.Min(startY, Data.currentPointerY);
                    int maxY = Math.Max(startY, Data.currentPointerY);

                    // Rectangle

                    Console.BackgroundColor = Data.COLORS[Data.currentColor];

                    for (int y = minY; y <= maxY; y++)
                    {
                        Console.SetCursorPosition(Data.BORDER_LEFT + minX, Data.BORDER_TOP + y);

                        for (int x = minX; x <= maxX; x++)
                        {
                            Data.imageData[y * Data.imageWidth + x] = Data.COLORS[Data.currentColor];

                            Console.Write(' ');
                        }
                    }

                    Util.PaintBorderTop();
                    Util.PaintTextTop();

                    Util.PaintBorderBottom();
                    Util.PaintTextBottom();

                    // Current pointer

                    Console.BackgroundColor = Data.imageData[Data.currentPointerY * Data.imageWidth + Data.currentPointerX];
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(Data.BORDER_LEFT + Data.currentPointerX, Data.BORDER_TOP + Data.currentPointerY);
                    Console.Write('X');

                    return;
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    int minX = Math.Min(startX, Data.currentPointerX);
                    int maxX = Math.Max(startX, Data.currentPointerX);

                    int minY = Math.Min(startY, Data.currentPointerY);
                    int maxY = Math.Max(startY, Data.currentPointerY);

                    // Top row

                    Console.SetCursorPosition(Data.BORDER_LEFT + minX, Data.BORDER_TOP + minY);

                    for (int x = minX; x <= maxX; x++)
                    {
                        Console.BackgroundColor = Data.imageData[minY * Data.imageWidth + x];
                        Console.Write(' ');
                    }

                    // Intermediate rows

                    for (int y = minY + 1; y <= maxY - 1; y++)
                    {
                        Console.BackgroundColor = Data.imageData[y * Data.imageWidth + minX];
                        Console.SetCursorPosition(Data.BORDER_LEFT + minX, Data.BORDER_TOP + y);
                        Console.Write(' ');

                        Console.BackgroundColor = Data.imageData[y * Data.imageWidth + maxX];
                        Console.SetCursorPosition(Data.BORDER_LEFT + maxX, Data.BORDER_TOP + y);
                        Console.Write(' ');
                    }

                    // Bottom row

                    Console.SetCursorPosition(Data.BORDER_LEFT + minX, Data.BORDER_TOP + maxY);

                    for (int x = minX; x <= maxX; x++)
                    {
                        Console.BackgroundColor = Data.imageData[maxY * Data.imageWidth + x];
                        Console.Write(' ');
                    }

                    Util.PaintBorderTop();
                    Util.PaintTextTop();

                    Util.PaintBorderBottom();
                    Util.PaintTextBottom();

                    // Current pointer

                    Console.BackgroundColor = Data.imageData[Data.currentPointerY * Data.imageWidth + Data.currentPointerX];
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(Data.BORDER_LEFT + Data.currentPointerX, Data.BORDER_TOP + Data.currentPointerY);
                    Console.Write('X');

                    return;
                }
            }
            while (true);
        }

        private static void MoveRectanglePointer(int startX, int startY)
        {
            int minX = Math.Min(startX, Data.currentPointerX);
            int maxX = Math.Max(startX, Data.currentPointerX);

            int minY = Math.Min(startY, Data.currentPointerY);
            int maxY = Math.Max(startY, Data.currentPointerY);

            if (Data.previousPointerX != Data.currentPointerX)
            {
                if (Data.previousPointerX < Data.currentPointerX)
                {
                    if (Data.previousPointerX != startX)
                    {
                        for (int y = minY; y <= maxY; y++)
                        {
                            Console.BackgroundColor = Data.imageData[y * Data.imageWidth + Data.previousPointerX];

                            Console.SetCursorPosition(Data.BORDER_LEFT + Data.previousPointerX, Data.BORDER_TOP + y);
                            Console.Write(Data.previousPointerX > startX && (y == minY || y == maxY) ? '-' : ' ');
                        }
                    }

                    if (Data.currentPointerX != startX)
                    {
                        for (int y = minY; y <= maxY; y++)
                        {
                            Console.BackgroundColor = Data.imageData[y * Data.imageWidth + Data.currentPointerX];

                            Console.SetCursorPosition(Data.BORDER_LEFT + Data.currentPointerX, Data.BORDER_TOP + y);
                            Console.Write('|');
                        }
                    }
                }
                else
                {
                    if (Data.currentPointerX != startX)
                    {
                        for (int y = minY; y <= maxY; y++)
                        {
                            Console.BackgroundColor = Data.imageData[y * Data.imageWidth + Data.currentPointerX];

                            Console.SetCursorPosition(Data.BORDER_LEFT + Data.currentPointerX, Data.BORDER_TOP + y);
                            Console.Write('|');
                        }
                    }

                    if (Data.previousPointerX != startX)
                    {
                        for (int y = minY; y <= maxY; y++)
                        {
                            Console.SetCursorPosition(Data.BORDER_LEFT + Data.previousPointerX, Data.BORDER_TOP + y);

                            Console.BackgroundColor = Data.imageData[y * Data.imageWidth + Data.previousPointerX];
                            Console.Write(Data.previousPointerX < startX && (y == minY || y == maxY) ? '-' : ' ');
                        }
                    }
                }
            }

            if (Data.previousPointerY != Data.currentPointerY)
            {
                if (Data.previousPointerY < Data.currentPointerY)
                {
                    if (Data.previousPointerY != startY)
                    {
                        Console.SetCursorPosition(Data.BORDER_LEFT + minX, Data.BORDER_TOP + Data.previousPointerY);

                        for (int x = minX; x <= maxX; x++)
                        {
                            Console.BackgroundColor = Data.imageData[Data.previousPointerY * Data.imageWidth + x];
                            Console.Write(Data.previousPointerY > startY && (x == minX || x == maxX) ? '|' : ' ');
                        }
                    }
                    if (Data.currentPointerY != startY)
                    {
                        Console.SetCursorPosition(Data.BORDER_LEFT + minX, Data.BORDER_TOP + Data.currentPointerY);

                        for (int x = minX; x <= maxX; x++)
                        {
                            Console.BackgroundColor = Data.imageData[Data.currentPointerY * Data.imageWidth + x];
                            Console.Write('-');
                        }
                    }
                }
                else
                {
                    if (Data.currentPointerY != startY)
                    {
                        Console.SetCursorPosition(Data.BORDER_LEFT + minX, Data.BORDER_TOP + Data.currentPointerY);

                        for (int x = minX; x <= maxX; x++)
                        {
                            Console.BackgroundColor = Data.imageData[Data.currentPointerY * Data.imageWidth + x];
                            Console.Write('-');
                        }
                    }

                    if (Data.previousPointerY != startY)
                    {
                        Console.SetCursorPosition(Data.BORDER_LEFT + minX, Data.BORDER_TOP + Data.previousPointerY);

                        for (int x = minX; x <= maxX; x++)
                        {
                            Console.BackgroundColor = Data.imageData[Data.previousPointerY * Data.imageWidth + x];
                            Console.Write(Data.previousPointerY < startY && (x == minX || x == maxX) ? '|' : ' ');
                        }
                    }
                }
            }

            // Other corners

            Console.BackgroundColor = Data.imageData[startY * Data.imageWidth + Data.currentPointerX];
            Console.SetCursorPosition(Data.BORDER_LEFT + Data.currentPointerX, Data.BORDER_TOP + startY);
            Console.Write('+');

            Console.BackgroundColor = Data.imageData[Data.currentPointerY * Data.imageWidth + startX];
            Console.SetCursorPosition(Data.BORDER_LEFT + startX, Data.BORDER_TOP + Data.currentPointerY);
            Console.Write('+');

            // Start corner

            Console.BackgroundColor = Data.imageData[startY * Data.imageWidth + startX];
            Console.SetCursorPosition(Data.BORDER_LEFT + startX, Data.BORDER_TOP + startY);
            Console.Write('O');

            // Current pointer

            Console.BackgroundColor = Data.imageData[Data.currentPointerY * Data.imageWidth + Data.currentPointerX];
            Console.SetCursorPosition(Data.BORDER_LEFT + Data.currentPointerX, Data.BORDER_TOP + Data.currentPointerY);
            Console.Write('X');
            Data.previousPointerX = Data.currentPointerX;
            Data.previousPointerY = Data.currentPointerY;
        }
    }
}