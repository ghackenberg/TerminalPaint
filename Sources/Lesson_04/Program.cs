namespace Lesson_04
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
                else if (input.Key == ConsoleKey.C) // added in this lesson!
                {
                    Clear();
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

        static void Clear() // added in this lesson!
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
            UpdateImagePixel(pointerX, pointerY);
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

        static void SetCursorPosition() // added in this lesson!
        {
            Console.SetCursorPosition(imageOffsetX + pointerX + 1, imageOffsetY + pointerY);
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
