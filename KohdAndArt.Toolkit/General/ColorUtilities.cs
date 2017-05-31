using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KohdAndArt.Toolkit
{
    public static class ColorUtilities
    {
        /// <summary>
        /// Convert a string of the form RRGGBB to a Color
        /// </summary>
        /// <param name="hexRGB">Input string as a color in hex format Ex: FF00FF</param>
        /// <returns></returns>
        public static Color GetColorFromHexRGBString(string hexRGB)
        {
            var opacity = 255;
            var ExpectedInputStringWidth = 6;

            if (hexRGB.Length < ExpectedInputStringWidth)
                throw new ArgumentException("Invalid Color Specified");

            // Get the Hex Strings and convert to integer
            var rHex = hexRGB.Substring(0, 2);
            var gHex = hexRGB.Substring(2, 2);
            var bHex = hexRGB.Substring(4, 2);

            var r = int.Parse(rHex, System.Globalization.NumberStyles.HexNumber);
            var g = int.Parse(gHex, System.Globalization.NumberStyles.HexNumber);
            var b = int.Parse(bHex, System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(opacity, r, g, b);
        }


        public static void ConvertRGBHexStringToBase10(string rgbHex, out byte R, out byte G, out byte B)
        {
            if (rgbHex.Length < 6 || rgbHex.Length > 6)
            {
                throw new ArgumentOutOfRangeException("Bad Hex Value String");
            }

            var r = rgbHex.Substring(0, 2);
            var g = rgbHex.Substring(2, 2);
            var b = rgbHex.Substring(4, 2);

            R = (byte)Convert.ToInt32(r, 16);
            G = (byte)Convert.ToInt32(g, 16);
            B = (byte)Convert.ToInt32(b, 16);
        }


        /// <summary>
        /// Convert a string of the form RRGGBB to a Color
        /// </summary>
        /// <param name="hexRGB">Input string as a color in hex format Ex: FF00FF</param>
        /// <returns></returns>
        public static Color GetColorFromHexARGBString(string hexARGB)
        {
            //var opacity = 255;
            var ExpectedInputStringWidth = 8;

            if (hexARGB.Length < ExpectedInputStringWidth)
                throw new ArgumentException("Invalid Color Specified");

            // Get the Hex Strings and convert to integer
            var opacity = hexARGB.Substring(0, 2);
            var rHex = hexARGB.Substring(2, 2);
            var gHex = hexARGB.Substring(4, 2);
            var bHex = hexARGB.Substring(6, 2);

            var o = int.Parse(opacity, System.Globalization.NumberStyles.HexNumber);
            var r = int.Parse(rHex, System.Globalization.NumberStyles.HexNumber);
            var g = int.Parse(gHex, System.Globalization.NumberStyles.HexNumber);
            var b = int.Parse(bHex, System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(o, r, g, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string ConvertColorToHexARGBString(Color c)
        {
            // Set the String (Hex) representation of the color
            var alpha = c.A.ToString("X2");
            var r = c.R.ToString("X2");
            var g = c.G.ToString("X2");
            var b = c.B.ToString("X2");
            return $"{alpha}{r}{g}{b}";
        }

        public static string ConvertColorToHexRGBString(Color c)
        {
            // Set the String (Hex) representation of the color
            var r = c.R.ToString("X2");
            var g = c.G.ToString("X2");
            var b = c.B.ToString("X2");
            return $"{r}{g}{b}";
        }

        public static List<string> GetListOfNamedColors()
        {
            List<string> colors = new List<string>();
            foreach (System.Reflection.PropertyInfo prop in typeof(Color).GetProperties())
            {
                if (prop.PropertyType.FullName == "System.Drawing.Color")
                {
                    colors.Add(prop.Name);
                }
            }
            return colors;
        }
    }
}
