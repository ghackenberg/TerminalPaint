namespace TerminalPaint.Tools
{
    internal static class Line
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

            // Paint line

            Draw(Pointer.previousX, Pointer.previousY, ' ');
            Draw(Pointer.currentX, Pointer.currentY, '+');

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

        private static void Commit()
        {
            // Paint line

            Draw(Pointer.currentX, Pointer.currentY, ' ', Color.current);

            // Top and bottom borders

            Util.PaintBorderTop();
            Util.PaintTextTop();

            Util.PaintBorderBottom();
            Util.PaintTextBottom();

            // Current pointer

            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + Pointer.currentX];
            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + Pointer.currentY);
            Console.Write('X');
        }

        private static void Cancel()
        {
            // Reset line

            Draw(Pointer.currentX, Pointer.currentX, ' ');

            // Top and bottom borders

            Util.PaintBorderTop();
            Util.PaintTextTop();

            Util.PaintBorderBottom();
            Util.PaintTextBottom();

            // Current pointer

            Console.BackgroundColor = Image.data[Pointer.currentY * Image.width + Pointer.currentX];
            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX, Util.BORDER_TOP + Pointer.currentY);
            Console.Write('X');
        }

        private static void Draw(int endX, int endY, char symbol, ConsoleColor? color = null)
        {
            // Paint line

            int dX = endX - startX;
            int dY = endY - startY;

            int dM = Math.Max(Math.Abs(dX), Math.Abs(dY));

            for (int i = 0; i <= dM; i++)
            {
                int x = dM > 0 ? startX + (int)Math.Round(dX * i / (double)dM) : startX;
                int y = dM > 0 ? startY + (int)Math.Round(dY * i / (double)dM) : startY;

                if (color != null)
                {
                    Image.data[y * Image.width + x] = color.Value;
                }

                Console.BackgroundColor = Image.data[y * Image.width + x];
                Console.SetCursorPosition(Util.BORDER_LEFT + x, Util.BORDER_TOP + y);
                Console.Write(symbol);
            }
        }
    }
}
