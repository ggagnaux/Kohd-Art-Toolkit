using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KohdAndArt.Toolkit.Console
{
    public static class ConsoleEx
    {
        public static int LeftMargin { get; set; } = 0;


        // Replacements for Generic Console.Write and Console.WriteLine methods

        /// <summary>
        /// Write text to the console with the specified foreground color
        /// </summary>
        /// <param name="color"></param>
        /// <param name="t"></param>
        public static void WriteColored(ConsoleColor color, string t)
        {
            if (LeftMargin > 0)
            {
                System.Console.Write(new string(' ', LeftMargin));
            }
            System.Console.ForegroundColor = color;
            System.Console.Write(t);
            System.Console.ResetColor();
        }

        /// <summary>
        /// Write text to the console with the specified foreground and background colors
        /// </summary>
        /// <param name="colorF"></param>
        /// <param name="colorB"></param>
        /// <param name="t"></param>
        public static void WriteColored(ConsoleColor colorF, ConsoleColor colorB, string t)
        {
            if (LeftMargin > 0)
            {
                System.Console.Write(new string(' ', LeftMargin));
            }
            System.Console.ForegroundColor = colorF;
            System.Console.BackgroundColor = colorB;
            System.Console.Write(t);
            System.Console.ResetColor();
        }

        /// <summary>
        /// Write a line of text to the console with the specified foreground color
        /// </summary>
        /// <param name="color"></param>
        /// <param name="t"></param>
        public static void WriteLineColored(ConsoleColor color, string t)
        {
            WriteColored(color, t + Environment.NewLine);
        }

        /// <summary>
        /// Write a line of text to the console with the specified foreground and background colors
        /// </summary>
        /// <param name="colorF"></param>
        /// <param name="colorB"></param>
        /// <param name="t"></param>
        public static void WriteLineColored(ConsoleColor colorF, ConsoleColor colorB, string t)
        {
            WriteColored(colorF, colorB, t + Environment.NewLine);
        }

        // Specific Colors

        /// <summary>
        /// Write a line of text to the console with the foreground color set to Red
        /// </summary>
        /// <param name="t"></param>
        public static void WriteLineRed(string t) { WriteLineColored(ConsoleColor.Red, t); }

        /// <summary>
        /// Write a line of text to the console with the foreground color set to Green
        /// </summary>
        /// <param name="t"></param>
        public static void WriteLineGreen(string t) { WriteLineColored(ConsoleColor.Green, t); }

        /// <summary>
        /// Write a line of text to the console with the foreground color set to Blue
        /// </summary>
        /// <param name="t"></param>
        public static void WriteLineBlue(string t) { WriteLineColored(ConsoleColor.Blue, t); }

        /// <summary>
        /// Write a line of text to the console with the foreground color set to Yellow
        /// </summary>
        /// <param name="t"></param>
        public static void WriteLineYellow(string t) { WriteLineColored(ConsoleColor.Yellow, t); }

        /// <summary>
        /// Write text to the console with the foreground color set to Red
        /// </summary>
        /// <param name="t"></param>
        public static void WriteRed(string t) { WriteColored(ConsoleColor.Red, t); }

        /// <summary>
        /// Write text to the console with the foreground color set to Green
        /// </summary>
        /// <param name="t"></param>
        public static void WriteGreen(string t) { WriteColored(ConsoleColor.Green, t); }

        /// <summary>
        /// Write text to the console with the foreground color set to Blue
        /// </summary>
        /// <param name="t"></param>
        public static void WriteBlue(string t) { WriteColored(ConsoleColor.Blue, t); }

        /// <summary>
        /// Write text to the console with the foreground color set to Yellow
        /// </summary>
        /// <param name="t"></param>
        public static void WriteYellow(string t) { WriteColored(ConsoleColor.Yellow, t); }
    }
}
