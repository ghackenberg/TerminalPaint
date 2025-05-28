namespace TerminalPaint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize previous and current pointer location

            int previousX = 1;
            int previousY = 1;

            int currentX = Console.WindowWidth / 2;
            int currentY = Console.WindowHeight / 2;

            int imageWidth = Console.WindowWidth - 2;
            int imageHeight = Console.WindowHeight - 2;

            int pixelCount = imageWidth * imageHeight;

            ConsoleColor[] imageData = new ConsoleColor[pixelCount];

            for (int pixel = 0; pixel < pixelCount; pixel++)
            {
                imageData[pixel] = ConsoleColor.Black;
            }

            // Make blank screen (and set cursor position to top-left)

            Console.Clear();

            // Paint frame (i.e. top, left, right, bottom borders)

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int column = 0; column < Console.WindowWidth; column++)
            {
                // Top border

                Console.SetCursorPosition(column, 0);
                Console.Write(' ');

                // Bottom border

                Console.SetCursorPosition(column, Console.WindowHeight - 1);
                Console.Write(' ');
            }

            for (int row = 0; row < Console.WindowHeight; row++)
            {
                // Left border

                Console.SetCursorPosition(0, row);
                Console.Write(' ');

                // Right border

                Console.SetCursorPosition(Console.WindowWidth - 1, row);
                Console.Write(' ');
            }

            // Enter main loop (repaint + read and process user input)

            do
            {
                // Repaint previous pointer location

                Console.BackgroundColor = imageData[previousY * imageWidth + previousX];
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(previousX, previousY);
                Console.Write(' ');

                // Repaint current pointer location

                Console.BackgroundColor = imageData[currentY * imageWidth + currentX];
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(currentX, currentY);
                Console.Write('X');

                // Update previous pointer location

                previousX = currentX;
                previousY = currentY;

                // Wait for and process user input

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (currentX > 1)
                    {
                        currentX--;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (currentX < Console.WindowWidth - 2)
                    {
                        currentX++;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (currentY > 1)
                    {
                        currentY--;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (currentY < Console.WindowHeight - 2)
                    {
                        currentY++;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    imageData[currentY * imageWidth + currentX] = ConsoleColor.Red;
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
