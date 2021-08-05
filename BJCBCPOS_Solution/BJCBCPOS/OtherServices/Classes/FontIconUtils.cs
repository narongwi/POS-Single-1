using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace BJCBCPOS.OtherServices.Classes {
    public static class FontIconUtils {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont,uint cbFont,
          IntPtr pdv,[System.Runtime.InteropServices.In] ref uint pcFonts);

        /// <summary>
        /// Store the icon font in a static variable to reuse between icons
        /// </summary>
        public static readonly PrivateFontCollection Fonts = new PrivateFontCollection();

        /// <summary>
        /// Loads the icon font from the resources.
        /// </summary>
        public static void InitialiseFont() {
            try {
                if(Fonts.Families.Count() == 0) {
                    var resource = "BJCBCPOS.OtherServices.Fonts.materialdesignicons.ttf";
                    var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    using(var fontAsStream = assembly.GetManifestResourceStream(resource)) {
                        if(fontAsStream != null) {
                            byte[] fontAsByte = new byte[fontAsStream.Length];
                            fontAsStream.Read(fontAsByte,0,(int)fontAsStream.Length);
                            fontAsStream.Close();
                            IntPtr memPointer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * fontAsByte.Length);
                            Marshal.Copy(fontAsByte,0,memPointer,fontAsByte.Length);
                            Fonts.AddMemoryFont(memPointer,fontAsByte.Length);
                            //AddFontMemResourceEx(memPointer,(uint)fontAsByte.Length,IntPtr.Zero,ref dummy);
                        }
                    }
                }
               
            } catch(Exception ex) {
                Console.WriteLine(ex);
            }
        }

    }
}
