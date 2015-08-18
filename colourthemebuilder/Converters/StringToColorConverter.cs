using colourthemebuilder.Extensions;
using System;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace colourthemebuilder.Converters
{
    class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var color = (Color)value;
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.R, color.G, color.B);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var str = (string)value;
            try
            {
                return ColorExtensions.FromString(str);
            }
            catch (ArgumentException)
            {
                return new Color();
            }
        }
    }
}
