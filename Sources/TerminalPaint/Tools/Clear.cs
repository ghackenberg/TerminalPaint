namespace TerminalPaint.Tools
{
    /// <summary>
    /// Implementation of the clear operation.
    /// </summary>
    internal static class Clear
    {
        /// <summary>
        /// Ask for confirmation and reset the image pixel colors to black, if confirmed
        /// </summary>
        public static void Execute()
        {
            // Ask for confirmation before clearing

            Util.PaintBorderBottom();

            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write("Are you sure? (Y|N|Escape) ");

            bool? sure = Util.ReadBool();

            // Check user input and clear, if confirmed

            if (sure != null && sure == true)
            {
                Image.data = new ConsoleColor[Image.size];
                Image.Paint();
            }

            // Paint standard bottom border

            Util.PaintBorderBottom();
            Util.PaintTextBottom();

            // Set cursor position to current pointer location

            Console.SetCursorPosition(Util.BORDER_LEFT + Pointer.currentX + 1, Util.BORDER_TOP + Pointer.currentY);
        }
    }
}