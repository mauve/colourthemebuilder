using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace colourthemebuilder.Model
{
    public class ColorSetting : INotifyPropertyChanged
    {
        public string ResourceName { get; set; }

        private Color _lightColor;
        public Color LightColor
        {
            get { return _lightColor; }
            set
            {
                if (_lightColor != value)
                {
                    _lightColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LightColor)));
                }
            }
        }

        private Color _darkColor;
        public Color DarkColor
        {
            get { return _darkColor; }
            set
            {
                if (_darkColor != value)
                {
                    _darkColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DarkColor)));
                }
            }
        }

        public Color OriginalLightColor { get; set; }
        public Color OriginalDarkColor { get; set; }

        public double LightFactor { get; set; }

        public double OriginalLightFactor { get; set; }

        public double DarkFactor { get; set; }

        public double OriginalDarkFactor { get; set; }


        public ColorSetting(string name, Color light, Color dark)
        {
            ResourceName = name;
            _lightColor = light;
            _darkColor = dark;
            OriginalLightColor = light;
            OriginalDarkColor = dark;
            OriginalLightFactor = 0.0;
            LightFactor = 0.0;
            OriginalDarkFactor = 0.0;
            DarkFactor = 0.0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public object CreateResourceObject(Color color)
        {
            if (ResourceName.EndsWith("Brush"))
            {
                return new SolidColorBrush(color);
            }
            else if (ResourceName.EndsWith("Color"))
            {
                return color;
            }
            else
            {
                switch (ResourceName)
                {
                    case "JumpListDefaultEnabledForeground":
                    case "JumpListDefaultEnabledBackground":
                    case "JumpListDefaultDisabledForeground":
                    case "JumpListDefaultDisabledBackground":
                        return new SolidColorBrush(color);
                }

                throw new ArgumentException($"unknown resource type for resource: {ResourceName}");
            }
        }
    }
}
