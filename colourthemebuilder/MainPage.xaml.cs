using colourthemebuilder.Colors;
using colourthemebuilder.Model;
using ThemeManagerRt;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace colourthemebuilder
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ViewModel _model = new ViewModel();

        public MainPage()
        {
            DataContext = _model;
            InitializeComponent();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var newHeight = ComputePreviewHeight(e.NewSize.Height);
            foreach (var preview in _model.ThemePreviews)
                preview.PreviewHeight = newHeight;
        }

        private double ComputePreviewHeight(double? actualHeight = null)
        {
            const double diffHeight = 80.0;
            if (!actualHeight.HasValue)
                return ActualHeight - diffHeight;
            return actualHeight.Value - diffHeight;
        }

        private void AddTheme_Click(object sender, RoutedEventArgs e)
        {
            _model.ThemePreviews.Add(new ThemePreview { Title = "Title 1", PreviewHeight = ComputePreviewHeight() });
        }

        private void ApplyTheme_Click(object sender, RoutedEventArgs e)
        {
            var newBaseColor = new HSLColor(_model.BaseColor);

            var lightDictionary = new ResourceDictionary();
            var darkDictionary = new ResourceDictionary();

            foreach(var group in _model.ColorGroups)
            {
                foreach(var setting in group.Colors)
                {
                    var lightColor = new HSLColor(setting.LightColor);
                    var darkColor = new HSLColor(setting.DarkColor);
                    var origLightColor = new HSLColor(setting.OriginalLightColor);
                    var origDarkColor = new HSLColor(setting.OriginalDarkColor);

                    var newLightColor = new HSLColor(origLightColor.Alpha,
                                                     newBaseColor.Hue,
                                                     newBaseColor.Saturation,
                                                     origLightColor.Luminosity);
                    var newDarkColor = new HSLColor(origDarkColor.Alpha,
                                                    newBaseColor.Hue,
                                                    newBaseColor.Saturation,
                                                    origDarkColor.Luminosity);

                    var lightResult = newLightColor.ToColor();
                    var darkResult = newDarkColor.ToColor();

                    setting.DarkColor = darkResult;
                    setting.LightColor = lightResult;

                    lightDictionary[setting.ResourceName] = setting.CreateResourceObject(lightResult);
                    darkDictionary[setting.ResourceName] = setting.CreateResourceObject(darkResult);
                }
            }
            ChangeTheme(lightDictionary, darkDictionary);
        }

        private void ChangeTheme(ResourceDictionary light, ResourceDictionary dark)
        {
            var _themeManager = (ThemeManager)Application.Current.Resources["ThemeManager"];

            var lightDicts = ((ResourceDictionary)Application.Current.Resources.ThemeDictionaries["Light"]).MergedDictionaries;
            lightDicts.Clear();
            lightDicts.Add(light);

            var darkDicts = ((ResourceDictionary)Application.Current.Resources.ThemeDictionaries["Dark"]).MergedDictionaries;
            darkDicts.Clear();
            darkDicts.Add(dark);

            if (ThemeManager.DefaultTheme == ElementTheme.Dark)
            {
                _themeManager.Theme = ElementTheme.Light;
                _themeManager.Theme = ElementTheme.Dark;
            }
            else
            {
                _themeManager.Theme = ElementTheme.Dark;
                _themeManager.Theme = ElementTheme.Light;
            }
        }
    }
}
