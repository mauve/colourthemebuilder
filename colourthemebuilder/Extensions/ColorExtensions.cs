using System;
using System.Collections.Generic;
using Windows.UI;

namespace colourthemebuilder.Extensions
{
    public static class ColorExtensions
    {
        private static Dictionary<string, Color> KnownColors = new Dictionary<string, Color>();

        static ColorExtensions()
        {
            KnownColors.Add("AliceBlue", Windows.UI.Colors.AliceBlue);
            KnownColors.Add("AntiqueWhite", Windows.UI.Colors.AntiqueWhite);
            KnownColors.Add("Aqua", Windows.UI.Colors.Aqua);
            KnownColors.Add("Aquamarine", Windows.UI.Colors.Aquamarine);
            KnownColors.Add("Azure", Windows.UI.Colors.Azure);
            KnownColors.Add("Beige", Windows.UI.Colors.Beige);
            KnownColors.Add("Bisque", Windows.UI.Colors.Bisque);
            KnownColors.Add("Black", Windows.UI.Colors.Black);
            KnownColors.Add("BlanchedAlmond", Windows.UI.Colors.BlanchedAlmond);
            KnownColors.Add("Blue", Windows.UI.Colors.Blue);
            KnownColors.Add("BlueViolet", Windows.UI.Colors.BlueViolet);
            KnownColors.Add("Brown", Windows.UI.Colors.Brown);
            KnownColors.Add("BurlyWood", Windows.UI.Colors.BurlyWood);
            KnownColors.Add("CadetBlue", Windows.UI.Colors.CadetBlue);
            KnownColors.Add("Chartreuse", Windows.UI.Colors.Chartreuse);
            KnownColors.Add("Chocolate", Windows.UI.Colors.Chocolate);
            KnownColors.Add("Coral", Windows.UI.Colors.Coral);
            KnownColors.Add("CornflowerBlue", Windows.UI.Colors.CornflowerBlue);
            KnownColors.Add("Cornsilk", Windows.UI.Colors.Cornsilk);
            KnownColors.Add("Crimson", Windows.UI.Colors.Crimson);
            KnownColors.Add("Cyan", Windows.UI.Colors.Cyan);
            KnownColors.Add("DarkBlue", Windows.UI.Colors.DarkBlue);
            KnownColors.Add("DarkCyan", Windows.UI.Colors.DarkCyan);
            KnownColors.Add("DarkGoldenrod", Windows.UI.Colors.DarkGoldenrod);
            KnownColors.Add("DarkGray", Windows.UI.Colors.DarkGray);
            KnownColors.Add("DarkGreen", Windows.UI.Colors.DarkGreen);
            KnownColors.Add("DarkKhaki", Windows.UI.Colors.DarkKhaki);
            KnownColors.Add("DarkMagenta", Windows.UI.Colors.DarkMagenta);
            KnownColors.Add("DarkOliveGreen", Windows.UI.Colors.DarkOliveGreen);
            KnownColors.Add("DarkOrange", Windows.UI.Colors.DarkOrange);
            KnownColors.Add("DarkOrchid", Windows.UI.Colors.DarkOrchid);
            KnownColors.Add("DarkRed", Windows.UI.Colors.DarkRed);
            KnownColors.Add("DarkSalmon", Windows.UI.Colors.DarkSalmon);
            KnownColors.Add("DarkSeaGreen", Windows.UI.Colors.DarkSeaGreen);
            KnownColors.Add("DarkSlateBlue", Windows.UI.Colors.DarkSlateBlue);
            KnownColors.Add("DarkSlateGray", Windows.UI.Colors.DarkSlateGray);
            KnownColors.Add("DarkTurquoise", Windows.UI.Colors.DarkTurquoise);
            KnownColors.Add("DarkViolet", Windows.UI.Colors.DarkViolet);
            KnownColors.Add("DeepPink", Windows.UI.Colors.DeepPink);
            KnownColors.Add("DeepSkyBlue", Windows.UI.Colors.DeepSkyBlue);
            KnownColors.Add("DimGray", Windows.UI.Colors.DimGray);
            KnownColors.Add("DodgerBlue", Windows.UI.Colors.DodgerBlue);
            KnownColors.Add("Firebrick", Windows.UI.Colors.Firebrick);
            KnownColors.Add("FloralWhite", Windows.UI.Colors.FloralWhite);
            KnownColors.Add("ForestGreen", Windows.UI.Colors.ForestGreen);
            KnownColors.Add("Fuchsia", Windows.UI.Colors.Fuchsia);
            KnownColors.Add("Gainsboro", Windows.UI.Colors.Gainsboro);
            KnownColors.Add("GhostWhite", Windows.UI.Colors.GhostWhite);
            KnownColors.Add("Gold", Windows.UI.Colors.Gold);
            KnownColors.Add("Goldenrod", Windows.UI.Colors.Goldenrod);
            KnownColors.Add("Gray", Windows.UI.Colors.Gray);
            KnownColors.Add("Green", Windows.UI.Colors.Green);
            KnownColors.Add("GreenYellow", Windows.UI.Colors.GreenYellow);
            KnownColors.Add("Honeydew", Windows.UI.Colors.Honeydew);
            KnownColors.Add("HotPink", Windows.UI.Colors.HotPink);
            KnownColors.Add("IndianRed", Windows.UI.Colors.IndianRed);
            KnownColors.Add("Indigo", Windows.UI.Colors.Indigo);
            KnownColors.Add("Ivory", Windows.UI.Colors.Ivory);
            KnownColors.Add("Khaki", Windows.UI.Colors.Khaki);
            KnownColors.Add("Lavender", Windows.UI.Colors.Lavender);
            KnownColors.Add("LavenderBlush", Windows.UI.Colors.LavenderBlush);
            KnownColors.Add("LawnGreen", Windows.UI.Colors.LawnGreen);
            KnownColors.Add("LemonChiffon", Windows.UI.Colors.LemonChiffon);
            KnownColors.Add("LightBlue", Windows.UI.Colors.LightBlue);
            KnownColors.Add("LightCoral", Windows.UI.Colors.LightCoral);
            KnownColors.Add("LightCyan", Windows.UI.Colors.LightCyan);
            KnownColors.Add("LightGoldenrodYellow", Windows.UI.Colors.LightGoldenrodYellow);
            KnownColors.Add("LightGray", Windows.UI.Colors.LightGray);
            KnownColors.Add("LightGreen", Windows.UI.Colors.LightGreen);
            KnownColors.Add("LightPink", Windows.UI.Colors.LightPink);
            KnownColors.Add("LightSalmon", Windows.UI.Colors.LightSalmon);
            KnownColors.Add("LightSeaGreen", Windows.UI.Colors.LightSeaGreen);
            KnownColors.Add("LightSkyBlue", Windows.UI.Colors.LightSkyBlue);
            KnownColors.Add("LightSlateGray", Windows.UI.Colors.LightSlateGray);
            KnownColors.Add("LightSteelBlue", Windows.UI.Colors.LightSteelBlue);
            KnownColors.Add("LightYellow", Windows.UI.Colors.LightYellow);
            KnownColors.Add("Lime", Windows.UI.Colors.Lime);
            KnownColors.Add("LimeGreen", Windows.UI.Colors.LimeGreen);
            KnownColors.Add("Linen", Windows.UI.Colors.Linen);
            KnownColors.Add("Magenta", Windows.UI.Colors.Magenta);
            KnownColors.Add("Maroon", Windows.UI.Colors.Maroon);
            KnownColors.Add("MediumAquamarine", Windows.UI.Colors.MediumAquamarine);
            KnownColors.Add("MediumBlue", Windows.UI.Colors.MediumBlue);
            KnownColors.Add("MediumOrchid", Windows.UI.Colors.MediumOrchid);
            KnownColors.Add("MediumPurple", Windows.UI.Colors.MediumPurple);
            KnownColors.Add("MediumSeaGreen", Windows.UI.Colors.MediumSeaGreen);
            KnownColors.Add("MediumSlateBlue", Windows.UI.Colors.MediumSlateBlue);
            KnownColors.Add("MediumSpringGreen", Windows.UI.Colors.MediumSpringGreen);
            KnownColors.Add("MediumTurquoise", Windows.UI.Colors.MediumTurquoise);
            KnownColors.Add("MediumVioletRed", Windows.UI.Colors.MediumVioletRed);
            KnownColors.Add("MidnightBlue", Windows.UI.Colors.MidnightBlue);
            KnownColors.Add("MintCream", Windows.UI.Colors.MintCream);
            KnownColors.Add("MistyRose", Windows.UI.Colors.MistyRose);
            KnownColors.Add("Moccasin", Windows.UI.Colors.Moccasin);
            KnownColors.Add("NavajoWhite", Windows.UI.Colors.NavajoWhite);
            KnownColors.Add("Navy", Windows.UI.Colors.Navy);
            KnownColors.Add("OldLace", Windows.UI.Colors.OldLace);
            KnownColors.Add("Olive", Windows.UI.Colors.Olive);
            KnownColors.Add("OliveDrab", Windows.UI.Colors.OliveDrab);
            KnownColors.Add("Orange", Windows.UI.Colors.Orange);
            KnownColors.Add("OrangeRed", Windows.UI.Colors.OrangeRed);
            KnownColors.Add("Orchid", Windows.UI.Colors.Orchid);
            KnownColors.Add("PaleGoldenrod", Windows.UI.Colors.PaleGoldenrod);
            KnownColors.Add("PaleGreen", Windows.UI.Colors.PaleGreen);
            KnownColors.Add("PaleTurquoise", Windows.UI.Colors.PaleTurquoise);
            KnownColors.Add("PaleVioletRed", Windows.UI.Colors.PaleVioletRed);
            KnownColors.Add("PapayaWhip", Windows.UI.Colors.PapayaWhip);
            KnownColors.Add("PeachPuff", Windows.UI.Colors.PeachPuff);
            KnownColors.Add("Peru", Windows.UI.Colors.Peru);
            KnownColors.Add("Pink", Windows.UI.Colors.Pink);
            KnownColors.Add("Plum", Windows.UI.Colors.Plum);
            KnownColors.Add("PowderBlue", Windows.UI.Colors.PowderBlue);
            KnownColors.Add("Purple", Windows.UI.Colors.Purple);
            KnownColors.Add("Red", Windows.UI.Colors.Red);
            KnownColors.Add("RosyBrown", Windows.UI.Colors.RosyBrown);
            KnownColors.Add("RoyalBlue", Windows.UI.Colors.RoyalBlue);
            KnownColors.Add("SaddleBrown", Windows.UI.Colors.SaddleBrown);
            KnownColors.Add("Salmon", Windows.UI.Colors.Salmon);
            KnownColors.Add("SandyBrown", Windows.UI.Colors.SandyBrown);
            KnownColors.Add("SeaGreen", Windows.UI.Colors.SeaGreen);
            KnownColors.Add("SeaShell", Windows.UI.Colors.SeaShell);
            KnownColors.Add("Sienna", Windows.UI.Colors.Sienna);
            KnownColors.Add("Silver", Windows.UI.Colors.Silver);
            KnownColors.Add("SkyBlue", Windows.UI.Colors.SkyBlue);
            KnownColors.Add("SlateBlue", Windows.UI.Colors.SlateBlue);
            KnownColors.Add("SlateGray", Windows.UI.Colors.SlateGray);
            KnownColors.Add("Snow", Windows.UI.Colors.Snow);
            KnownColors.Add("SpringGreen", Windows.UI.Colors.SpringGreen);
            KnownColors.Add("SteelBlue", Windows.UI.Colors.SteelBlue);
            KnownColors.Add("Tan", Windows.UI.Colors.Tan);
            KnownColors.Add("Teal", Windows.UI.Colors.Teal);
            KnownColors.Add("Thistle", Windows.UI.Colors.Thistle);
            KnownColors.Add("Tomato", Windows.UI.Colors.Tomato);
            KnownColors.Add("Transparent", Windows.UI.Colors.Transparent);
            KnownColors.Add("Turquoise", Windows.UI.Colors.Turquoise);
            KnownColors.Add("Violet", Windows.UI.Colors.Violet);
            KnownColors.Add("Wheat", Windows.UI.Colors.Wheat);
            KnownColors.Add("White", Windows.UI.Colors.White);
            KnownColors.Add("WhiteSmoke", Windows.UI.Colors.WhiteSmoke);
            KnownColors.Add("Yellow", Windows.UI.Colors.Yellow);
            KnownColors.Add("YellowGreen", Windows.UI.Colors.YellowGreen);
        }

        public static Color FromString(string argb)
        {
            Color found;
            if (KnownColors.TryGetValue(argb, out found))
                return found;

            argb = argb.TrimStart(new[] { '#' });

            if (argb.Length == 8)
            {
                byte a = Convert.ToByte(argb.Substring(0, 2), 16);
                byte r = Convert.ToByte(argb.Substring(2, 2), 16);
                byte g = Convert.ToByte(argb.Substring(4, 2), 16);
                byte b = Convert.ToByte(argb.Substring(6, 2), 16);

                return Color.FromArgb(a, r, g, b);
            }
            else if (argb.Length == 6)
            {
                byte r = Convert.ToByte(argb.Substring(0, 2), 16);
                byte g = Convert.ToByte(argb.Substring(2, 2), 16);
                byte b = Convert.ToByte(argb.Substring(4, 2), 16);

                return Color.FromArgb(255, r, g, b);
            }
            else if (argb.Length == 4)
            {
                byte a = Convert.ToByte(argb.Substring(0, 1), 16);
                byte r = Convert.ToByte(argb.Substring(1, 1), 16);
                byte g = Convert.ToByte(argb.Substring(2, 1), 16);
                byte b = Convert.ToByte(argb.Substring(3, 1), 16);

                return Color.FromArgb((byte)(a | a << 4),
                                      (byte)(r | r << 4),
                                      (byte)(g | g << 4),
                                      (byte)(b | b << 4));
            }
            else if (argb.Length == 3)
            {
                byte r = Convert.ToByte(argb.Substring(0, 1), 16);
                byte g = Convert.ToByte(argb.Substring(1, 1), 16);
                byte b = Convert.ToByte(argb.Substring(2, 1), 16);

                return Color.FromArgb(255,
                                      (byte)(r | r << 4),
                                      (byte)(g | g << 4),
                                      (byte)(b | b << 4));
            }
            else if (argb.Length == 2)
            {
                byte all = Convert.ToByte(argb, 16);

                return Color.FromArgb(all, all, all, all);
            }
            else if (argb.Length == 1)
            {
                byte d = Convert.ToByte(argb, 16);

                return Color.FromArgb((byte)(d | d << 4),
                                      (byte)(d | d << 4),
                                      (byte)(d | d << 4),
                                      (byte)(d | d << 4));
            }
            else
            {
                throw new ArgumentException("expected format #aarrggbb");
            }
        }
    }
}
