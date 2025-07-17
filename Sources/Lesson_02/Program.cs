namespace Lesson_02
{
    internal class Program
    {
        // FIELDS

        static readonly int imageOffsetX = 1;
        static readonly int imageOffsetY = 1;

        static readonly int imageWidth = Console.WindowWidth - 2;
        static readonly int imageHeight = Console.WindowHeight - 2;

        static readonly int imageSize = imageWidth * imageHeight; // added in this lesson!

        static readonly ConsoleColor[] imageData = new ConsoleColor[imageSize]; // added in this lesson!

        static int pointerX = imageWidth / 2;
        static int pointerY = imageHeight / 2;

        // METHODS

        static void Main(string[] args) // revised in this lesson!
        {
            InitializeImage(); // added in this lesson!
            InitializeInterface();
            MainLoop(); // revised in this lesson!
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
                else if (input.Key == ConsoleKey.Spacebar) // added in this lesson!
                {
                    Stroke();
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

        static void Stroke() // added in this lesson!
        {
            ConsoleColor color = ConsoleColor.Red;

            SetImagePixelBackgroundColor(pointerX, pointerY, color);

            UpdateImagePixel(pointerX, pointerY);
        }

        // - HELPERS

        static void InitializeImage() // added in this lesson!
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

        static void UpdateImagePixel(int x, int y)
        {
            Console.BackgroundColor = GetImagePixelBackgroundColor(x, y);
            Console.ForegroundColor = GetImagePixelForegroundColor(x, y);

            char symbol = GetImagePixelSymbol(x, y);

            Console.SetCursorPosition(imageOffsetX + x, imageOffsetY + y);
            Console.Write(symbol);
        }

        static void SetImagePixelBackgroundColor(int x, int y, ConsoleColor color) // added in this lesson!
        {
            imageData[y * imageWidth + x] = color;
        }

        static ConsoleColor GetImagePixelBackgroundColor(int x, int y) // revised in this lesson!
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
    }
}
