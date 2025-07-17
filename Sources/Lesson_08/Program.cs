namespace Lesson_08
{
    internal class Program
    {
        // FIELDS

        static readonly ConsoleColor[] colors =
        {
            ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue
        };

        static int currentColorIndex = 0;

        static readonly int imageOffsetX = 1;
        static readonly int imageOffsetY = 1;

        static readonly int imageWidth = Console.WindowWidth - 4;
        static readonly int imageHeight = Console.WindowHeight - 2;

        static readonly int imageSize = imageWidth * imageHeight;

        static readonly ConsoleColor[] imageData = new ConsoleColor[imageSize];

        static int pointerX = imageWidth / 2;
        static int pointerY = imageHeight / 2;

        static int rectangleStartX = -1;
        static int rectangleStartY = -1;

        static int rectangleMinX = -1;
        static int rectangleMinY = -1;

        static int rectangleMaxX = -1;
        static int rectangleMaxY = -1;

        static int lineStartX = -1;
        static int lineStartY = -1;

        // METHODS

        static void Main(string[] args)
        {
            InitializeImage();
            InitializeInterface();
            MainLoop();
            SayGoodbye();
        }

        // - LOOPS

        static void MainLoop() // revised in this lesson!
        {
            while (true)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                if (input.Key == ConsoleKey.UpArrow)
                {
                    MovePointer(0, -1);
                }
                else if (input.Key == ConsoleKey.DownArrow)
                {
                    MovePointer(0, +1);
                }
                else if (input.Key == ConsoleKey.LeftArrow)
                {
                    MovePointer(-1, 0);
                }
                else if (input.Key == ConsoleKey.RightArrow)
                {
                    MovePointer(+1, 0);
                }
                else if (input.Key == ConsoleKey.Spacebar)
                {
                    Stroke();
                }
                else if (input.Key == ConsoleKey.PageUp)
                {
                    ChangeColor(-1);
                }
                else if (input.Key == ConsoleKey.PageDown)
                {
                    ChangeColor(+1);
                }
                else if (input.Key == ConsoleKey.C)
                {
                    Clear();
                }
                else if (input.Key == ConsoleKey.F)
                {
                    Fill();
                }
                else if (input.Key == ConsoleKey.R)
                {
                    RectangleLoop();
                }
                else if (input.Key == ConsoleKey.L)
                {
                    LineLoop();
                }
                else if (input.Key == ConsoleKey.S) // added in this lesson!
                {
                    SaveLoop();
                }
                else if (input.Key == ConsoleKey.O) // added in this lesson!
                {
                    OpenLoop();
                }
                else if (input.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        static void RectangleLoop()
        {
            // Remember rectangle start location

            rectangleStartX = pointerX;
            rectangleStartY = pointerY;

            // Initialize rectangle min/max

            rectangleMinX = pointerX;
            rectangleMinY = pointerY;

            rectangleMaxX = pointerX;
            rectangleMaxY = pointerY;

            // Enter rectangle loop

            while (true)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                if (input.Key == ConsoleKey.UpArrow)
                {
                    MoveRectanglePointer(0, -1);
                }
                else if (input.Key == ConsoleKey.DownArrow)
                {
                    MoveRectanglePointer(0, +1);
                }
                else if (input.Key == ConsoleKey.LeftArrow)
                {
                    MoveRectanglePointer(-1, 0);
                }
                else if (input.Key == ConsoleKey.RightArrow)
                {
                    MoveRectanglePointer(+1, 0);
                }
                else if (input.Key == ConsoleKey.Enter)
                {
                    CommitRectangle();
                    break;
                }
                else if (input.Key == ConsoleKey.Escape)
                {
                    CancelRectangle();
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        static void LineLoop()
        {
            // Remember line start location

            lineStartX = pointerX;
            lineStartY = pointerY;

            // Enter line loop

            while (true)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                if (input.Key == ConsoleKey.UpArrow)
                {
                    MoveLinePointer(0, -1);
                }
                else if (input.Key == ConsoleKey.DownArrow)
                {
                    MoveLinePointer(0, +1);
                }
                else if (input.Key == ConsoleKey.LeftArrow)
                {
                    MoveLinePointer(-1, 0);
                }
                else if (input.Key == ConsoleKey.RightArrow)
                {
                    MoveLinePointer(+1, 0);
                }
                else if (input.Key == ConsoleKey.Enter)
                {
                    CommitLine();
                    break;
                }
                else if (input.Key == ConsoleKey.Escape)
                {
                    CancelLine();
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        static void SaveLoop() // added in this lesson!
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();
            Console.Write("Please enter file name (press ENTER to cancel): ");

            while (true)
            {
                string? fileName = Console.ReadLine();

                if (fileName != null && fileName.Length > 0)
                {
                    try
                    {
                        FileStream stream = new FileStream(fileName, FileMode.Create);

                        for (int y = 0; y < imageHeight; y++)
                        {
                            for (int x = 0; x < imageWidth; x++)
                            {
                                ConsoleColor color = GetImagePixelBackgroundColor(x, y);

                                stream.WriteByte((byte)color);
                            }
                        }

                        stream.Close();

                        break;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.Clear();
                        Console.Write("Failed to write file. Please enter file name (press ENTER to cancel): ");
                    }
                }
                else
                {
                    break;
                }
            }

            // Restore main view

            InitializeInterface();

            SetCursorPosition();
        }

        static void OpenLoop() // added in this lesson!
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();
            Console.Write("Please enter file name (press ENTER to cancel): ");

            while (true)
            {
                string? fileName = Console.ReadLine();

                if (fileName != null && fileName.Length > 0)
                {
                    try
                    {
                        FileStream stream = new FileStream(fileName, FileMode.Open);

                        for (int y = 0; y < imageHeight; y++)
                        {
                            for (int x = 0; x < imageWidth; x++)
                            {
                                ConsoleColor color = (ConsoleColor)stream.ReadByte();

                                SetImagePixelBackgroundColor(x, y, color);
                            }
                        }

                        stream.Close();

                        break;
                    }
                    catch (FileNotFoundException)
                    {
                        Console.Clear();
                        Console.Write("Failed to open file. Please enter file name (press ENTER to cancel): ");
                    }
                }
                else
                {
                    break;
                }
            }

            // Restore main view

            InitializeInterface();

            SetCursorPosition();
        }

        // - TOOLS

        static void MovePointer(int dx, int dy)
        {
            // Remember previous pointer location

            int previousX = pointerX;
            int previousY = pointerY;

            // Update current pointer location

            if (pointerX + dx >= 0 && pointerX + dx < imageWidth)
            {
                pointerX += dx;
            }
            if (pointerY + dy >= 0 && pointerY + dy < imageHeight)
            {
                pointerY += dy;
            }

            // Update previous pointer location pixel

            if (previousX != pointerX || previousY != pointerY)
            {
                UpdateImagePixel(previousX, previousY);
            }

            // Update current pointer location pixel

            UpdateImagePixel(pointerX, pointerY);
        }

        static void Stroke()
        {
            ConsoleColor color = colors[currentColorIndex];

            SetImagePixelBackgroundColor(pointerX, pointerY, color);

            UpdateImagePixel(pointerX, pointerY);
        }

        static void ChangeColor(int d)
        {
            if (currentColorIndex + d >= 0 && currentColorIndex + d < colors.Length)
            {
                // Remember previous color index

                int previousColorIndex = currentColorIndex;

                // Update current color index

                currentColorIndex += d;

                // Update previous color pixel

                UpdateColorPixel(previousColorIndex);

                // Update current color pixel

                UpdateColorPixel(currentColorIndex);
            }
        }

        static void Clear()
        {
            for (int y = 0; y < imageHeight; y++)
            {
                for (int x = 0; x < imageWidth; x++)
                {
                    SetImagePixelBackgroundColor(x, y, ConsoleColor.Black);

                    UpdateImagePixel(x, y);
                }
            }

            SetCursorPosition();
        }

        static void Fill()
        {
            ConsoleColor originalColor = GetImagePixelBackgroundColor(pointerX, pointerY);

            if (originalColor != colors[currentColorIndex])
            {
                FillRecursive(pointerX, pointerY, originalColor);

                SetCursorPosition();
            }
        }

        static void MoveRectanglePointer(int dx, int dy)
        {
            // Remember previous pointer location

            int previousX = pointerX;
            int previousY = pointerY;

            // Update current pointer location

            if (pointerX + dx >= 0 && pointerX + dx < imageWidth)
            {
                pointerX += dx;
            }
            if (pointerY + dy >= 0 && pointerY + dy < imageHeight)
            {
                pointerY += dy;
            }

            // Remember previous min/max

            int previousRectangleMinX = rectangleMinX;
            int previousRectangleMinY = rectangleMinY;

            int previousRectangleMaxX = rectangleMaxX;
            int previousRectangleMaxY = rectangleMaxY;

            // Update rectangle min/max

            rectangleMinX = Math.Min(rectangleStartX, pointerX);
            rectangleMinY = Math.Min(rectangleStartY, pointerY);

            rectangleMaxX = Math.Max(rectangleStartX, pointerX);
            rectangleMaxY = Math.Max(rectangleStartY, pointerY);

            // Redraw rectangle preview

            if (previousX != pointerX)
            {
                for (int y = previousRectangleMinY; y <= previousRectangleMaxY; y++)
                {
                    UpdateImagePixel(previousX, y);
                }
                for (int y = rectangleMinY; y <= rectangleMaxY; y++)
                {
                    UpdateImagePixel(pointerX, y);
                }
            }

            if (previousY != pointerY)
            {
                for (int x = previousRectangleMinX; x <= previousRectangleMaxX; x++)
                {
                    UpdateImagePixel(x, previousY);
                }
                for (int x = rectangleMinX; x <= rectangleMaxX; x++)
                {
                    UpdateImagePixel(x, pointerY);
                }
            }

            // Update cursor position

            SetCursorPosition();
        }

        static void CommitRectangle()
        {
            // Reset rectangle start location

            rectangleStartX = -1;
            rectangleStartY = -1;

            // Remember previous min/max

            int previousRectangleMinX = rectangleMinX;
            int previousRectangleMinY = rectangleMinY;

            int previousRectangleMaxX = rectangleMaxX;
            int previousRectangleMaxY = rectangleMaxY;

            // Reset rectangle min/max

            rectangleMinX = -1;
            rectangleMinY = -1;

            rectangleMaxX = -1;
            rectangleMaxY = -1;

            // Paint rectangle

            for (int y = previousRectangleMinY; y <= previousRectangleMaxY; y++)
            {
                for (int x = previousRectangleMinX; x <= previousRectangleMaxX; x++)
                {
                    SetImagePixelBackgroundColor(x, y, colors[currentColorIndex]);

                    UpdateImagePixel(x, y);
                }
            }

            // Set cursor position

            SetCursorPosition();
        }

        static void CancelRectangle()
        {
            // Reset rectangle start location

            rectangleStartX = -1;
            rectangleStartY = -1;

            // Remember previous min/max

            int previousRectangleMinX = rectangleMinX;
            int previousRectangleMinY = rectangleMinY;

            int previousRectangleMaxX = rectangleMaxX;
            int previousRectangleMaxY = rectangleMaxY;

            // Reset rectangle min/max

            rectangleMinX = -1;
            rectangleMinY = -1;

            rectangleMaxX = -1;
            rectangleMaxY = -1;

            // Clear rectangle preview

            for (int x = previousRectangleMinX; x <= previousRectangleMaxX; x++)
            {
                UpdateImagePixel(x, previousRectangleMinY);
                UpdateImagePixel(x, previousRectangleMaxY);
            }

            for (int y = previousRectangleMinY + 1; y < previousRectangleMaxY; y++)
            {
                UpdateImagePixel(previousRectangleMinX, y);
                UpdateImagePixel(previousRectangleMaxX, y);
            }

            // Set cursor position

            SetCursorPosition();
        }

        static void MoveLinePointer(int dx, int dy)
        {
            // Remember previous pointer location

            int previousX = pointerX;
            int previousY = pointerY;

            // Update current pointer location

            if (pointerX + dx >= 0 && pointerX + dx < imageWidth)
            {
                pointerX += dx;
            }
            if (pointerY + dy >= 0 && pointerY + dy < imageHeight)
            {
                pointerY += dy;
            }

            // Update image pixels

            DrawLine(lineStartX, lineStartY, previousX, previousY, 0);
            DrawLine(lineStartX, lineStartY, pointerX, pointerY, 1);

            // Set cursor position

            SetCursorPosition();
        }

        static void CommitLine()
        {
            int previousLineStartX = lineStartX;
            int previousLineStartY = lineStartY;

            // Reset line start location

            lineStartX = -1;
            lineStartY = -1;

            // Update image pixels

            DrawLine(previousLineStartX, previousLineStartY, pointerX, pointerY, 2);

            // Set cursor position

            SetCursorPosition();
        }

        static void CancelLine()
        {
            int previousLineStartX = lineStartX;
            int previousLineStartY = lineStartY;

            // Reset line start location

            lineStartX = -1;
            lineStartY = -1;

            // Update image pixels

            DrawLine(previousLineStartX, previousLineStartY, pointerX, pointerY, 0);

            // Set cursor position

            SetCursorPosition();
        }

        // - HELPERS

        static void InitializeImage()
        {
            for (int x = 0; x < imageWidth; x++)
            {
                for (int y = 0; y < imageHeight; y++)
                {
                    SetImagePixelBackgroundColor(x, y, ConsoleColor.Black);
                }
            }
        }

        static void InitializeInterface()
        {
            ClearScreen();

            PaintBorders();
            PaintColors();
            PaintImage(); // added in this lesson!

            SetCursorPosition(); // added in this lesson!
        }

        static void SayGoodbye()
        {
            ClearScreen();

            Console.WriteLine("Good bye!");
        }

        static void ClearScreen()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();
        }

        static void PaintBorders()
        {
            PaintHorizontalBorder(0);
            PaintHorizontalBorder(Console.WindowHeight - 1);

            PaintVerticalBorder(0);
            PaintVerticalBorder(Console.WindowWidth - 3);
            PaintVerticalBorder(Console.WindowWidth - 1);
        }

        static void PaintHorizontalBorder(int row)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(0, row);

            for (int column = 0; column < Console.WindowWidth; column++)
            {
                Console.Write(' ');
            }
        }

        static void PaintVerticalBorder(int column)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int row = 0; row < Console.WindowHeight; row++)
            {
                Console.SetCursorPosition(column, row);
                Console.Write(' ');
            }
        }

        static void PaintColors()
        {
            for (int colorIndex = 0; colorIndex < colors.Length; colorIndex++)
            {
                UpdateColorPixel(colorIndex);
            }
        }

        static void PaintImage()
        {
            for (int y = 0; y < imageHeight; y++)
            {
                for (int x = 0; x < imageWidth; x++)
                {
                    UpdateImagePixel(x, y);
                }
            }
        }

        static void SetCursorPosition()
        {
            Console.SetCursorPosition(imageOffsetX + pointerX + 1, imageOffsetY + pointerY);
        }

        static void FillRecursive(int x, int y, ConsoleColor originalColor)
        {
            if (x >= 0 && y >= 0 && x < imageWidth && y < imageHeight)
            {
                if (GetImagePixelBackgroundColor(x, y) == originalColor)
                {
                    SetImagePixelBackgroundColor(x, y, colors[currentColorIndex]);

                    UpdateImagePixel(x, y);

                    FillRecursive(x - 1, y, originalColor);
                    FillRecursive(x + 1, y, originalColor);

                    FillRecursive(x, y - 1, originalColor);
                    FillRecursive(x, y + 1, originalColor);
                }
            }
        }

        static void DrawLine(int startX, int startY, int endX, int endY, int mode = 0)
        {
            // Compute line delta

            int lineDeltaX = endX - startX;
            int lineDeltaY = endY - startY;

            // Compute pixel count x/y

            int linePixelCountX = Math.Abs(lineDeltaX);
            int linePixelCountY = Math.Abs(lineDeltaY);

            // Compute pixel count

            int linePixelCount = Math.Max(linePixelCountX, linePixelCountY);

            // Update image pixels

            Console.ForegroundColor = ConsoleColor.White;

            for (double pixel = 0; pixel <= linePixelCount + 0.1; pixel++)
            {
                int x = startX + (linePixelCount > 0 ? (int)Math.Round(lineDeltaX * pixel / linePixelCount) : 0);
                int y = startY + (linePixelCount > 0 ? (int)Math.Round(lineDeltaY * pixel / linePixelCount) : 0);

                Console.SetCursorPosition(imageOffsetX + x, imageOffsetY + y);

                if (mode == 0)
                {
                    Console.BackgroundColor = GetImagePixelBackgroundColor(x, y);

                    if (x == pointerX && y == pointerY)
                    {
                        Console.Write('X');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                else if (mode == 1)
                {
                    Console.BackgroundColor = GetImagePixelBackgroundColor(x, y);

                    if (x == pointerX && y == pointerY)
                    {
                        Console.Write('X');
                    }
                    else if (x == lineStartX && y == lineStartY)
                    {
                        Console.Write('O');
                    }
                    else if (lineDeltaX == 0)
                    {
                        Console.Write('|');
                    }
                    else if (lineDeltaY == 0)
                    {
                        Console.Write('-');
                    }
                    else
                    {
                        Console.Write('+');
                    }
                }
                else
                {
                    SetImagePixelBackgroundColor(x, y, colors[currentColorIndex]);

                    Console.BackgroundColor = GetImagePixelBackgroundColor(x, y);

                    if (x == pointerX && y == pointerY)
                    {
                        Console.Write('X');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
            }
        }

        static void UpdateImagePixel(int x, int y)
        {
            Console.BackgroundColor = GetImagePixelBackgroundColor(x, y);
            Console.ForegroundColor = GetImagePixelForegroundColor(x, y);

            char symbol = GetImagePixelSymbol(x, y);

            Console.SetCursorPosition(imageOffsetX + x, imageOffsetY + y);
            Console.Write(symbol);
        }

        static void UpdateColorPixel(int colorIndex)
        {
            Console.BackgroundColor = colors[colorIndex];
            Console.ForegroundColor = ConsoleColor.White;

            char symbol = GetColorPixelSymbol(colorIndex);

            Console.SetCursorPosition(Console.WindowWidth - 2, 1 + colorIndex);
            Console.Write(symbol);
        }

        static void SetImagePixelBackgroundColor(int x, int y, ConsoleColor color)
        {
            imageData[y * imageWidth + x] = color;
        }

        static ConsoleColor GetImagePixelBackgroundColor(int x, int y)
        {
            return imageData[y * imageWidth + x];
        }

        static ConsoleColor GetImagePixelForegroundColor(int x, int y)
        {
            return ConsoleColor.White;
        }

        static char GetImagePixelSymbol(int x, int y)
        {
            if (x == pointerX && y == pointerY)
            {
                return 'X';
            }
            else if (x == rectangleStartX && y == rectangleStartY)
            {
                return 'O';
            }
            else if ((x == rectangleStartX && y == pointerY) || (x == pointerX && y == rectangleStartY))
            {
                return '+';
            }
            else if ((x == rectangleStartX || x == pointerX) && y >= rectangleMinY && y <= rectangleMaxY)
            {
                return '|';
            }
            else if (x >= rectangleMinX && x <= rectangleMaxX && (y == rectangleStartY || y == pointerY))
            {
                return '-';
            }
            else
            {
                return ' ';
            }
        }

        static char GetColorPixelSymbol(int colorIndex)
        {
            if (colorIndex == currentColorIndex)
            {
                return 'X';
            }
            else
            {
                return ' ';
            }
        }
    }
}
