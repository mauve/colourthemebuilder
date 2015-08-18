using colourthemebuilder.Colors;
using colourthemebuilder.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml.Media;

/* LIGHT

            <Color x:Key="SystemAltHighColor">#FFFFFFFF</Color>
            <Color x:Key="SystemAltLowColor">#33FFFFFF</Color>
            <Color x:Key="SystemAltMediumColor">#99FFFFFF</Color>
            <Color x:Key="SystemAltMediumHighColor">#CCFFFFFF</Color>
            <Color x:Key="SystemAltMediumLowColor">#66FFFFFF</Color>
            <Color x:Key="SystemBaseHighColor">#FF000000</Color>
            <Color x:Key="SystemBaseLowColor">#33000000</Color>
            <Color x:Key="SystemBaseMediumColor">#99000000</Color>
            <Color x:Key="SystemBaseMediumHighColor">#CC000000</Color>
            <Color x:Key="SystemBaseMediumLowColor">#66000000</Color>
            <Color x:Key="SystemChromeAltLowColor">#FF171717</Color>
            <Color x:Key="SystemChromeBlackHighColor">#FF000000</Color>
            <Color x:Key="SystemChromeBlackLowColor">#33000000</Color>
            <Color x:Key="SystemChromeBlackMediumLowColor">#66000000</Color>
            <Color x:Key="SystemChromeBlackMediumColor">#CC000000</Color>
            <Color x:Key="SystemChromeDisabledHighColor">#FFCCCCCC</Color>
            <Color x:Key="SystemChromeDisabledLowColor">#FF7A7A7A</Color>
            <Color x:Key="SystemChromeHighColor">#FFCCCCCC</Color>
            <Color x:Key="SystemChromeLowColor">#FFF2F2F2</Color>
            <Color x:Key="SystemChromeMediumColor">#FFE6E6E6</Color>
            <Color x:Key="SystemChromeMediumLowColor">#FFF2F2F2</Color>
            <Color x:Key="SystemChromeWhiteColor">#FFFFFFFF</Color>
            <Color x:Key="SystemListLowColor">#19000000</Color>
            <Color x:Key="SystemListMediumColor">#33000000</Color>

*/

namespace colourthemebuilder.Model
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ThemePreview> ThemePreviews { get; private set; } = new ObservableCollection<ThemePreview>();

        private Color _baseColor;
        public Color BaseColor
        {
            get { return _baseColor; }
            set
            {
                if (_baseColor != value)
                {
                    _baseColor = value;
                    BaseColorBrush.Color = _baseColor;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BaseColor)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BaseColorBrush)));
                }
            }
        }

        public SolidColorBrush BaseColorBrush { get; private set; } = new SolidColorBrush();

        public IEnumerable<ColorGroup> AllColorGroups { get; private set; }

        public IEnumerable<ColorGroup> ColorGroups
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Filter))
                    return AllColorGroups;
                else
                    return from grp in AllColorGroups
                           select new ColorGroup(grp.Name, grp.BaseColor,
                                                 from clr in grp.Colors
                                                 where clr.ResourceName.CaseInsensitiveContains(_filter)
                                                 select clr);
            }
        }

        string _filter;
        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorGroups)));
                }
            }
        }

        public ViewModel()
        {
            //
            // these values were extracted from generic.xaml using the following PowerShell snippet:
            //
            //      $ns = @{x="http://schemas.microsoft.com/winfx/2006/xaml"; p="http://schemas.microsoft.com/winfx/2006/xaml/presentation"}
            //
            //      [xml]$GenericXaml = Get-Content "C:\Program Files (x86)\Windows Kits\10\DesignTime\CommonConfiguration\Neutral\UAP\10.0.10240.0\Generic\generic.xaml"
            //
            //      $ResourceNames | %{
            //          $name =$_;
            //          $light = (Select -Xml -XPath "//p:ResourceDictionary[@x:Key='Light']/*[@x:Key='$name']/@Color" -Namespace $ns -Xml $GenericXaml);
            //          $dark = (Select -Xml -XPath "//p:ResourceDictionary[@x:Key='Default']/*[@x:Key='$name']/@Color" -Namespace $ns -Xml $GenericXaml);
            //          Write-Output "new ColorSetting(""${name}"", FromArgb(""$light""), FromArgb(""$dark"")),";
            //      }
            //
            // IMPORTANT: The base color in the ColorGroup is currently not used.

            AllColorGroups = new[]
            {
                new ColorGroup("System Colors", FromString("#FFFFFFFF"),
                    new []
                    {
                        new ColorSetting("SystemAltHighColor", FromString("#FFFFFFFF"), FromString("#FF000000")),
                        new ColorSetting("SystemAltLowColor", FromString("#33FFFFFF"), FromString("#33000000")),
                        new ColorSetting("SystemAltMediumColor", FromString("#99FFFFFF"), FromString("#99000000")),
                        new ColorSetting("SystemAltMediumHighColor", FromString("#CCFFFFFF"), FromString("#CC000000")),
                        new ColorSetting("SystemAltMediumLowColor", FromString("#66FFFFFF"), FromString("#66000000")),
                        new ColorSetting("SystemBaseHighColor", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("SystemBaseLowColor", FromString("#33000000"), FromString("#33FFFFFF")),
                        new ColorSetting("SystemBaseMediumColor", FromString("#99000000"), FromString("#99FFFFFF")),
                        new ColorSetting("SystemBaseMediumHighColor", FromString("#CC000000"), FromString("#CCFFFFFF")),
                        new ColorSetting("SystemBaseMediumLowColor", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("SystemChromeAltLowColor", FromString("#FF171717"), FromString("#FFF2F2F2")),
                        new ColorSetting("SystemChromeBlackHighColor", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("SystemChromeBlackLowColor", FromString("#33000000"), FromString("#33000000")),
                        new ColorSetting("SystemChromeBlackMediumLowColor", FromString("#66000000"), FromString("#66000000")),
                        new ColorSetting("SystemChromeBlackMediumColor", FromString("#CC000000"), FromString("#CC000000")),
                        new ColorSetting("SystemChromeDisabledHighColor", FromString("#FFCCCCCC"), FromString("#FF333333")),
                        new ColorSetting("SystemChromeDisabledLowColor", FromString("#FF7A7A7A"), FromString("#FF858585")),
                        new ColorSetting("SystemChromeHighColor", FromString("#FFCCCCCC"), FromString("#FF767676")),
                        new ColorSetting("SystemChromeLowColor", FromString("#FFF2F2F2"), FromString("#FF171717")),
                        new ColorSetting("SystemChromeMediumColor", FromString("#FFE6E6E6"), FromString("#FF1F1F1F")),
                        new ColorSetting("SystemChromeMediumLowColor", FromString("#FFF2F2F2"), FromString("#FF2B2B2B")),
                        new ColorSetting("SystemChromeWhiteColor", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("SystemListLowColor", FromString("#19000000"), FromString("#19FFFFFF")),
                        new ColorSetting("SystemListMediumColor", FromString("#33000000"), FromString("#33FFFFFF"))
                    }),
                new ColorGroup("AppBar (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("AppBarBackgroundThemeBrush", FromString("#FFF0F0F0"), FromString("#FF000000")),
                        new ColorSetting("AppBarBorderThemeBrush", FromString("#FFF0F0F0"), FromString("#FF000000")),
                        new ColorSetting("AppBarItemBackgroundThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("AppBarItemDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("AppBarItemForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("AppBarItemPointerOverBackgroundThemeBrush", FromString("#3D000000"), FromString("#21FFFFFF")),
                        new ColorSetting("AppBarItemPointerOverForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("AppBarItemPressedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                        new ColorSetting("AppBarSeparatorForegroundThemeBrush", FromString("#FF7B7B7B"), FromString("#FF7B7B7B")),
                        new ColorSetting("AppBarToggleButtonCheckedBackgroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("AppBarToggleButtonCheckedBorderThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("AppBarToggleButtonCheckedDisabledBackgroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("AppBarToggleButtonCheckedDisabledBorderThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("AppBarToggleButtonCheckedDisabledForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                        new ColorSetting("AppBarToggleButtonCheckedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                        new ColorSetting("AppBarToggleButtonCheckedPointerOverBackgroundThemeBrush", FromString("#99000000"), FromString("#99FFFFFF")),
                        new ColorSetting("AppBarToggleButtonCheckedPointerOverBorderThemeBrush", FromString("#99000000"), FromString("#99FFFFFF")),
                        new ColorSetting("AppBarToggleButtonCheckedPressedBackgroundThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("AppBarToggleButtonCheckedPressedBorderThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("AppBarToggleButtonCheckedPressedForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("AppBarToggleButtonPointerOverBackgroundThemeBrush", FromString("#3D000000"), FromString("#21FFFFFF"))
                    }),
                new ColorGroup("Application (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("ApplicationForegroundThemeBrush", FromString("#DE000000"), FromString("#DEFFFFFF")),
                        new ColorSetting("ApplicationHeaderForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ApplicationPageBackgroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                        new ColorSetting("ApplicationPointerOverForegroundThemeBrush", FromString("#CC000000"), FromString("#CCFFFFFF")),
                        new ColorSetting("ApplicationPressedForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("ApplicationSecondaryForegroundThemeBrush", FromString("#99000000"), FromString("#99FFFFFF")),
                    }),
                new ColorGroup("AutoSuggest (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("AutoSuggestBackgroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                    }),
                new ColorGroup("BackButton (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("BackButtonBackgroundThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("BackButtonDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("BackButtonForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("BackButtonPointerOverBackgroundThemeBrush", FromString("#3D000000"), FromString("#21FFFFFF")),
                        new ColorSetting("BackButtonPointerOverForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("BackButtonPressedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                    }),
                new ColorGroup("Button (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("ButtonBackgroundThemeBrush", FromString("#B3B6B6B6"), FromString("Transparent")),
                        new ColorSetting("ButtonBorderThemeBrush", FromString("#33000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ButtonDisabledBackgroundThemeBrush", FromString("#66CACACA"), FromString("Transparent")),
                        new ColorSetting("ButtonDisabledBorderThemeBrush", FromString("#1A000000"), FromString("#66FFFFFF")),
                        new ColorSetting("ButtonDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("ButtonForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ButtonPointerOverBackgroundThemeBrush", FromString("#D1CDCDCD"), FromString("#21FFFFFF")),
                        new ColorSetting("ButtonPointerOverForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ButtonPressedBackgroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ButtonPressedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                    }),
                new ColorGroup("CheckBox (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("CheckBoxBackgroundThemeBrush", FromString("#CCFFFFFF"), FromString("#CCFFFFFF")),
                        new ColorSetting("CheckBoxBorderThemeBrush", FromString("#45000000"), FromString("#CCFFFFFF")),
                        new ColorSetting("CheckBoxContentDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("CheckBoxContentForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("CheckBoxDisabledBackgroundThemeBrush", FromString("#66CACACA"), FromString("#66FFFFFF")),
                        new ColorSetting("CheckBoxDisabledBorderThemeBrush", FromString("#26000000"), FromString("#66FFFFFF")),
                        new ColorSetting("CheckBoxDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66000000")),
                        new ColorSetting("CheckBoxForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("CheckBoxPointerOverBackgroundThemeBrush", FromString("#DEFFFFFF"), FromString("#DEFFFFFF")),
                        new ColorSetting("CheckBoxPointerOverBorderThemeBrush", FromString("#70000000"), FromString("#DEFFFFFF")),
                        new ColorSetting("CheckBoxPointerOverForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("CheckBoxPressedBackgroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("CheckBoxPressedBorderThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("CheckBoxPressedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                    }),
                new ColorGroup("ComboBox (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("ComboBoxArrowDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("ComboBoxArrowForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("ComboBoxArrowPressedForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("ComboBoxBackgroundThemeBrush", FromString("#CCFFFFFF"), FromString("#CCFFFFFF")),
                        new ColorSetting("ComboBoxBorderThemeBrush", FromString("#45000000"), FromString("#CCFFFFFF")),
                        new ColorSetting("ComboBoxDisabledBackgroundThemeBrush", FromString("#66CACACA"), FromString("Transparent")),
                        new ColorSetting("ComboBoxDisabledBorderThemeBrush", FromString("#26000000"), FromString("#66FFFFFF")),
                        new ColorSetting("ComboBoxDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("ComboBoxFocusedBackgroundThemeBrush", FromString("White"), FromString("White")),
                        new ColorSetting("ComboBoxFocusedBorderThemeBrush", FromString("#70000000"), FromString("White")),
                        new ColorSetting("ComboBoxFocusedForegroundThemeBrush", FromString("White"), FromString("White")),
                        new ColorSetting("ComboBoxForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("ComboBoxHeaderForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ComboBoxItemDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66000000")),
                        new ColorSetting("ComboBoxItemPointerOverBackgroundThemeBrush", FromString("#21000000"), FromString("#21000000")),
                        new ColorSetting("ComboBoxItemPointerOverForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("ComboBoxItemPressedBackgroundThemeBrush", FromString("#FFD3D3D3"), FromString("#FFD3D3D3")),
                        new ColorSetting("ComboBoxItemPressedForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("ComboBoxItemSelectedBackgroundThemeBrush", FromString("#FF4617B4"), FromString("#FF4617B4")),
                        new ColorSetting("ComboBoxItemSelectedDisabledBackgroundThemeBrush", FromString("#8C000000"), FromString("#8C000000")),
                        new ColorSetting("ComboBoxItemSelectedDisabledForegroundThemeBrush", FromString("#99FFFFFF"), FromString("#99FFFFFF")),
                        new ColorSetting("ComboBoxItemSelectedForegroundThemeBrush", FromString("White"), FromString("White")),
                        new ColorSetting("ComboBoxItemSelectedPointerOverBackgroundThemeBrush", FromString("#FF5F37BE"), FromString("#FF5F37BE")),
                        new ColorSetting("ComboBoxPlaceholderTextForegroundThemeBrush", FromString("#88000000"), FromString("#88000000")),
                        new ColorSetting("ComboBoxPointerOverBackgroundThemeBrush", FromString("#DEFFFFFF"), FromString("#DEFFFFFF")),
                        new ColorSetting("ComboBoxPointerOverBorderThemeBrush", FromString("#70000000"), FromString("#DEFFFFFF")),
                        new ColorSetting("ComboBoxPopupBackgroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("ComboBoxPopupBorderThemeBrush", FromString("#FF212121"), FromString("#FF212121")),
                        new ColorSetting("ComboBoxPopupForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("ComboBoxPressedBackgroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("ComboBoxPressedBorderThemeBrush", FromString("#A3000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ComboBoxPressedHighlightThemeBrush", FromString("#FFD3D3D3"), FromString("#FFD3D3D3")),
                        new ColorSetting("ComboBoxPressedForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("ComboBoxSelectedBackgroundThemeBrush", FromString("#FF4617B4"), FromString("#FF4617B4")),
                        new ColorSetting("ComboBoxSelectedPointerOverBackgroundThemeBrush", FromString("#FF5F37BE"), FromString("#FF5F37BE")),
                    }),
                new ColorGroup("ContentDialog (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("ContentDialogBackgroundThemeBrush", FromString("LightGray"), FromString("LightGray")),
                        // new ColorSetting("ContentDialogBorderThemeBrush", FromString("{ThemeResource SystemAccentColor}"), FromString("{ThemeResource SystemAccentColor}")),
                        new ColorSetting("ContentDialogContentForegroundBrush", FromString("Black"), FromString("Black")),
                        new ColorSetting("ContentDialogDimmingThemeBrush", FromString("#99FFFFFF"), FromString("#99FFFFFF")),
                    }),
                new ColorGroup("DatePicker (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("DatePickerHeaderForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("DatePickerForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                    }),
                new ColorGroup("DefaultText (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("DefaultTextForegroundThemeBrush", FromString("Black"), FromString("White")),
                    }),
                new ColorGroup("FlipView (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("FlipViewButtonBackgroundThemeBrush", FromString("#59D5D5D5"), FromString("#59D5D5D5")),
                        new ColorSetting("FlipViewButtonBorderThemeBrush", FromString("#59D5D5D5"), FromString("#59D5D5D5")),
                        new ColorSetting("FlipViewButtonForegroundThemeBrush", FromString("#99000000"), FromString("#99000000")),
                        new ColorSetting("FlipViewButtonPointerOverBackgroundThemeBrush", FromString("#F0D7D7D7"), FromString("#F0D7D7D7")),
                        new ColorSetting("FlipViewButtonPointerOverBorderThemeBrush", FromString("#9EC1C1C1"), FromString("#9EC1C1C1")),
                        new ColorSetting("FlipViewButtonPointerOverForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("FlipViewButtonPressedBackgroundThemeBrush", FromString("#BD292929"), FromString("#BD292929")),
                        new ColorSetting("FlipViewButtonPressedBorderThemeBrush", FromString("#BD292929"), FromString("#BD292929")),
                        new ColorSetting("FlipViewButtonPressedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                    }),
                new ColorGroup("Flyout (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("FlyoutBackgroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                        new ColorSetting("FlyoutBorderThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                    }),
                new ColorGroup("FocusVisual (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("FocusVisualBlackStrokeThemeBrush", FromString("Black"), FromString("Black")),
                        new ColorSetting("FocusVisualWhiteStrokeThemeBrush", FromString("White"), FromString("White")),
                    }),
                new ColorGroup("Hyperlink (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("HyperlinkButtonBackgroundThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("HyperlinkButtonBorderThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("HyperlinkDisabledThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("HyperlinkForegroundThemeBrush", FromString("#FF4F1ACB"), FromString("#FF9C72FF")),
                        new ColorSetting("HyperlinkPointerOverForegroundThemeBrush", FromString("#CC4F1ACB"), FromString("#CC9C72FF")),
                        new ColorSetting("HyperlinkPressedForegroundThemeBrush", FromString("#994F1ACB"), FromString("#999C72FF")),
                    }),
                new ColorGroup("HubSection (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("HubSectionHeaderPointerOverForegroundThemeBrush", FromString("#FFD1D1D1"), FromString("#FFD1D1D1")),
                        new ColorSetting("HubSectionHeaderPressedForegroundThemeBrush", FromString("#FF777777"), FromString("#FF777777")),
                    }),
                new ColorGroup("IMECandidate (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("IMECandidateBackgroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("IMECandidateForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("IMECandidatePointerOverBackgroundThemeBrush", FromString("#21000000"), FromString("#21000000")),
                        new ColorSetting("IMECandidatePointerOverForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("IMECandidateSecondaryForegroundThemeBrush", FromString("#FF707070"), FromString("#FF707070")),
                        new ColorSetting("IMECandidateSelectedBackgroundThemeBrush", FromString("#FF4617B4"), FromString("#FF4617B4")),
                        new ColorSetting("IMECandidateSelectedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("IMECandidateListBackgroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("IMECandidateListPagingButtonBackgroundThemeBrush", FromString("#7FD9D9D9"), FromString("#7FD9D9D9")),
                        new ColorSetting("IMECandidateListPagingButtonBorderThemeBrush", FromString("#33000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("IMECandidateListPagingButtonForegroundThemeBrush", FromString("#7F000000"), FromString("#7F000000")),
                        new ColorSetting("IMECandidateListPagingButtonPointerOverBackgroundThemeBrush", FromString("#FFD9D9D9"), FromString("#FFD9D9D9")),
                        new ColorSetting("IMECandidateListPagingButtonPointerOverForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("IMECandidateListPagingButtonPressedBackgroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("IMECandidateListPagingButtonPressedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                    }),
                new ColorGroup("JumpList (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("JumpListDefaultEnabledForeground", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        // new ColorSetting("JumpListDefaultEnabledBackground", FromString("{ThemeResource SystemAccentColor}"), FromString("{ThemeResource SystemAccentColor}")),
                        new ColorSetting("JumpListDefaultDisabledForeground", FromString("#FFA5A5A5"), FromString("#FFFFFFFF")),
                        new ColorSetting("JumpListDefaultDisabledBackground", FromString("#FFDEDEDE"), FromString("#FF1F1F1F")),
                    }),
                new ColorGroup("ListBox (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("ListBoxBackgroundThemeBrush", FromString("#CCFFFFFF"), FromString("#CCFFFFFF")),
                        new ColorSetting("ListBoxBorderThemeBrush", FromString("#45000000"), FromString("Transparent")),
                        new ColorSetting("ListBoxDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("ListBoxFocusBackgroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("ListBoxForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("ListBoxItemDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("ListBoxItemPointerOverBackgroundThemeBrush", FromString("#21000000"), FromString("#21000000")),
                        new ColorSetting("ListBoxItemPointerOverForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("ListBoxItemPressedBackgroundThemeBrush", FromString("#FFD3D3D3"), FromString("#FFD3D3D3")),
                        new ColorSetting("ListBoxItemPressedForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("ListBoxItemSelectedBackgroundThemeBrush", FromString("#FF4617B4"), FromString("#FF4617B4")),
                        new ColorSetting("ListBoxItemSelectedDisabledBackgroundThemeBrush", FromString("#8C000000"), FromString("#66FFFFFF")),
                        new ColorSetting("ListBoxItemSelectedDisabledForegroundThemeBrush", FromString("#99FFFFFF"), FromString("#99000000")),
                        new ColorSetting("ListBoxItemSelectedForegroundThemeBrush", FromString("White"), FromString("White")),
                        new ColorSetting("ListBoxItemSelectedPointerOverBackgroundThemeBrush", FromString("#FF5F37BE"), FromString("#FF5F37BE")),
                    }),
                new ColorGroup("ListPicker (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        // new ColorSetting("ListPickerFlyoutPresenterSelectedItemForegroundThemeBrush", FromString("{ThemeResource SystemColorHighlightColor}"), FromString("{ThemeResource SystemColorHighlightColor}")),
                        new ColorSetting("ListPickerFlyoutPresenterSelectedItemBackgroundThemeBrush", FromString("#00FFFFFF"), FromString("#00000000")),
                    }),
                new ColorGroup("ListView (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("ListViewGroupHeaderForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ListViewGroupHeaderPointerOverForegroundThemeBrush", FromString("#CC000000"), FromString("#CCFFFFFF")),
                        new ColorSetting("ListViewGroupHeaderPressedForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("ListViewItemCheckHintThemeBrush", FromString("#FF4617B4"), FromString("#FFFFFFFF")),
                        new ColorSetting("ListViewItemCheckSelectingThemeBrush", FromString("#FF4617B4"), FromString("#FFFFFFFF")),
                        new ColorSetting("ListViewItemCheckThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("ListViewItemDragBackgroundThemeBrush", FromString("#994617B4"), FromString("#994617B4")),
                        new ColorSetting("ListViewItemDragForegroundThemeBrush", FromString("White"), FromString("White")),
                        new ColorSetting("ListViewItemFocusBorderThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ListViewItemOverlayBackgroundThemeBrush", FromString("#A6000000"), FromString("#A6000000")),
                        new ColorSetting("ListViewItemOverlayForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("ListViewItemOverlaySecondaryForegroundThemeBrush", FromString("#99FFFFFF"), FromString("#99FFFFFF")),
                        new ColorSetting("ListViewItemPlaceholderBackgroundThemeBrush", FromString("#FF3D3D3D"), FromString("#FF3D3D3D")),
                        new ColorSetting("ListViewItemPointerOverBackgroundThemeBrush", FromString("#4D000000"), FromString("#4DFFFFFF")),
                        new ColorSetting("ListViewItemSelectedBackgroundThemeBrush", FromString("#FF4617B4"), FromString("#FF4617B4")),
                        new ColorSetting("ListViewItemSelectedForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ListViewItemSelectedPointerOverBackgroundThemeBrush", FromString("#FF5F37BE"), FromString("#FF5F37BE")),
                        new ColorSetting("ListViewItemSelectedPointerOverBorderThemeBrush", FromString("#FF5F37BE"), FromString("#FF5F37BE")),
                    }),
                new ColorGroup("LoopingSelector (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("LoopingSelectorForegroundThemeBrush", FromString("#40000000"), FromString("#73FFFFFF")),
                        // new ColorSetting("LoopingSelectorSelectionBackgroundThemeBrush", FromString("{ThemeResource SystemColorHighlightColor}"), FromString("{ThemeResource SystemColorHighlightColor}")),
                        new ColorSetting("LoopingSelectorSelectionForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                    }),
                new ColorGroup("Media (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("MediaButtonForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("MediaButtonBackgroundThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("MediaButtonPointerOverForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("MediaButtonPointerOverBackgroundThemeBrush", FromString("#21FFFFFF"), FromString("#21FFFFFF")),
                        new ColorSetting("MediaButtonPressedForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("MediaButtonPressedBackgroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("MediaButtonPressedBorderThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("MediaControlPanelVideoThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("MediaControlPanelAudioThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("MediaDownloadProgressIndicatorThemeBrush", FromString("#38FFFFFF"), FromString("#38FFFFFF")),
                        new ColorSetting("MediaErrorBackgroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("MediaTextThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                    }),
                new ColorGroup("MenuFlyout (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("MenuFlyoutItemFocusedBackgroundThemeBrush", FromString("#FFE5E5E5"), FromString("#FF212121")),
                        new ColorSetting("MenuFlyoutItemFocusedForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("MenuFlyoutItemDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("MenuFlyoutItemPointerOverBackgroundThemeBrush", FromString("#FFE5E5E5"), FromString("#FF212121")),
                        new ColorSetting("MenuFlyoutItemPointerOverForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("MenuFlyoutItemPressedBackgroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("MenuFlyoutItemPressedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                    }),
                new ColorGroup("Pivot (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("PivotForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("PivotHeaderBackgroundSelectedBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("PivotHeaderBackgroundUnselectedBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("PivotHeaderForegroundSelectedBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("PivotHeaderForegroundUnselectedBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("PivotNavButtonBackgroundThemeBrush", FromString("#59D5D5D5"), FromString("#59D5D5D5")),
                        new ColorSetting("PivotNavButtonBorderThemeBrush", FromString("#59D5D5D5"), FromString("#59D5D5D5")),
                        new ColorSetting("PivotNavButtonForegroundThemeBrush", FromString("#99000000"), FromString("#99000000")),
                        new ColorSetting("PivotNavButtonPointerOverBackgroundThemeBrush", FromString("#F0D7D7D7"), FromString("#F0D7D7D7")),
                        new ColorSetting("PivotNavButtonPointerOverBorderThemeBrush", FromString("#9EC1C1C1"), FromString("#9EC1C1C1")),
                        new ColorSetting("PivotNavButtonPointerOverForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("PivotNavButtonPressedBackgroundThemeBrush", FromString("#BD292929"), FromString("#BD292929")),
                        new ColorSetting("PivotNavButtonPressedBorderThemeBrush", FromString("#BD292929"), FromString("#BD292929")),
                        new ColorSetting("PivotNavButtonPressedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                    }),
                new ColorGroup("MenuFlyout (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("MenuFlyoutSeparatorThemeBrush", FromString("#FF7A7A7A"), FromString("#FF7A7A7A")),
                    }),
                new ColorGroup("ProgressBar (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("ProgressBarBackgroundThemeBrush", FromString("#30000000"), FromString("#59FFFFFF")),
                        new ColorSetting("ProgressBarBorderThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("ProgressBarForegroundThemeBrush", FromString("#FF4617B4"), FromString("#FF5B2EC5")),
                        new ColorSetting("ProgressBarIndeterminateForegroundThemeBrush", FromString("#FF4617B4"), FromString("#FF8A57FF")),
                    }),
                new ColorGroup("RadioButton (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("RadioButtonBackgroundThemeBrush", FromString("#CCFFFFFF"), FromString("#CCFFFFFF")),
                        new ColorSetting("RadioButtonBorderThemeBrush", FromString("#45000000"), FromString("#CCFFFFFF")),
                        new ColorSetting("RadioButtonContentDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("RadioButtonContentForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("RadioButtonDisabledBackgroundThemeBrush", FromString("#66CACACA"), FromString("#66FFFFFF")),
                        new ColorSetting("RadioButtonDisabledBorderThemeBrush", FromString("#26000000"), FromString("#66FFFFFF")),
                        new ColorSetting("RadioButtonDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66000000")),
                        new ColorSetting("RadioButtonForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("RadioButtonPointerOverBackgroundThemeBrush", FromString("#DEFFFFFF"), FromString("#DEFFFFFF")),
                        new ColorSetting("RadioButtonPointerOverBorderThemeBrush", FromString("#70000000"), FromString("#DEFFFFFF")),
                        new ColorSetting("RadioButtonPointerOverForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("RadioButtonPressedBackgroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("RadioButtonPressedBorderThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("RadioButtonPressedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                    }),
                new ColorGroup("RepeatButton (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("RepeatButtonBorderThemeBrush", FromString("#33000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("RepeatButtonDisabledBackgroundThemeBrush", FromString("#66CACACA"), FromString("Transparent")),
                        new ColorSetting("RepeatButtonDisabledBorderThemeBrush", FromString("#1A000000"), FromString("#66FFFFFF")),
                        new ColorSetting("RepeatButtonDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("RepeatButtonForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("RepeatButtonPointerOverBackgroundThemeBrush", FromString("#D1CDCDCD"), FromString("#21FFFFFF")),
                        new ColorSetting("RepeatButtonPointerOverForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("RepeatButtonPressedBackgroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("RepeatButtonPressedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                    }),
                new ColorGroup("ScrollBar (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("ScrollBarButtonForegroundThemeBrush", FromString("#99000000"), FromString("#99000000")),
                        new ColorSetting("ScrollBarButtonPointerOverBackgroundThemeBrush", FromString("#FFDADADA"), FromString("#FFDADADA")),
                        new ColorSetting("ScrollBarButtonPointerOverBorderThemeBrush", FromString("#FFDADADA"), FromString("#FFDADADA")),
                        new ColorSetting("ScrollBarButtonPointerOverForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("ScrollBarButtonPressedBackgroundThemeBrush", FromString("#99000000"), FromString("#99000000")),
                        new ColorSetting("ScrollBarButtonPressedBorderThemeBrush", FromString("#99000000"), FromString("#99000000")),
                        new ColorSetting("ScrollBarButtonPressedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("ScrollBarPanningBackgroundThemeBrush", FromString("#FFCDCDCD"), FromString("#FFCDCDCD")),
                        new ColorSetting("ScrollBarPanningBorderThemeBrush", FromString("#7D9A9A9A"), FromString("#7D9A9A9A")),
                        new ColorSetting("ScrollBarThumbBackgroundThemeBrush", FromString("#FFCDCDCD"), FromString("#FFCDCDCD")),
                        new ColorSetting("ScrollBarThumbBorderThemeBrush", FromString("#3B555555"), FromString("#3B555555")),
                        new ColorSetting("ScrollBarThumbPointerOverBackgroundThemeBrush", FromString("#FFDADADA"), FromString("#FFDADADA")),
                        new ColorSetting("ScrollBarThumbPointerOverBorderThemeBrush", FromString("#6BB7B7B7"), FromString("#6BB7B7B7")),
                        new ColorSetting("ScrollBarThumbPressedBackgroundThemeBrush", FromString("#99000000"), FromString("#99000000")),
                        new ColorSetting("ScrollBarThumbPressedBorderThemeBrush", FromString("#ED555555"), FromString("#ED555555")),
                        new ColorSetting("ScrollBarTrackBackgroundThemeBrush", FromString("#59D5D5D5"), FromString("#59D5D5D5")),
                        new ColorSetting("ScrollBarTrackBorderThemeBrush", FromString("#59D5D5D5"), FromString("#59D5D5D5")),
                    }),
                new ColorGroup("SearchBox (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("SearchBoxBackgroundThemeBrush", FromString("#CCFFFFFF"), FromString("#CCFFFFFF")),
                        new ColorSetting("SearchBoxBorderThemeBrush", FromString("#FF2A2A2A"), FromString("#FF2A2A2A")),
                        new ColorSetting("SearchBoxDisabledBackgroundThemeBrush", FromString("#66CACACA"), FromString("Transparent")),
                        new ColorSetting("SearchBoxDisabledTextThemeBrush", FromString("#38000000"), FromString("#66FFFFFF")),
                        new ColorSetting("SearchBoxDisabledBorderThemeBrush", FromString("#26000000"), FromString("#FF666666")),
                        new ColorSetting("SearchBoxPointerOverBackgroundThemeBrush", FromString("#DDFFFFFF"), FromString("#DDFFFFFF")),
                        new ColorSetting("SearchBoxPointerOverTextThemeBrush", FromString("#99000000"), FromString("#99000000")),
                        new ColorSetting("SearchBoxPointerOverBorderThemeBrush", FromString("#88000000"), FromString("#FFDDDDDD")),
                        new ColorSetting("SearchBoxFocusedBackgroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("SearchBoxFocusedTextThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("SearchBoxFocusedBorderThemeBrush", FromString("#FF2A2A2A"), FromString("#FF2A2A2A")),
                        new ColorSetting("SearchBoxButtonBackgroundThemeBrush", FromString("#FF4617B4"), FromString("#FF4617B4")),
                        new ColorSetting("SearchBoxButtonForegroundThemeBrush", FromString("White"), FromString("White")),
                        new ColorSetting("SearchBoxButtonPointerOverForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("SearchBoxButtonPointerOverBackgroundThemeBrush", FromString("#FF5F37BE"), FromString("#FF5F37BE")),
                        new ColorSetting("SearchBoxSeparatorSuggestionForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("SearchBoxHitHighlightForegroundThemeBrush", FromString("#FF4617B4"), FromString("#FF4617B4")),
                        new ColorSetting("SearchBoxHitHighlightSelectedForegroundThemeBrush", FromString("#FFA38BDA"), FromString("#FFA38BDA")),
                        new ColorSetting("SearchBoxIMECandidateListSeparatorThemeBrush", FromString("#FF2A2A2A"), FromString("#FF2A2A2A")),
                        new ColorSetting("SearchBoxForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                    }),
                new ColorGroup("SemanticZoom (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("SemanticZoomButtonBackgroundThemeBrush", FromString("#59D5D5D5"), FromString("#59D5D5D5")),
                        new ColorSetting("SemanticZoomButtonBorderThemeBrush", FromString("#59D5D5D5"), FromString("#59D5D5D5")),
                        new ColorSetting("SemanticZoomButtonForegroundThemeBrush", FromString("#99000000"), FromString("#99000000")),
                        new ColorSetting("SemanticZoomButtonPointerOverBackgroundThemeBrush", FromString("#FFDADADA"), FromString("#FFDADADA")),
                        new ColorSetting("SemanticZoomButtonPointerOverBorderThemeBrush", FromString("#FFDADADA"), FromString("#FFDADADA")),
                        new ColorSetting("SemanticZoomButtonPointerOverForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("SemanticZoomButtonPressedBackgroundThemeBrush", FromString("#99000000"), FromString("#99000000")),
                        new ColorSetting("SemanticZoomButtonPressedBorderThemeBrush", FromString("#99000000"), FromString("#99000000")),
                        new ColorSetting("SemanticZoomButtonPressedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                    }),
                new ColorGroup("SettingFlyout (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("SettingsFlyoutBackgroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                        new ColorSetting("SettingsFlyoutBackButtonPointerOverBackgroundThemeBrush", FromString("#21FFFFFF"), FromString("#21FFFFFF")),
                        new ColorSetting("SettingsFlyoutHeaderBackgroundThemeBrush", FromString("#FF464646"), FromString("#FF464646")),
                        new ColorSetting("SettingsFlyoutHeaderForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                    }),
                new ColorGroup("Slider (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("SliderBorderThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("SliderDisabledBorderThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("SliderThumbBackgroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("SliderThumbBorderThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("SliderThumbDisabledBackgroundThemeBrush", FromString("#FF929292"), FromString("#FF7E7E7E")),
                        new ColorSetting("SliderThumbPointerOverBackgroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("SliderThumbPointerOverBorderThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("SliderThumbPressedBackgroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("SliderThumbPressedBorderThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("SliderTickMarkInlineBackgroundThemeBrush", FromString("White"), FromString("Black")),
                        new ColorSetting("SliderTickMarkInlineDisabledForegroundThemeBrush", FromString("White"), FromString("Black")),
                        new ColorSetting("SliderTickmarkOutsideBackgroundThemeBrush", FromString("#80000000"), FromString("#80FFFFFF")),
                        new ColorSetting("SliderTickMarkOutsideDisabledForegroundThemeBrush", FromString("#80000000"), FromString("#80FFFFFF")),
                        new ColorSetting("SliderTrackBackgroundThemeBrush", FromString("#1A000000"), FromString("#29FFFFFF")),
                        new ColorSetting("SliderTrackDecreaseBackgroundThemeBrush", FromString("#FF4617B4"), FromString("#FF5B2EC5")),
                        new ColorSetting("SliderTrackDecreaseDisabledBackgroundThemeBrush", FromString("#1C000000"), FromString("#1FFFFFFF")),
                        new ColorSetting("SliderTrackDecreasePointerOverBackgroundThemeBrush", FromString("#FF5F37BE"), FromString("#FF724BCD")),
                        new ColorSetting("SliderTrackDecreasePressedBackgroundThemeBrush", FromString("#FF7241E4"), FromString("#FF8152EF")),
                        new ColorSetting("SliderTrackDisabledBackgroundThemeBrush", FromString("#1A000000"), FromString("#29FFFFFF")),
                        new ColorSetting("SliderTrackPointerOverBackgroundThemeBrush", FromString("#26000000"), FromString("#46FFFFFF")),
                        new ColorSetting("SliderTrackPressedBackgroundThemeBrush", FromString("#33000000"), FromString("#59FFFFFF")),
                        new ColorSetting("SliderHeaderForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                    }),
                new ColorGroup("TextBox (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("TextBoxForegroundHeaderThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("TextBoxPlaceholderTextThemeBrush", FromString("#AB000000"), FromString("#AB000000")),
                        new ColorSetting("TextBoxBackgroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("TextSelectionHighlightColorThemeBrush", FromString("#FF4617b4"), FromString("#FF4617b4")),
                        new ColorSetting("TextBoxBorderThemeBrush", FromString("#A3000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("TextBoxButtonBackgroundThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("TextBoxButtonBorderThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("TextBoxButtonForegroundThemeBrush", FromString("#99000000"), FromString("#99000000")),
                        new ColorSetting("TextBoxButtonPointerOverBackgroundThemeBrush", FromString("#FFDEDEDE"), FromString("#FFDEDEDE")),
                        new ColorSetting("TextBoxButtonPointerOverBorderThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("TextBoxButtonPointerOverForegroundThemeBrush", FromString("Black"), FromString("#FF000000")),
                        new ColorSetting("TextBoxButtonPressedBackgroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                        new ColorSetting("TextBoxButtonPressedBorderThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("TextBoxButtonPressedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("TextBoxDisabledBackgroundThemeBrush", FromString("#66CACACA"), FromString("Transparent")),
                        new ColorSetting("TextBoxDisabledBorderThemeBrush", FromString("#26000000"), FromString("#66FFFFFF")),
                        new ColorSetting("TextBoxDisabledForegroundThemeBrush", FromString("#FF666666"), FromString("#FF666666")),
                        new ColorSetting("TextBoxForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                    }),
                new ColorGroup("Thumb (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("ThumbBackgroundThemeBrush", FromString("#FFCDCDCD"), FromString("#FFCDCDCD")),
                        new ColorSetting("ThumbBorderThemeBrush", FromString("#3B555555"), FromString("#3B555555")),
                        new ColorSetting("ThumbPointerOverBackgroundThemeBrush", FromString("#FFDADADA"), FromString("#FFDADADA")),
                        new ColorSetting("ThumbPointerOverBorderThemeBrush", FromString("#6BB7B7B7"), FromString("#6BB7B7B7")),
                        new ColorSetting("ThumbPressedBackgroundThemeBrush", FromString("#99000000"), FromString("#99000000")),
                        new ColorSetting("ThumbPressedBorderThemeBrush", FromString("#ED555555"), FromString("#ED555555")),
                    }),
                new ColorGroup("TimePicker (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("TimePickerHeaderForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("TimePickerForegroundThemeBrush", FromString("#FF000000"), FromString("#FF000000")),
                    }),
                new ColorGroup("ToggleButton (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("ToggleButtonBackgroundThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("ToggleButtonBorderThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToggleButtonCheckedBackgroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToggleButtonCheckedBorderThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToggleButtonCheckedDisabledBackgroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("ToggleButtonCheckedDisabledForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                        new ColorSetting("ToggleButtonCheckedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                        new ColorSetting("ToggleButtonCheckedPointerOverBackgroundThemeBrush", FromString("#99000000"), FromString("#99FFFFFF")),
                        new ColorSetting("ToggleButtonCheckedPointerOverBorderThemeBrush", FromString("#99000000"), FromString("#99FFFFFF")),
                        new ColorSetting("ToggleButtonCheckedPressedBackgroundThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("ToggleButtonCheckedPressedBorderThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToggleButtonCheckedPressedForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToggleButtonDisabledBorderThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("ToggleButtonDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("ToggleButtonForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToggleButtonPointerOverBackgroundThemeBrush", FromString("#21000000"), FromString("#21FFFFFF")),
                        new ColorSetting("ToggleButtonPressedBackgroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToggleButtonPressedForegroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FF000000")),
                    }),
                new ColorGroup("ToggleSwitch (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("ToggleSwitchCurtainBackgroundThemeBrush", FromString("#FF4617B4"), FromString("#FF5729C1")),
                        new ColorSetting("ToggleSwitchCurtainDisabledBackgroundThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("ToggleSwitchCurtainPointerOverBackgroundThemeBrush", FromString("#FF5F37BE"), FromString("#FF6E46CA")),
                        new ColorSetting("ToggleSwitchCurtainPressedBackgroundThemeBrush", FromString("#FF7241E4"), FromString("#FF7E4FEC")),
                        new ColorSetting("ToggleSwitchDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("ToggleSwitchForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToggleSwitchHeaderDisabledForegroundThemeBrush", FromString("#66000000"), FromString("#66FFFFFF")),
                        new ColorSetting("ToggleSwitchHeaderForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToggleSwitchOuterBorderBorderThemeBrush", FromString("#59000000"), FromString("#59FFFFFF")),
                        new ColorSetting("ToggleSwitchOuterBorderDisabledBorderThemeBrush", FromString("#33000000"), FromString("#33FFFFFF")),
                        new ColorSetting("ToggleSwitchThumbBackgroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToggleSwitchThumbBorderThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToggleSwitchThumbDisabledBackgroundThemeBrush", FromString("#FF929292"), FromString("#FF7E7E7E")),
                        new ColorSetting("ToggleSwitchThumbDisabledBorderThemeBrush", FromString("#FF929292"), FromString("#FF7E7E7E")),
                        new ColorSetting("ToggleSwitchThumbPointerOverBackgroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToggleSwitchThumbPointerOverBorderThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToggleSwitchThumbPressedBackgroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToggleSwitchThumbPressedForegroundThemeBrush", FromString("#FF000000"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToggleSwitchTrackBackgroundThemeBrush", FromString("#59000000"), FromString("#42FFFFFF")),
                        new ColorSetting("ToggleSwitchTrackBorderThemeBrush", FromString("Transparent"), FromString("Transparent")),
                        new ColorSetting("ToggleSwitchTrackDisabledBackgroundThemeBrush", FromString("#1F000000"), FromString("#1FFFFFFF")),
                        new ColorSetting("ToggleSwitchTrackPointerOverBackgroundThemeBrush", FromString("#4A000000"), FromString("#4AFFFFFF")),
                        new ColorSetting("ToggleSwitchTrackPressedBackgroundThemeBrush", FromString("#42000000"), FromString("#59FFFFFF")),
                    }),
                new ColorGroup("Tooltip (Legacy)", FromString("#FFFFFFFF"),
                    new[]
                    {
                        new ColorSetting("ToolTipBackgroundThemeBrush", FromString("#FFFFFFFF"), FromString("#FFFFFFFF")),
                        new ColorSetting("ToolTipBorderThemeBrush", FromString("#FF808080"), FromString("#FF808080")),
                        new ColorSetting("ToolTipForegroundThemeBrush", FromString("#FF666666"), FromString("#FF666666"))
                    })
            };

            var lightBase = new HSLColor("#FFFFFFFF");
            var darkBase = new HSLColor("#FF000000");

            foreach (var color in ColorGroups)
            {
                // THIS IS CURRENTLY NOT USED, when we start to use it
                // we probably need two base colors for both themes (Light, Dark)
                // var baseColor = new HSLColor(color.BaseColor);

                foreach (var setting in color.Colors)
                {
                    var lightColor = new HSLColor(setting.LightColor);
                    var darkColor = new HSLColor(setting.DarkColor);

                    // trying to compute the "Factor" automatically, this does not really work
                    setting.OriginalDarkFactor = setting.DarkFactor = (darkColor.Luminosity - darkBase.Luminosity);
                    setting.OriginalLightFactor = setting.LightFactor = (lightColor.Luminosity - lightBase.Luminosity);
                }
            }
        }

        private Color FromString(string str)
        {
            return ColorExtensions.FromString(str);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
