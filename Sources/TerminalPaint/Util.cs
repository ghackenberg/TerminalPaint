namespace TerminalPaint
{
    internal static class Util
    {
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

        public static void PaintColorPalette()
        {
            for (int color = 0; color < Data.COLORS.Length; color++)
            {
                int colorX = Console.WindowWidth - 3;
                int colorY = 2 + color * 2;

                if (colorY < Console.WindowHeight - 1)
                {
                    Console.BackgroundColor = Data.COLORS[color];
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(colorX, colorY);
                    if (color == Data.currentColor)
                    {
                        Console.Write('X');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
            }
        }

        public static void PaintImage()
        {
            for (int pixelX = 0; pixelX < Data.imageWidth; pixelX++)
            {
                for (int pixelY = 0; pixelY < Data.imageHeight; pixelY++)
                {
                    Console.BackgroundColor = Data.imageData[pixelY * Data.imageWidth + pixelX];
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(Data.BORDER_LEFT + pixelX, Data.BORDER_TOP + pixelY);
                    if (Data.currentPointerX == pixelX && Data.currentPointerY == pixelY)
                    {
                        Console.Write('X');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
            }

            Console.SetCursorPosition(Data.BORDER_LEFT + Data.currentPointerX + 1, Data.BORDER_TOP + Data.currentPointerY);
        }

        public static void MovePointer()
        {
            // Repaint previous pointer location

            Console.BackgroundColor = Data.imageData[Data.previousPointerY * Data.imageWidth + Data.previousPointerX];
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(Data.BORDER_LEFT + Data.previousPointerX, Data.BORDER_TOP + Data.previousPointerY);
            Console.Write(' ');

            // Repaint current pointer location

            Console.BackgroundColor = Data.imageData[Data.currentPointerY * Data.imageWidth + Data.currentPointerX];
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(Data.BORDER_LEFT + Data.currentPointerX, Data.BORDER_TOP + Data.currentPointerY);
            Console.Write('X');

            // Update previous pointer location

            Data.previousPointerX = Data.currentPointerX;
            Data.previousPointerY = Data.currentPointerY;
        }

        public static void LoadFile(string fileName)
        {
            // Open file stream
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            Data.imageWidth = stream.ReadByte();
            Data.imageHeight = stream.ReadByte();

            Data.imageSize = Data.imageWidth * Data.imageHeight;
            Data.imageData = new ConsoleColor[Data.imageSize];

            for (int pixel = 0; pixel < Data.imageSize; pixel++)
            {
                Data.imageData[pixel] = (ConsoleColor)stream.ReadByte();
            }

            // Close file stream
            stream.Close();
        }

        public static void SaveFile(string fileName)
        {
            // Create file and open with write access
            FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);

            // Write image width and height to file
            stream.WriteByte((byte)Data.imageWidth);
            stream.WriteByte((byte)Data.imageHeight);

            // Write pixel colors to file
            for (int pixel = 0; pixel < Data.imageSize; pixel++)
            {
                stream.WriteByte((byte)Data.imageData[pixel]);
            }

            // Close file stream
            stream.Close();
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

            Console.SetCursorPosition(Data.BORDER_LEFT + Data.currentPointerX + 1, Data.BORDER_TOP + Data.currentPointerY);

            return fileName;
        }
    }
}