using TerminalPaint.Tools;

namespace TerminalPaint
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // Make blank screen (and set cursor position to top-left)

            Console.Clear();

            Util.PaintFrame();
            Color.PaintPalette();
            Image.Paint();

            // Enter main loop (repaint + read and process user input)
            do
            {
                // Wait for and process user input
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.PageUp)
                {
                    Color.Change(-1);
                }
                else if (keyInfo.Key == ConsoleKey.PageDown)
                {
                    Color.Change(+1);
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    Pointer.Move(-1, 0);
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    Pointer.Move(+1, 0);
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    Pointer.Move(0, -1);
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    Pointer.Move(0, +1);
                }
                else if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    Pointer.Brush();
                }
                else if (keyInfo.Key == ConsoleKey.F)
                {
                    Fill.Execute();
                }
                else if (keyInfo.Key == ConsoleKey.R)
                {
                    Rectangle.Execute();
                }
                else if (keyInfo.Key == ConsoleKey.S)
                {
                    Save.Execute();
                }
                else if (keyInfo.Key == ConsoleKey.L)
                {
                    Load.Execute();
                }
                else if (keyInfo.Key == ConsoleKey.C)
                {
                    Clear.Execute();
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            while (true);

            // Say goodbye

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            Console.WriteLine("Good bye!");
        }
    }
}
