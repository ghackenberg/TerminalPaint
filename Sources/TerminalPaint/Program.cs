using System.Drawing;

namespace TerminalPaint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize colors

            ConsoleColor[] colors = 
            {
                ConsoleColor.Red,
                ConsoleColor.DarkRed,
                ConsoleColor.Green,
                ConsoleColor.DarkGreen,
                ConsoleColor.Blue,
                ConsoleColor.DarkBlue,
                ConsoleColor.Yellow,
                ConsoleColor.DarkYellow,
                ConsoleColor.Cyan,
                ConsoleColor.DarkCyan,
                ConsoleColor.Magenta,
                ConsoleColor.DarkMagenta,
                ConsoleColor.White,
                ConsoleColor.Gray,
                ConsoleColor.DarkGray,
                ConsoleColor.Black
            };

            int previousColor = 0;

            int currentColor = 0;

            // Initialize borders

            int borderTop = 3;
            int borderLeft = 2;
            int borderRight = 6;
            int borderBottom = 3;

            // Initialize image width / height and data

            int imageWidth = Console.WindowWidth - borderLeft - borderRight;
            int imageHeight = Console.WindowHeight - borderTop - borderBottom;

            int pixelCount = imageWidth * imageHeight;

            ConsoleColor[] imageData = new ConsoleColor[pixelCount];

            for (int pixel = 0; pixel < pixelCount; pixel++)
            {
                imageData[pixel] = ConsoleColor.Black;
            }

            // Initialize previous and current pointer location

            int previousX = 0;
            int previousY = 0;

            int currentX = imageWidth / 2;
            int currentY = imageHeight / 2;

            // Make blank screen (and set cursor position to top-left)

            Console.Clear();

            // Paint frame (i.e. top, left, right, bottom borders)

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int column = 0; column < Console.WindowWidth; column++)
            {
                // Top border 1

                Console.SetCursorPosition(column, 0);
                Console.Write(' ');

                // Bottom border 2

                Console.SetCursorPosition(column, Console.WindowHeight - 1);
                Console.Write(' ');
            }

            for (int row = 0; row < Console.WindowHeight; row++)
            {
                // Left border

                Console.SetCursorPosition(0, row);
                Console.Write(' ');

                // Right border 1

                Console.SetCursorPosition(Console.WindowWidth - 5, row);
                Console.Write(' ');

                // Right border 2

                Console.SetCursorPosition(Console.WindowWidth - 1, row);
                Console.Write(' ');
            }

            // Paint text

            Console.SetCursorPosition(1, 0);
            Console.Write("TerminalPaint v0.0.1 | Pointer = Arrow Up/Down/Left/Right, Color = Page Up/Down, Paint = Space, Close = Escape");

            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write("(c) 2025 Dr. Georg Hackenberg, Professor for Industrial Informatics, School of Engineering, FH Upper Austria");

            // Paint colors

            for (int color = 0; color < colors.Length; color++)
            {
                int colorX = Console.WindowWidth - 3;
                int colorY = 2 + color * 2;

                if (colorY < Console.WindowHeight - 1)
                {
                    Console.BackgroundColor = colors[color];
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(colorX, colorY);
                    Console.Write(color == currentColor ? 'X' : ' ');
                }
            }

            // Enter main loop (repaint + read and process user input)

            do
            {
                // Repaint previous color

                int previousColorX = Console.WindowWidth - 3;
                int previousColorY = 2 + previousColor * 2;

                if (previousColorY < Console.WindowHeight - 1)
                {
                    Console.BackgroundColor = colors[previousColor];
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(previousColorX, previousColorY);
                    Console.Write(' ');
                }

                // Repaint current color

                int currentColorX = Console.WindowWidth - 3;
                int currentColorY = 2 + currentColor * 2;

                if (currentColorY < Console.WindowHeight - 1)
                {
                    Console.BackgroundColor = colors[currentColor];
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(currentColorX, currentColorY);
                    Console.Write('X');
                }

                // Repaint previous pointer location

                Console.BackgroundColor = imageData[previousY * imageWidth + previousX];
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(borderLeft + previousX, borderTop + previousY);
                Console.Write(' ');

                // Repaint current pointer location

                Console.BackgroundColor = imageData[currentY * imageWidth + currentX];
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(borderLeft + currentX, borderTop + currentY);
                Console.Write('X');

                // Update previous color

                previousColor = currentColor;

                // Update previous pointer location

                previousX = currentX;
                previousY = currentY;

                // Wait for and process user input

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (currentX > 0)
                    {
                        currentX--;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (currentX < imageWidth - 1)
                    {
                        currentX++;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (currentY > 0)
                    {
                        currentY--;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (currentY < imageHeight - 1)
                    {
                        currentY++;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.PageUp)
                {
                    if (currentColor > 0)
                    {
                        currentColor--;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.PageDown)
                {
                    if (currentColor < colors.Length - 1)
                    {
                        currentColor++;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    imageData[currentY * imageWidth + currentX] = colors[currentColor];
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            while (true);

            // Say goodbye

            Console.Clear();
            Console.WriteLine("Good bye!");
        }
    }
}
