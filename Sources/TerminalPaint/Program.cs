using TerminalPaint.Tools;

namespace TerminalPaint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize image data

            for (int pixel = 0; pixel < Data.imageSize; pixel++)
            {
                Data.imageData[pixel] = ConsoleColor.Black;
            }

            // Make blank screen (and set cursor position to top-left)

            Console.Clear();

            Util.PaintFrame();
            Util.PaintColorPalette();
            Util.PaintImage();

            // Enter main loop (repaint + read and process user input)
            do
            {
                // Wait for and process user input
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (Data.currentPointerX > 0)
                    {
                        Data.currentPointerX--;
                        Util.MovePointer();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (Data.currentPointerX < Data.imageWidth - 1)
                    {
                        Data.currentPointerX++;
                        Util.MovePointer();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (Data.currentPointerY > 0)
                    {
                        Data.currentPointerY--;
                        Util.MovePointer();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (Data.currentPointerY < Data.imageHeight - 1)
                    {
                        Data.currentPointerY++;
                        Util.MovePointer();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.PageUp)
                {
                    if (Data.currentColor > 0)
                    {
                        Data.currentColor--;
                        Color.ChangeColor();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.PageDown)
                {
                    if (Data.currentColor < Data.COLORS.Length - 1)
                    {
                        Data.currentColor++;
                        Color.ChangeColor();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    Data.imageData[Data.currentPointerY * Data.imageWidth + Data.currentPointerX] = Data.COLORS[Data.currentColor];
                    Util.MovePointer();
                }
                else if (keyInfo.Key == ConsoleKey.F)
                {
                    Fill.FillImage();
                }
                else if (keyInfo.Key == ConsoleKey.R)
                {
                    Rectangle.DrawRectangle();
                }
                else if (keyInfo.Key == ConsoleKey.S)
                {
                    Save.SaveImage();
                }
                else if (keyInfo.Key == ConsoleKey.L)
                {
                    Load.LoadImage();
                }
                else if (keyInfo.Key == ConsoleKey.C)
                {
                    Clear.ClearImage();
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
