using System;
using System.IO;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Windows;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;

namespace Pigeon.Msg
{
    class MessageUtility
    {
        public static string EncodeImage(BitmapImage bmpImage)
        {
            WriteableBitmap writeableBitmap = new WriteableBitmap(bmpImage);
            MemoryStream stream = new MemoryStream();
            writeableBitmap.SaveJpeg(stream, writeableBitmap.PixelWidth,
                writeableBitmap.PixelHeight, 0, 100);
            byte[] imageBytes = stream.ToArray();
            string strPicture = Convert.ToBase64String(imageBytes);
            return strPicture;
        }
        public static BitmapImage DecodeImage(string strImage)
        {
            byte[] imageBytes = Convert.FromBase64String(strImage);
            MemoryStream stream = new MemoryStream(imageBytes);
            BitmapImage bmpImage = new BitmapImage();
            bmpImage.SetSource(stream);
            return bmpImage;
        }

        public static string EncodeVoice(MemoryStream stream)
        {
         byte[] imageBytes = stream.ToArray();
         return  Convert.ToBase64String(imageBytes);
        }

        public static byte[] DecodeVoice(string strVoi)
        {
            return  Convert.FromBase64String(strVoi);
        }

        public static string GetHeader(string body)
        {
            int index = body.IndexOf('@');
            return body.Substring(0, index - 1);
        }
    }
}
