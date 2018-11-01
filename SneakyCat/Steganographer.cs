using System;
using System.Linq;
using System.Drawing;
using System.IO;

namespace SneakyCat
{
    class Steganographer
    {


        private static byte getPwdByte(string pwdStr)
        {
            byte ret = 0x00;
            if (pwdStr != null)
            {
                ret = (byte)(pwdStr.GetHashCode());
            }
            return ret;
        }


        private static byte[] xorr(byte[] embedData, string pwdStr)
        {
            if (pwdStr == null) return embedData;
            byte pwdByt = getPwdByte(pwdStr);
            byte[] encodedData = new byte[embedData.Length];
            for (int i = 0; i < embedData.Length; i++)
            {
                encodedData[i] = (byte)(embedData[i] ^ pwdByt);
            }
            return encodedData.ToArray();
        }
        

        private static Color getEmbeddedPixel(Color pixel, byte dataToEmbed)
        {
            byte pR = pixel.R;
            byte pG = pixel.G;
            byte pB = pixel.B;

            byte embedByte;

            embedByte = (byte)(dataToEmbed & 0x0F);
            dataToEmbed >>= 4;
            pR &= 0xF0;
            pR |= embedByte;

            embedByte = (byte)(dataToEmbed & 0x03);
            dataToEmbed >>= 2;
            pG &= 0xFC;
            pG |= embedByte;

            embedByte = (byte)(dataToEmbed & 0x03);
            dataToEmbed >>= 2;
            pB &= 0xFC;
            pB |= embedByte;

            Color ePixel = Color.FromArgb(pR, pG, pB);
            return ePixel;
        }


        private static byte getEmbeddedData(Color pixel)
        {
            byte pR = pixel.R;
            byte pG = pixel.G;
            byte pB = pixel.B;

            byte embededData = 0x0;

            byte rR = (byte)(pR & 0x0F);
            byte rG = (byte)(pG & 0x03);
            byte rB = (byte)(pB & 0x03);

            rG <<= 4;
            rB <<= 6;

            embededData = (byte)(rR | rG | rB);
            return embededData;
        }









        /// <summary>
        /// Embed string into file.
        /// </summary>
        /// <param name="str">string to embed</param>
        /// <param name="srcImgP">path of source image</param>
        public static void Encode(string str, string srcImgP, string outP, string pwdStr = null)
        {
            byte[] embedData = System.Text.Encoding.UTF8.GetBytes(str);
            //byte[] embedData = File.ReadAllBytes(embedFileP);

            byte[] encodedData = xorr(embedData, pwdStr);

            Bitmap srcImg = new Bitmap(srcImgP);
            Bitmap embImg = new Bitmap(srcImg.Width, srcImg.Height);

            int count = 0;
            int lim = encodedData.Length;
            for (int i = 0; i < srcImg.Width; i++)
            {
                for (int j = 0; j < srcImg.Height; j++)
                {
                    Color pixel = srcImg.GetPixel(i, j);
                    byte bToEmbed = (count < encodedData.Length) ? encodedData[count] : (byte)0x00;
                    Color ePixel = (count < encodedData.Length) ? getEmbeddedPixel(pixel, bToEmbed) : pixel;
                    embImg.SetPixel(i, j, ePixel);

                    count++;
                }
            }

            embImg.Save(Path.Combine(outP, string.Format("{0}.png", lim)));
        }


        /// <summary>
        /// Decode embedded string. File name should have the number of bytes expected in out string.
        /// </summary>
        /// <param name="imgP">source of img with string embedded</param>
        /// <returns></returns>
        public static byte[] Decode(string imgP, string pwdStr = null)
        {
            int sz = int.Parse(Path.GetFileNameWithoutExtension(imgP));
            byte[] ret = new byte[sz];

            int count = 0;

            Bitmap embImg = new Bitmap(imgP);
            for (int i = 0; i < embImg.Width; i++)
            {
                for (int j = 0; j < embImg.Height; j++)
                {
                    if (count >= sz)
                    {
                        return xorr(ret, pwdStr);
                    }

                    Color pixel = embImg.GetPixel(i, j);
                    byte embededByte = getEmbeddedData(pixel);

                    ret[count] = embededByte;
                    count++;
                }
            }

            return xorr(ret, pwdStr);
        }
    }
}
