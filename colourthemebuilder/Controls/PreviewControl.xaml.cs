using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ThemeManagerRt;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace colourthemebuilder.Controls
{
    public sealed partial class PreviewControl : UserControl
    {

        public PreviewControl()
        {
            this.ThemeEnableThisElement();
            this.InitializeComponent();
        }
    }
}
