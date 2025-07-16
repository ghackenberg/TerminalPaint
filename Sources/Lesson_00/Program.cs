namespace Lesson_00
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Clear screen

            Console.Clear();

            Console.ReadKey(true);

            // Paint borders

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            // Paint top border

            Console.SetCursorPosition(0, 0);

            for (int column = 0; column < Console.WindowWidth; column++)
            {
                Console.Write(' ');
            }

            Console.ReadKey(true);

            // Paint left and right borders

            for (int row = 1; row < Console.WindowHeight - 1; row++)
            {
                // Paint left border
                Console.SetCursorPosition(0, row);
                Console.Write(' ');

                // Paint right border
                Console.SetCursorPosition(Console.WindowWidth - 1, row);
                Console.Write(' ');
            }

            Console.ReadKey(true);

            // Paint bottom border

            Console.SetCursorPosition(0, Console.WindowHeight - 1);

            for (int column = 0; column < Console.WindowWidth; column++)
            {
                Console.Write(' ');
            }

            Console.ReadKey(true);

            // Define pointer location

            int pointerX = Console.WindowWidth / 2;
            int pointerY = Console.WindowHeight / 2;

            // Paint pointer location

            Console.SetCursorPosition(pointerX, pointerY);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write('X');

            // Wait for input

            Console.ReadKey(true);
        }
    }
}
