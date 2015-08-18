using colourthemebuilder.Extensions;
using System;
using Windows.UI;

namespace colourthemebuilder.Colors
{
    public class HSLColor
    {
        // Private data members below are on scale 0-1
        // They are scaled for use externally based on scale
        private double _hue = 1.0;
        private double _saturation = 1.0;
        private double _luminosity = 1.0;

        private const double Scale = 240.0;

        private double _alpha = 1.0;

        public double Hue
        {
            get { return _hue * Scale; }
            set { _hue = CheckRange(value / Scale); }
        }

        public double Saturation
        {
            get { return _saturation * Scale; }
            set { _saturation = CheckRange(value / Scale); }
        }

        public double Luminosity
        {
            get { return _luminosity * Scale; }
            set { _luminosity = CheckRange(value / Scale); }
        }

        public double Alpha
        {
            get { return _alpha; }
            set { _alpha = value; }
        }

        private double CheckRange(double value)
        {
            if (value < 0.0)
                value = 0.0;
            else if (value > 1.0)
                value = 1.0;
            return value;
        }

        public override string ToString()
        {
            return String.Format("H: {0:#0.##} S: {1:#0.##} L: {2:#0.##} A: {3:#0.##}", Hue, Saturation, Luminosity, Alpha);
        }

        public string ToRGBString()
        {
            var color = ToColor();
            return String.Format("R: {0:#0.##} G: {1:#0.##} B: {2:#0.##} A: {3:#0.##}", color.R, color.G, color.B, color.A);
        }

        public Color ToColor()
        {
            double r = 0, g = 0, b = 0;
            if (_luminosity != 0)
            {
                if (_saturation == 0)
                    r = g = b = _luminosity;
                else
                {
                    double temp2 = GetTemp2(this);
                    double temp1 = 2.0 * _luminosity - temp2;

                    r = GetColorComponent(temp1, temp2, _hue + 1.0 / 3.0);
                    g = GetColorComponent(temp1, temp2, _hue);
                    b = GetColorComponent(temp1, temp2, _hue - 1.0 / 3.0);
                }
            }
            return Color.FromArgb((byte)(255 * Alpha), (byte)(255 * r), (byte)(255 * g), (byte)(255 * b));
        }

        private static double GetColorComponent(double temp1, double temp2, double temp3)
        {
            temp3 = MoveIntoRange(temp3);
            if (temp3 < 1.0 / 6.0)
                return temp1 + (temp2 - temp1) * 6.0 * temp3;

            if (temp3 < 0.5)
                return temp2;

            if (temp3 < 2.0 / 3.0)
                return temp1 + ((temp2 - temp1) * ((2.0 / 3.0) - temp3) * 6.0);

            return temp1;
        }

        private static double MoveIntoRange(double temp3)
        {
            if (temp3 < 0.0)
                temp3 += 1.0;
            else if (temp3 > 1.0)
                temp3 -= 1.0;
            return temp3;
        }

        private static double GetTemp2(HSLColor hslColor)
        {
            double temp2;
            if (hslColor._luminosity < 0.5)  //<=??
                temp2 = hslColor._luminosity * (1.0 + hslColor._saturation);
            else
                temp2 = hslColor._luminosity + hslColor._saturation - (hslColor._luminosity * hslColor._saturation);
            return temp2;
        }

        public static HSLColor FromColor(Color color)
        {
            double _R = (color.R / 255.0);
            double _G = (color.G / 255.0);
            double _B = (color.B / 255.0);
            double _A = (color.A / 255.0);

            double _Min = Math.Min(Math.Min(_R, _G), _B);
            double _Max = Math.Max(Math.Max(_R, _G), _B);
            double _Delta = _Max - _Min;

            double H = 0;
            double S = 0;
            double L = ((_Max + _Min) / 2.0);

            if (_Delta != 0)
            {
                if (L < 0.5)
                {
                    S = (float)(_Delta / (_Max + _Min));
                }
                else
                {
                    S = (float)(_Delta / (2.0 - _Max - _Min));
                }

                if (_R == _Max)
                {
                    H = (_G - _B) / _Delta;
                }
                else if (_G == _Max)
                {
                    H = 2.0 + (_B - _R) / _Delta;
                }
                else if (_B == _Max)
                {
                    H = 4.0 + (_R - _G) / _Delta;
                }
            }

            // we store hue as 0-1 as opposed to 0-360 
            return new HSLColor(_A, H * 240.0, S * 240.0, L * 240.0);
        }

        public void SetRGB(byte alpha, byte red, byte green, byte blue)
        {
            var hslColor = FromColor(Color.FromArgb(alpha, red, green, blue));
            _hue = hslColor._hue;
            _saturation = hslColor._saturation;
            _luminosity = hslColor._luminosity;
            _alpha = hslColor._alpha;
        }

        public HSLColor()
        {}

        public HSLColor(Color color)
        {
            SetRGB(color.A, color.R, color.G, color.B);
        }

        public HSLColor(string hex)
        {
            var color = ColorExtensions.FromString(hex);
            SetRGB(color.A, color.R, color.G, color.B);
        }

        public HSLColor(byte alpha, byte red, byte green, byte blue)
        {
            SetRGB(alpha, red, green, blue);
        }

        public HSLColor(double alpha, double hue, double saturation, double luminosity)
        {
            Alpha = alpha;
            Hue = hue;
            Saturation = saturation;
            Luminosity = luminosity;
        }

        public string ToHex()
        {
            var color = ToColor();
            return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.R, color.G, color.B);
        }
    }
}
