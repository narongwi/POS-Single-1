using BJCBCPOS.OtherServices.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;

namespace BJCBCPOS.OtherServices.Fonts {
   public static class FontIconHelper {
        //private static readonly Lazy<PrivateFontCollection> Fonts = new Lazy<PrivateFontCollection>(InitializeFonts);
        //private static readonly Lazy<FontFamily> FallbackFont = new Lazy<FontFamily>(() => Fonts.Value.Families[0]);
        internal const int DefaultSize = 30;
        private static readonly Color DefaultColor = SystemColors.WindowText;
        private static readonly Brush DefaultBrush = new SolidBrush(DefaultColor);

        public static Bitmap ToBitmap<FontAwensomeSolid>(this FontFamily fontFamily,FontAwensomeSolid icon,int width,int height,Color? color = null,bool iconBold = false)
         where FontAwensomeSolid : struct, IConvertible, IComparable, IFormattable {
            var bitmap = new Bitmap(width,height);
            using(var graphics = Graphics.FromImage(bitmap)) {
                var text = icon.ToChar();
                var font = graphics.GetAdjustedIconFont(fontFamily,text,new SizeF(width,height),iconBold);
                var brush = color.HasValue ? new SolidBrush(color.Value) : DefaultBrush;
                DrawIcon(graphics,font,text,width,height,brush);
            }
            return bitmap;
        }
        private static PointF GetTopLeft(this Graphics graphics,string text,Font font,SizeF size) {
            var iconSize = graphics.GetIconSize(text,font,size);
            // center icon
            var left = Math.Max(0f,(size.Width - iconSize.Width) / 2);
            var top = Math.Max(0f,(size.Height - iconSize.Height) / 2);
            return new PointF(left,top);
        }

        public static string ToChar<FontAwensomeSolid>(this FontAwensomeSolid icon) where FontAwensomeSolid : struct, IConvertible, IComparable, IFormattable {
            return char.ConvertFromUtf32(icon.UniCode());
        }

        private static int UniCode<TEnum>(this TEnum icon)
           where TEnum : struct, IConvertible, IComparable, IFormattable {
            return icon.ToInt32(System.Globalization.CultureInfo.InvariantCulture);
        }
        private static Font GetAdjustedIconFont(this Graphics g,FontFamily fontFamily,string text,
           SizeF size,bool iconBold = false,int maxFontSize = 0,int minFontSize = 4,bool smallestOnFail = true) {
            var safeMaxFontSize = maxFontSize > 0 ? maxFontSize : size.Height;
            for(double adjustedSize = safeMaxFontSize ; adjustedSize >= minFontSize ; adjustedSize -= 0.5) {
                var font = GetIconFont(fontFamily,(float)adjustedSize,iconBold);
                // Test the string with the new size
                var iconSize = g.GetIconSize(text,font,size);
                if(iconSize.Width < size.Width && iconSize.Height < size.Height)
                    return font;
            }

            // Could not find a font size
            // return min or max or maxFontSize?
            return GetIconFont(fontFamily,smallestOnFail ? minFontSize : maxFontSize);
        }
        public static void DrawIcon(this Graphics graphics,Font font,string text, int width = DefaultSize,int height = DefaultSize,Brush brush = null) {
                    // Set best quality
                    graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PageUnit = GraphicsUnit.Pixel;

                    var topLeft = graphics.GetTopLeft(text,font,new SizeF(width,height));

                    graphics.DrawString(text,font,brush ?? DefaultBrush,topLeft);
        }
        private static Font GetIconFont(FontFamily fontFamily,float size,bool iconBold = false) {
            return new Font(fontFamily,size,iconBold? FontStyle.Bold: FontStyle.Regular,GraphicsUnit.Point);
            //return new Font(fontFamily,size,FontStyle.Bold,GraphicsUnit.Point);

        }
        private static Font GetIconFont(float size) {
            return new Font(FontIconUtils.Fonts.Families[0],size,GraphicsUnit.Point);
        }
        private static SizeF GetIconSize(this Graphics graphics,string text,Font font,SizeF size) {
            var format = new StringFormat();
            var ranges = new[] { new CharacterRange(0,text.Length) };
            format.SetMeasurableCharacterRanges(ranges);
            format.Alignment = StringAlignment.Center;
            var iconSize = graphics.MeasureString(text,font,size,format);
            return iconSize;
        }
    }
}
