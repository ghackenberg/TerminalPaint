﻿namespace TerminalPaint
{
    internal class Program
    {
        static readonly ConsoleColor[] colors =
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

        static readonly int borderTop = 2;
        static readonly int borderLeft = 2;
        static readonly int borderRight = 6;
        static readonly int borderBottom = 2;

        static int imageWidth = Console.WindowWidth - borderLeft - borderRight;
        static int imageHeight = Console.WindowHeight - borderTop - borderBottom;
        static int imageSize = imageWidth * imageHeight;

        static ConsoleColor[] imageData = new ConsoleColor[imageSize];

        static int previousColor = 0;
        static int currentColor = 0;

        static int previousPointerX = imageWidth / 2;
        static int previousPointerY = imageHeight / 2;
        static int currentPointerX = imageWidth / 2;
        static int currentPointerY = imageHeight / 2;

        static void Main(string[] args)
        {
            // Initialize image data
            for (int pixel = 0; pixel < imageSize; pixel++)
            {
                imageData[pixel] = ConsoleColor.Black;
            }

            // Make blank screen (and set cursor position to top-left)
            Console.Clear();

            // Update GUI
            PaintFrame();
            PaintColor();
            PaintImage();

            // Enter main loop (repaint + read and process user input)
            do
            {
                // Wait for and process user input
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (currentPointerX > 0)
                    {
                        currentPointerX--;
                        UpdateImage();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (currentPointerX < imageWidth - 1)
                    {
                        currentPointerX++;
                        UpdateImage();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (currentPointerY > 0)
                    {
                        currentPointerY--;
                        UpdateImage();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (currentPointerY < imageHeight - 1)
                    {
                        currentPointerY++;
                        UpdateImage();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.PageUp)
                {
                    if (currentColor > 0)
                    {
                        currentColor--;
                        UpdateColor();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.PageDown)
                {
                    if (currentColor < colors.Length - 1)
                    {
                        currentColor++;
                        UpdateColor();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    imageData[currentPointerY * imageWidth + currentPointerX] = colors[currentColor];
                    UpdateImage();
                }
                else if (keyInfo.Key == ConsoleKey.S)
                {
                    SaveImage();
                }
                else if (keyInfo.Key == ConsoleKey.L)
                {
                    LoadImage();
                }
                else if (keyInfo.Key == ConsoleKey.C)
                {
                    ClearImage();
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

        static void PaintFrame()
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

        static void PaintBorderTop()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int column = 0; column < Console.WindowWidth; column++)
            {
                Console.SetCursorPosition(column, 0);
                Console.Write(' ');
            }
        }

        static void PaintBorderLeft()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int row = 0; row < Console.WindowHeight; row++)
            {
                Console.SetCursorPosition(0, row);
                Console.Write(' ');
            }
        }

        static void PaintBorderRight()
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

        static void PaintBorderBottom()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int column = 0; column < Console.WindowWidth; column++)
            {
                Console.SetCursorPosition(column, Console.WindowHeight - 1);
                Console.Write(' ');
            }
        }

        static void PaintTextTop()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(1, 0);
            Console.Write("TerminalPaint v0.0.1 | Pointer = Arrow Up/Down/Left/Right, Color = Page Up/Down, Paint = Space");
        }

        static void PaintTextBottom()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write("(c) 2025 Dr. Georg Hackenberg <georg.hackenberg@fh-wels.at> | Load = L, Save = S, Clear = C, Close = Escape");
        }

        static void PaintColor()
        {
            for (int color = 0; color < colors.Length; color++)
            {
                int colorX = Console.WindowWidth - 3;
                int colorY = 2 + color * 2;

                if (colorY < Console.WindowHeight - 1)
                {
                    Console.BackgroundColor = colors[color];
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(colorX, colorY);
                    if (color == currentColor)
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

        static void UpdateColor()
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

            // Update previous color

            previousColor = currentColor;
        }

        static void PaintImage()
        {
            for (int pixelX = 0; pixelX < imageWidth; pixelX++)
            {
                for (int pixelY = 0; pixelY < imageHeight; pixelY++)
                {
                    Console.BackgroundColor = imageData[pixelY * imageWidth + pixelX];
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(borderLeft + pixelX, borderTop + pixelY);
                    if (currentPointerX == pixelX && currentPointerY == pixelY)
                    {
                        Console.Write('X');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
            }

            Console.SetCursorPosition(borderLeft + currentPointerX + 1, borderTop + currentPointerY);
        }

        static void UpdateImage()
        {
            // Repaint previous pointer location

            Console.BackgroundColor = imageData[previousPointerY * imageWidth + previousPointerX];
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(borderLeft + previousPointerX, borderTop + previousPointerY);
            Console.Write(' ');

            // Repaint current pointer location

            Console.BackgroundColor = imageData[currentPointerY * imageWidth + currentPointerX];
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(borderLeft + currentPointerX, borderTop + currentPointerY);
            Console.Write('X');

            // Update previous pointer location

            previousPointerX = currentPointerX;
            previousPointerY = currentPointerY;
        }

        static void SaveImage()
        {
            // Let the user choose the file name
            string? fileName = ReadFileName(false);

            // Check if file name has been entered
            if (fileName != null)
            {
                // Create file and open with write access
                FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);

                // Write image width and height to file
                stream.WriteByte((byte)imageWidth);
                stream.WriteByte((byte)imageHeight);

                // Write pixel colors to file
                for (int pixel = 0; pixel < imageSize; pixel++)
                {
                    stream.WriteByte((byte)imageData[pixel]);
                }

                // Close file stream
                stream.Close();
            }
        }

        static void LoadImage()
        {
            // Read file name from user input
            string? fileName = ReadFileName(true);

            // Check if file name has been entered
            if (fileName != null)
            {
                // Open file stream
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                // Read image width and height
                imageWidth = stream.ReadByte();
                imageHeight = stream.ReadByte();

                // Recompute pixel count
                imageSize = imageWidth * imageHeight;

                // Read image data
                imageData = new ConsoleColor[imageSize];
                for (int pixel = 0; pixel < imageSize; pixel++)
                {
                    imageData[pixel] = (ConsoleColor)stream.ReadByte();
                }

                // Repaint entire image
                PaintImage();

                // Close file stream
                stream.Close();
            }
        }

        static void ClearImage()
        {
            PaintBorderBottom();

            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write("Are you sure? (Y|N|Escape) ");

            bool? sure = ReadBool();

            if (sure != null && sure == true)
            {
                imageData = new ConsoleColor[imageSize];
                for (int pixel = 0; pixel < imageSize; pixel++)
                {
                    imageData[pixel] = ConsoleColor.Black;
                }

                PaintImage();
            }

            PaintBorderBottom();
            PaintTextBottom();

            Console.SetCursorPosition(borderLeft + currentPointerX + 1, borderTop + currentPointerY);
        }

        static string? ReadFileName(bool exists)
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

            Console.SetCursorPosition(borderLeft + currentPointerX + 1, borderTop + currentPointerY);

            return fileName;
        }

        static bool? ReadBool()
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
    }
}
