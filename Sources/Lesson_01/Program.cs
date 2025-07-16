namespace Lesson_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Clear screen

            Console.Clear();

            // Define current pointer location

            int currentX = Console.WindowWidth / 2;
            int currentY = Console.WindowHeight / 2;

            // Define previous pointer location

            int previousX = 0;
            int previousY = 0;

            // Read and process user input

            while (true)
            {
                // Reset previous pointer location

                Console.SetCursorPosition(previousX, previousY);

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write(' ');

                // Paint current pointer location

                Console.SetCursorPosition(currentX, currentY);

                Console.BackgroundColor = ConsoleColor.Black;
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
                    if (currentY < Console.WindowHeight - 1)
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
                    if (currentX < Console.WindowWidth - 1)
                    {
                        currentX++;
                    }
                }
                else if (input.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}
