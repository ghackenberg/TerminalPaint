namespace TerminalPaint
{
    internal static class Data
    {
        public static readonly ConsoleColor[] COLORS =
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

        public static readonly int BORDER_TOP = 1;
        public static readonly int BORDER_LEFT = 1;
        public static readonly int BORDER_RIGHT = 5;
        public static readonly int BORDER_BOTTOM = 1;

        public static int imageWidth = Console.WindowWidth - Data.BORDER_LEFT - Data.BORDER_RIGHT;
        public static int imageHeight = Console.WindowHeight - Data.BORDER_TOP - Data.BORDER_BOTTOM;
        public static int imageSize = imageWidth * imageHeight;

        public static ConsoleColor[] imageData = new ConsoleColor[imageSize];

        public static int previousColor = 0;
        public static int currentColor = 0;

        public static int previousPointerX = imageWidth / 2;
        public static int previousPointerY = imageHeight / 2;
        public static int currentPointerX = imageWidth / 2;
        public static int currentPointerY = imageHeight / 2;
    }
}