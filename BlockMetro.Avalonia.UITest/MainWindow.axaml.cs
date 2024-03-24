using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using BlockMetro.Avalonia.Styles;
using System;
using System.Diagnostics;

namespace BlockMetro.Avalonia.UITest
{
    public partial class MainWindow : Window
    {
        MetroTheme metroTheme = new MetroTheme();
        public MainWindow()
        {
            metroTheme.Theme = ThemePropertyValue.Light;
            metroTheme.Accent = AccentPropertyValue.BrightBlueAccent;
            InitializeComponent();
            Debug.WriteLine(this.ActualThemeVariant);
        }
        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            metroTheme.Accent = AccentPropertyValue.GrassBlockGreenAccent;
        }
    }
}