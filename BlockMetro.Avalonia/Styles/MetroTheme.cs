using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        BrightBlue,
        BrightRed,
        GrassBlockGreen,
        CreeperGreen,
        Gold,
        Golden,
        BrightOrangeYellow,
        DeepOrangeYellow,
        Rust,
        LightRust,
        BrickRed,
        MediumRed,
        LightRed,
        Red,
        BrightMagenta,
        RoseRed,
        LightPurpleRed,
        PlumRed,
        LightOrchidPurple,
        Blue,
        DeepBlue,
        PurpleShadow,
        DeepPurpleShadow,
        SoftRainbow,
        IrisFlower,
        LightPurpleR,
        PurpleRed,
        BrightCoolBlue,
        CoolBlue,
        SeaFoamGreen,
        Teal,
        LightMint,
        DeepMint,
        LightGreen,
        SportGreen,
        Gray,
        GrayBrown,
        SteelBlue,
        MetallicBlue,
        LightMoss,
        Moss,
        GrassGreen,
        Green,
        SkyGray,
        FogGray,
        BlueGray,
        DeepGray,
        BrightGreen,
        FairyTeaGreen,
        DesertCamouflage,
        ProtectiveColor
    }

    public class MetroTheme : Style
    { 
        public static readonly StyledProperty<ThemePropertyValue?> ThemeProperty = AvaloniaProperty.Register<MetroTheme, ThemePropertyValue?>(nameof(Theme), null);
        public static readonly StyledProperty<AccentPropertyValue?> AccentProperty = AvaloniaProperty.Register<MetroTheme, AccentPropertyValue?>(nameof(Accent), null);

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

            var ThemeUri = theme switch
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
            var accent = e.NewValue;
            UpdateAccent(accent + "Accent", "GlobalDefaultAccent");
            UpdateAccent(accent + "SecondaryAccent", "GlobalSecondaryAccent");
            UpdateAccent(accent + "TertiaryAccent", "GlobalTertiaryAccent");
        }
        private void UpdateAccent(string AccentKey, string GlobalAccent)
        {
            var GetAccent = Application.Current.FindResource(AccentKey);

            SolidColorBrush AccentBrush = new SolidColorBrush();

            AccentBrush.Color = Color.Parse(GetAccent.ToString());

            Application.Current.Resources.Remove(GlobalAccent);

            Application.Current.Resources.Add(GlobalAccent, AccentBrush);
        }
    }
}
