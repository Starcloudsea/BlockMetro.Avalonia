using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace BlockMetro.Avalonia.Styles
{
    public enum ThemePropertyValue
    {
        Light,
        Dark
        
    }
    public enum AccentPropertyValue
    {
        BrightBlueAccent,
        BrightRedAccent,
        GrassBlockGreenAccent,
        CreeperGreenAccent,
        GoldAccent,
        GoldenAccent,
        BrightOrangeYellowAccent,
        DeepOrangeYellowAccent,
        RustAccent,
        LightRustAccent,
        BrickRedAccent,
        MediumRedAccent,
        LightRedAccent,
        RedAccent,
        BrightMagentaAccent,
        RoseRedAccent,
        LightPurpleRedAccent,
        PlumRedAccent,
        LightOrchidPurpleAccent,
        BlueAccent,
        DeepBlueAccent,
        PurpleShadowAccent,
        DeepPurpleShadowAccent,
        SoftRainbowAccent,
        IrisFlowerAccent,
        LightPurpleRAccent,
        PurpleRedAccent,
        BrightCoolBlueAccent,
        CoolBlueAccent,
        SeaFoamGreenAccent,
        TealAccent,
        LightMintAccent,
        DeepMintAccent,
        LightGreenAccent,
        SportGreenAccent,
        GrayAccent,
        GrayBrownAccent,
        SteelBlueAccent,
        MetallicBlueAccent,
        LightMossAccent,
        MossAccent,
        GrassGreenAccent,
        GreenAccent,
        SkyGrayAccent,
        FogGrayAccent,
        BlueGrayAccent,
        DeepGrayAccent,
        BrightGreenAccent,
        FairyTeaGreenAccent,
        DesertCamouflageAccent,
        ProtectiveColorAccent

    }

    public class MetroTheme : Style
    {
        Uri ThemeUri { get; set; }

        public static readonly StyledProperty<ThemePropertyValue?> ThemeProperty = AvaloniaProperty.Register<MetroTheme, ThemePropertyValue?>("Theme", null);
        public static readonly StyledProperty<AccentPropertyValue?> AccentProperty = AvaloniaProperty.Register<MetroTheme, AccentPropertyValue?>("Accent", null);


        public ThemePropertyValue? Theme
        {
            get { return GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }
        public AccentPropertyValue? Accent
        {
            get { return GetValue(AccentProperty); }
            set { SetValue(AccentProperty, value); }
        }

        public MetroTheme()
        {
            ThemeProperty.Changed.AddClassHandler<MetroTheme>((x, e) => x.OnThemeChanged(e));
            AccentProperty.Changed.AddClassHandler<MetroTheme>((x, e) => x.OnAccentChanged(e));
        }

        private void OnThemeChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var theme = (ThemePropertyValue?)e.NewValue;

            ThemeUri = theme switch
            {
                ThemePropertyValue.Dark => new Uri("avares://BlockMetro.Avalonia/Styles/Themes/Dark/Dark.axaml", UriKind.Absolute),
                ThemePropertyValue.Light => new Uri("avares://BlockMetro.Avalonia/Styles/Themes/Light/Light.axaml", UriKind.Absolute),
                _ => throw new ArgumentException($"The value provided by the Theme property is not defined in {typeof(ThemePropertyValue).Namespace}.ThemePropertyValue, which may occur because of a problem in assigning a value to the Theme property."),
            };
            var StyleUri = new Uri("avares://BlockMetro.Avalonia/Styles/ControlsStyle/ControlsStyle.axaml", UriKind.Absolute);

            var applyTheme = new ResourceInclude(ThemeUri);
            applyTheme.Source = ThemeUri;

            var applyStyle = new StyleInclude(StyleUri);
            applyStyle.Source = StyleUri;
            //throw new StackOverflowException("你 库 炸 了 OwO");

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Styles.Clear();

            // Add the new dictionary
            Application.Current.Resources.MergedDictionaries.Add(applyTheme);
            Application.Current.Styles.Add(applyStyle);
        }
        private void OnAccentChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var accent = (AccentPropertyValue?)e.NewValue;
            var GetAccent = Application.Current.FindResource(accent.ToString());

            SolidColorBrush AccentBrush = new SolidColorBrush();
            AccentBrush.Color = Color.Parse(GetAccent.ToString());

            Application.Current.Resources.Remove("GlobalDefaultAccent");

            Application.Current.Resources.Add("GlobalDefaultAccent", AccentBrush);
        }
    }
}
