using TerminalPaint.Tools;

namespace TerminalPaint
{
    internal static class Util
    {
        public static readonly int BORDER_TOP = 1;
        public static readonly int BORDER_LEFT = 1;
        public static readonly int BORDER_RIGHT = 5;
        public static readonly int BORDER_BOTTOM = 1;

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

        public static void PaintTextBottom()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write("(c) 2025 Dr. Georg Hackenberg <georg.hackenberg@fh-wels.at> | Load = L, Save = S, Clear = C, Close = Escape");
        }

        public static void PaintTextTop()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(1, 0);
            Console.Write("TerminalPaint | Pointer = Arrow Up/Down/Left/Right, Color = Page Up/Down, Paint = Space, Fill = F, Rectangle = R");
        }

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

        public static string? ReadFileName(bool exists)
        {
            PaintBorderBottom();

            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write("Please enter file name (Char|Enter|Escape): ");

            string? fileName = "";

            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (!Char.IsControl(keyInfo.KeyChar))
                {
                    fileName = fileName + keyInfo.KeyChar;

                    Console.Write(keyInfo.KeyChar);
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
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
                    fileName = null;

                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (exists)
                    {
                        if (File.Exists(fileName))
                        {
                            break;
                        }
                        else
                        {
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
                            PaintBorderBottom();

                            Console.SetCursorPosition(1, Console.WindowHeight - 1);

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("File already exists! ");

                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Overwrite? (Y|N|Escape) ");

                            bool? overwrite = ReadBool();

                            if (overwrite == null)
                            {
                                fileName = null;

                                break;
                            }
                            else if (overwrite == false)
                            {
                                PaintBorderBottom();

                                Console.SetCursorPosition(1, Console.WindowHeight - 1);
                                Console.Write("Please enter file name (Char|Enter|Escape): ");

                                fileName = "";
                            }
                            else if (overwrite == true)
                            {
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

            PaintBorderBottom();
            PaintTextBottom();

            Console.SetCursorPosition(BORDER_LEFT + Pointer.currentX + 1, BORDER_TOP + Pointer.currentY);

            return fileName;
        }
    }
}