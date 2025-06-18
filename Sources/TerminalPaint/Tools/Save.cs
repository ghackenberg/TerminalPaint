namespace TerminalPaint.Tools
{
    internal static class Save
    {
        public static void Execute()
        {
            // Let the user choose the file name
            string? fileName = Util.ReadFileName(false);

            // Check if file name has been entered
            if (fileName != null)
            {
                // Create file and open with write access
                FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);

                // Write image width and height to file
                stream.WriteByte((byte)Image.width);
                stream.WriteByte((byte)Image.height);

                // Write pixel colors to file
                for (int pixel = 0; pixel < Image.size; pixel++)
                {
                    stream.WriteByte((byte)Image.data[pixel]);
                }

                // Close file stream
                stream.Close();
            }
        }
    }
}