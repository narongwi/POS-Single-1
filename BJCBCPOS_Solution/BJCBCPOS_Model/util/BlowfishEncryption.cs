using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.Encoders;

namespace BJCBCPOS_Model
{
    public static class BlowfishEncryption
    {
        public static string decrypt(string message)
        {
            BlowfishEngine engine = new BlowfishEngine();
            PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(engine);

            cipher.Init(false, new KeyParameter(Encoding.GetEncoding(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ANSICodePage).GetBytes("mis@bigc")));

            byte[] out1 = Hex.Decode(message);
            byte[] out2 = new byte[cipher.GetOutputSize(out1.Length)];

            int len2 = cipher.ProcessBytes(out1, 0, out1.Length, out2, 0);

            cipher.DoFinal(out2, len2);

            int index = 0;
            while (index < out2.Length && out2[index] > 0) {
                index++;
            }

            return Encoding.Default.GetString(out2, 0, index);
        }
    }
}
