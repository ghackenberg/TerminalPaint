using TerminalPaint.Tools;

namespace TerminalPaint
{
    /// <summary>
    /// Encapsulates utility functions.
    /// </summary>
    internal static class Util
    {
        /// <summary>
        /// The distance from console window top to image top in pixels.
        /// </summary>
        public static readonly int BORDER_TOP = 1;
        /// <summary>
        /// The distance from console window left to image left in pixels.
        /// </summary>
        public static readonly int BORDER_LEFT = 1;
        /// <summary>
        /// The distance from console window right to image right in pixels.
        /// </summary>
        public static readonly int BORDER_RIGHT = 5;
        /// <summary>
        /// The distance from console window bottom to image bottom in pixels.
        /// </summary>
        public static readonly int BORDER_BOTTOM = 1;

        /// <summary>
        /// Paint the entire frame of the application.
        /// </summary>
        public static void PaintFrame()
        {
            // Paint borders

            PaintBorderTop();
            PaintBorderLeft();
            PaintBorderRight();
            PaintBorderBottom();

            // Paint text

            PaintTextTop();
            PaintTextBottom();
        }

        /// <summary>
        /// Paint only the top border of the application.
        /// </summary>
        public static void PaintBorderTop()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int column = 0; column < Console.WindowWidth; column++)
            {
                Console.SetCursorPosition(column, 0);
                Console.Write(' ');
            }
        }
        /// <summary>
        /// Paint only the left border of the application.
        /// </summary>
        public static void PaintBorderLeft()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int row = 0; row < Console.WindowHeight; row++)
            {
                Console.SetCursorPosition(0, row);
                Console.Write(' ');
            }
        }
        /// <summary>
        /// Paint only the right border of the application.
        /// </summary>
        public static void PaintBorderRight()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int row = 0; row < Console.WindowHeight; row++)
            {
                // Right border 1

                Console.SetCursorPosition(Console.WindowWidth - 5, row);
                Console.Write(' ');

                // Right border 2

                Console.SetCursorPosition(Console.WindowWidth - 1, row);
                Console.Write(' ');
            }
        }
        /// <summary>
        /// Paint only the bottom border of the application.
        /// </summary>()
        public static void PaintBorderBottom()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int column = 0; column < Console.WindowWidth; column++)
            {
                Console.SetCursorPosition(column, Console.WindowHeight - 1);
                Console.Write(' ');
            }
        }

        /// <summary>
        /// Paint only the standard top border text of the application.
        /// </summary>
        public static void PaintTextTop()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(1, 0);
            Console.Write("TP | Color = Page Up/Down, Pointer = Arrow Up/Down/Left/Right, Paint = Space, Fill = F, Rectangle = R, Line = L");
        }

        /// <summary>
        /// Paint only the standard bottom border text of the application.
        /// </summary>
        public static void PaintTextBottom()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write("(c) 2025 Dr. Georg Hackenberg <georg.hackenberg@fh-wels.at> | Open = O, Save = S, Clear = C, Close = Escape");
        }

        /// <summary>
        /// Read a boolean input from the user (yes or no).
        /// </summary>
        /// <returns>The user choice.</returns>
        public static bool? ReadBool()
        {
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Y)
                {
                    return true;
                }
                else if (keyInfo.Key == ConsoleKey.N)
                {
                    return false;
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    return null;
                }
            }
            while (true);
        }
        /// <summary>
        /// Read a file name from the user.
        /// </summary>
        /// <param name="exists">Indicate whether the file must exist or not.</param>
        /// <returns>The selected file name.</returns>
        public static string? ReadFileName(bool exists)
        {
            // Overwrite the original bottom border

            PaintBorderBottom();

            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write("Please enter file name (Char|Enter|Escape): ");

            // Read file name from user

            string? fileName = "";

            do
            {
                // Read key input
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // Process key
                if (!Char.IsControl(keyInfo.KeyChar))
                {
                    // Append character
                    fileName = fileName + keyInfo.KeyChar;

                    Console.Write(keyInfo.KeyChar);
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    // Remove last character
                    if (fileName.Length > 0)
                    {
                        fileName = fileName.Substring(0, fileName.Length - 1);

                        Console.CursorLeft--;
                        Console.Write(' ');
                        Console.CursorLeft--;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    // Cancel file name input
                    fileName = null;

                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    // Commit file name input
                    if (exists)
                    {
                        if (File.Exists(fileName))
                        {
                            // Finish file name input
                            break;
                        }
                        else
                        {
                            // Inform the user that the file does not exist and repeat file input

                            PaintBorderBottom();

                            Console.SetCursorPosition(1, Console.WindowHeight - 1);

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("File does not exist! ");

                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Please enter file name (Char|Enter|Escape): ");

                            fileName = "";
                        }
                    }
                    else
                    {
                        if (File.Exists(fileName))
                        {
                            // Inform the user that the file already exists

                            PaintBorderBottom();

                            Console.SetCursorPosition(1, Console.WindowHeight - 1);

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("File already exists! ");

                            // Ask to overwrite the file or nor

                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Overwrite? (Y|N|Escape) ");

                            bool? overwrite = ReadBool();

                            if (overwrite == null)
                            {
                                // Cancel file name input
                                fileName = null;

                                break;
                            }
                            else if (overwrite == false)
                            {
                                // Repeat file name input

                                PaintBorderBottom();

                                Console.SetCursorPosition(1, Console.WindowHeight - 1);
                                Console.Write("Please enter file name (Char|Enter|Escape): ");

                                fileName = "";
                            }
                            else if (overwrite == true)
                            {
                                // Finish file name input
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            while (true);

            // Paint the original bottom border

            PaintBorderBottom();
            PaintTextBottom();

            // Set cursor position to current pointer location

            Console.SetCursorPosition(BORDER_LEFT + Pointer.currentX + 1, BORDER_TOP + Pointer.currentY);

            // Return file name

            return fileName;
        }
    }
}