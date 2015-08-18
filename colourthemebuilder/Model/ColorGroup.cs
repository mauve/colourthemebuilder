using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace colourthemebuilder.Model
{
    public class ColorGroup
    {
        public string Name { get; set; }

        public Color BaseColor { get; set; }

        public IEnumerable<ColorSetting> Colors { get; private set; }

        public ColorGroup(string name, Color color, IEnumerable<ColorSetting> colors)
        {
            Name = name;
            BaseColor = color;
            Colors = colors;
        }
    }
}
