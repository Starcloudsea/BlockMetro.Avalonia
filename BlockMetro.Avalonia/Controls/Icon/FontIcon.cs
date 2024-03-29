using Avalonia.Media;
using Avalonia;
using Avalonia.Controls;
using System.Globalization;
using System.Diagnostics;
using System;
using System.Text.RegularExpressions;

namespace BlockMetro.Avalonia.Controls
{
    public class FontIcon : Control
    {
        SolidColorBrush DefaultForegroundBrush = new SolidColorBrush();

        public static readonly StyledProperty<IBrush?> ForegroundProperty = AvaloniaProperty.Register<FontIcon, IBrush?>(
            nameof(Foreground),
            new SolidColorBrush(Color.Parse(Application.Current.FindResource("DefaultTextFill").ToString())));
        public static readonly StyledProperty<string> GlyphProperty = AvaloniaProperty.Register<FontIcon, string>(nameof(Glyph));
        public static readonly StyledProperty<FontFamily> FontFamilyProperty = AvaloniaProperty.Register<FontIcon, FontFamily>("FontFamily", new FontFamily("avares://BlockMetro.Avalonia/Assets/Fonts/Icons#remixicon"));
        public static readonly StyledProperty<int> FontSizeProperty = AvaloniaProperty.Register<FontIcon, int>(nameof(FontSize), 12);
        public static readonly StyledProperty<FontWeight> FontWeightProperty = AvaloniaProperty.Register<FontIcon, FontWeight>("FontWeight", FontWeight.Normal);
        public static readonly StyledProperty<FontStyle> FontStyleProperty = AvaloniaProperty.Register<FontIcon, FontStyle>("FontStyle", FontStyle.Normal);

        private FormattedText textLayout;
        private string GetText;

        public IBrush? Foreground
        {
            get => GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }
        public string Glyph
        {
            get => GetValue(GlyphProperty);
            set => SetValue(GlyphProperty, value);
        }
        public FontFamily fontFamily
        {
            get => GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }
        public int FontSize
        {
            get => GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }
        public FontWeight fontWeight
        {
            get => GetValue(FontWeightProperty);
            set => SetValue(FontWeightProperty, value);
        }
        public FontStyle fontStyle
        {
            get => GetValue(FontStyleProperty);
            set => SetValue(FontStyleProperty, value);
        }
        public FontIcon()
        {
            AddChangeHandler(ForegroundProperty, SetText);
            AddChangeHandler(GlyphProperty, SetText);
            AddChangeHandler(FontFamilyProperty, SetText);
            AddChangeHandler(FontSizeProperty, SetText);
            AddChangeHandler(FontWeightProperty, SetText);
            AddChangeHandler(FontStyleProperty, SetText);
        }

        private void AddChangeHandler<T>(AvaloniaProperty<T> property, Action action)
        {
            property.Changed.AddClassHandler<FontIcon>((sender, e) => action());
        }

        public void SetText()
        {
            if (Glyph != null)
            {
                textLayout = new FormattedText(
                Glyph,
                CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface(
                    fontFamily,
                    fontStyle,
                    fontWeight
                    ),
                FontSize,
                Foreground);

                InvalidateMeasure();
                InvalidateVisual();

            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (textLayout != null)
            {
                var textWidth = textLayout.Width;
                var textHeight = textLayout.Height;

                return new Size(textWidth, textHeight);
            }
            else
            {
                return new Size(0, 0);
            }
        }

        public override void Render(DrawingContext context)
        {
            if (textLayout != null)
            {

                var textWidth = textLayout.Width;
                var textHeight = textLayout.Height;
                var controlWidth = this.Bounds.Width;
                var controlHeight = this.Bounds.Height;
                var textPositionX = (controlWidth - textWidth) / 2;
                var textPositionY = (controlHeight - textHeight) / 2;

                context.DrawText(textLayout, new Point(textPositionX, textPositionY));
            }
        }
    }
}
