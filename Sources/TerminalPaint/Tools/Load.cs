namespace TerminalPaint.Tools
{
    internal static class Load
    {
        public static void Execute()
        {
            // Read file name from user input
            string? fileName = Util.ReadFileName(true);

            // Check if file name has been entered
            if (fileName != null)
            {
                // Open file stream
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                Image.width = stream.ReadByte();
                Image.height = stream.ReadByte();
                Image.size = Image.width * Image.height;
                Image.data = new ConsoleColor[Image.size];
                for (int pixel = 0; pixel < Image.size; pixel++)
                {
                    Image.data[pixel] = (ConsoleColor)stream.ReadByte();
                }
                Image.Paint();

                // Close file stream
                stream.Close();
            }
        }
    }
}