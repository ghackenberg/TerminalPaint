namespace TerminalPaint.Tools
{
    internal static class Clear
    {
        public static void Execute()
        {
            Util.PaintBorderBottom();

            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write("Are you sure? (Y|N|Escape) ");

            bool? sure = Util.ReadBool();

            if (sure != null && sure == true)
            {
                Image.data = new ConsoleColor[Image.size];
                Image.Paint();
            }

            Util.PaintBorderBottom();
            Util.PaintTextBottom();

            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX + 1, Util.BORDER_TOP + Pointer.currentY);
        }
    }
}