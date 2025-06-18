namespace TerminalPaint.Tools
{
    internal static class Clear
    {
        public static void ClearImage()
        {
            Util.PaintBorderBottom();

            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write("Are you sure? (Y|N|Escape) ");

            bool? sure = Util.ReadBool();

            if (sure != null && sure == true)
            {
                Data.imageData = new ConsoleColor[Data.imageSize];
                for (int pixel = 0; pixel < Data.imageSize; pixel++)
                {
                    Data.imageData[pixel] = ConsoleColor.Black;
                }

                Util.PaintImage();
            }

            Util.PaintBorderBottom();
            Util.PaintTextBottom();

            Console.SetCursorPosition(Data.BORDER_LEFT + Data.currentPointerX + 1, Data.BORDER_TOP + Data.currentPointerY);
        }
    }
}