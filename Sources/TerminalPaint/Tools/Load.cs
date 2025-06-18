namespace TerminalPaint.Tools
{
    internal static class Load
    {
        public static void LoadImage()
        {
            // Read file name from user input
            string? fileName = Util.ReadFileName(true);

            // Check if file name has been entered
            if (fileName != null)
            {
                Util.LoadFile(fileName);

                // Repaint entire image
                Util.PaintImage();
            }
        }
    }
}