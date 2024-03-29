using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using BlockMetro.Avalonia.Styles;
using System;
using System.Diagnostics;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace BlockMetro.Avalonia.UITest
{
    public partial class MainWindow : Window
    {
        MetroTheme metroTheme = new MetroTheme();
        string result;
        public MainWindow()
        {
            //InvalidateVisual()
            metroTheme.Theme = ThemePropertyValue.Light;
            metroTheme.Accent = AccentPropertyValue.BrightBlue;
            InitializeComponent();
            Debug.WriteLine(this.ActualThemeVariant);
            string unicodeEscape = "&#xE700;";
            // 移除"&#"和";"，保留16进制数字部分
            string hexValue = unicodeEscape.TrimStart(new char[] { '&', '#', 'x'}).TrimEnd(';');
            // 将16进制数字转换为对应的字符
            result = char.ConvertFromUtf32(Convert.ToInt32(hexValue, 16));
            Debug.WriteLine("CCC" + result);
        }
        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            metroTheme.Accent = AccentPropertyValue.BrightRed;
            TEVST.FontSize = 50;
            TEVST.Foreground = Brushes.Aqua;
        }
    }
}