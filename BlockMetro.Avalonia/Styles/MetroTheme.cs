using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using System;

namespace BlockMetro.Avalonia.Styles
{
    public class MetroTheme : Style
    {
        public static readonly StyledProperty<string> ThemeProperty = AvaloniaProperty.Register<MetroTheme, string>(
            "Theme", default(string));

        public string Theme
        {
            get { return GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        public MetroTheme()
        {
            ThemeProperty.Changed.AddClassHandler<MetroTheme>((x, e) => x.OnThemeChanged(e));
        }

        private void OnThemeChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var theme = (string)e.NewValue;

            var ThemeUri = theme switch
            {
                "Dark" => new Uri("avares://BlockMetro.Avalonia/Styles/Themes/Dark/Accent/AccentDefault.axaml", UriKind.Absolute),
                "Light" => new Uri("avares://BlockMetro.Avalonia/Styles/Themes/Light/Accent/AccentDefault.axaml", UriKind.Absolute),
                _ => new Uri("avares://BlockMetro.Avalonia/Styles/Themes/Light/Accent/AccentDefault.axaml", UriKind.Absolute),
            };
            var dict = new ResourceInclude(ThemeUri);
            dict.Source = ThemeUri;
            //throw new StackOverflowException("我快崩溃了啊啊啊啊啊");

            // Remove the old dictionary
            Application.Current.Resources.MergedDictionaries.Clear();

            // Add the new dictionary
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }
    }
}
