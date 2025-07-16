namespace Lesson_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Clear screen

            Console.Clear();

            // Paint borders

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            // Paint top border

            Console.SetCursorPosition(0, 0);

            for (int column = 0; column < Console.WindowWidth; column++)
            {
                Console.Write(' ');
            }

            // Paint left and right borders

            for (int row = 1; row < Console.WindowHeight - 1; row++)
            {
                // Paint left border
                Console.SetCursorPosition(0, row);
                Console.Write(' ');

                // Paint right border 1
                Console.SetCursorPosition(Console.WindowWidth - 3, row);
                Console.Write(' ');

                // Paint right border 2
                Console.SetCursorPosition(Console.WindowWidth - 1, row);
                Console.Write(' ');
            }

            // Paint bottom border

            Console.SetCursorPosition(0, Console.WindowHeight - 1);

            for (int column = 0; column < Console.WindowWidth; column++)
            {
                Console.Write(' ');
            }

            // Define available colors

            ConsoleColor[] colors =
            {
                ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue
            };

            // Define color selection

            int currentColorIndex = 0;

            // Paint color palette

            Console.ForegroundColor = ConsoleColor.White;

            for (int colorIndex = 0; colorIndex < colors.Length; colorIndex++)
            {
                Console.BackgroundColor = colors[colorIndex];

                Console.SetCursorPosition(Console.WindowWidth - 2, 1 + colorIndex);

                if (colorIndex == currentColorIndex)
                {
                    Console.Write('X');
                }
                else
                {
                    Console.Write(' ');
                }
            }

            // Define current pointer location

            int currentX = Console.WindowWidth / 2;
            int currentY = Console.WindowHeight / 2;

            // Define previous pointer location

            int previousX = 0;
            int previousY = 0;

            // Compute image dimensions

            int imageWidth = Console.WindowWidth - 4;
            int imageHeight = Console.WindowHeight - 2;

            // Compute image size

            int imageSize = imageWidth * imageHeight;

            // Initialize image data

            ConsoleColor[] imageData = new ConsoleColor[imageSize];

            for (int pixel = 0; pixel < imageSize; pixel++)
            {
                imageData[pixel] = ConsoleColor.Black;
            }

            // Define image offset

            int imageOffsetX = 1;
            int imageOffsetY = 1;

            // Read and process user input

            while (true)
            {
                // Reset previous pointer location

                Console.SetCursorPosition(imageOffsetX + previousX, imageOffsetY + previousY);

                Console.BackgroundColor = imageData[previousY * imageWidth + previousX];
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write(' ');

                // Paint current pointer location

                Console.SetCursorPosition(imageOffsetX + currentX, imageOffsetY + currentY);

                Console.BackgroundColor = imageData[currentY * imageWidth + currentX];
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write('X');

                // Update previous pointer location

                previousX = currentX;
                previousY = currentY;

                // Read and process user input

                ConsoleKeyInfo input = Console.ReadKey(true);

                if (input.Key == ConsoleKey.UpArrow)
                {
                    if (currentY > 0)
                    {
                        currentY--;
                    }
                }
                else if (input.Key == ConsoleKey.DownArrow)
                {
                    if (currentY < imageHeight - 1)
                    {
                        currentY++;
                    }
                }
                else if (input.Key == ConsoleKey.LeftArrow)
                {
                    if (currentX > 0)
                    {
                        currentX--;
                    }
                }
                else if (input.Key == ConsoleKey.RightArrow)
                {
                    if (currentX < imageWidth - 1)
                    {
                        currentX++;
                    }
                }
                else if (input.Key == ConsoleKey.PageUp)
                {
                    if (currentColorIndex > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;

                        // Clear previous color

                        Console.BackgroundColor = colors[currentColorIndex];
                        Console.SetCursorPosition(Console.WindowWidth - 2, 1 + currentColorIndex);
                        Console.Write(' ');

                        // Update current color

                        currentColorIndex--;

                        // Paint current color

                        Console.BackgroundColor = colors[currentColorIndex];
                        Console.SetCursorPosition(Console.WindowWidth - 2, 1 + currentColorIndex);
                        Console.Write('X');
                    }
                }
                else if (input.Key == ConsoleKey.PageDown)
                {
                    if (currentColorIndex < colors.Length - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.White;

                        // Clear previous color

                        Console.BackgroundColor = colors[currentColorIndex];
                        Console.SetCursorPosition(Console.WindowWidth - 2, 1 + currentColorIndex);
                        Console.Write(' ');

                        // Update current color

                        currentColorIndex++;

                        // Paint current color

                        Console.BackgroundColor = colors[currentColorIndex];
                        Console.SetCursorPosition(Console.WindowWidth - 2, 1 + currentColorIndex);
                        Console.Write('X');
                    }
                }
                else if (input.Key == ConsoleKey.Spacebar)
                {
                    imageData[currentY * imageWidth + currentX] = colors[currentColorIndex];
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

            // Say goodbye!

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            Console.WriteLine("Good bye!");
        }
    }
}
