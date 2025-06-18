namespace TerminalPaint.Tools
{
    internal static class Save
    {
        public static void SaveImage()
        {
            // Let the user choose the file name
            string? fileName = Util.ReadFileName(false);

            // Check if file name has been entered
            if (fileName != null)
            {
                Util.SaveFile(fileName);
            }
        }
    }
}