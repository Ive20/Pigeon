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
using System.Device.Location;

namespace Pigeon.Msg
{
    class MessageUtility
    {
        /// <summary>
        /// Converts an image to a string of bytes.
        /// </summary>
        /// <param name="image"> The image to convert. </param>
        /// <returns> A string of code that represents the input image. </returns>
        public static string EncodeImage(BitmapImage image)
        {
            WriteableBitmap writeableBitmap = new WriteableBitmap(image);
            MemoryStream stream = new MemoryStream();
            writeableBitmap.SaveJpeg(stream, writeableBitmap.PixelWidth,
                writeableBitmap.PixelHeight, 0, 100);
            byte[] imageBytes = stream.ToArray();
            string strPicture = Convert.ToBase64String(imageBytes);
            return strPicture;
        }

        /// <summary>
        /// Converts a string of bytes to image. 
        /// </summary>
        /// <param name="imageString"> The string of bytes to be converted. </param>
        /// <returns> An image that the input string of code represents. </returns>
        public static BitmapImage DecodeImage(string imageString)
        {
            byte[] imageBytes = Convert.FromBase64String(imageString);
            MemoryStream stream = new MemoryStream(imageBytes);
            BitmapImage bmpImage = new BitmapImage();
            bmpImage.SetSource(stream);
            return bmpImage;
        }

        /// <summary>
        /// Converts voice stream to a string of bytes
        /// </summary>
        /// <param name="voiceStream"> The voice stream to be converted. </param>
        /// <returns> A string of code that represents the input voice stream. </returns>
        public static string EncodeVoice(MemoryStream voiceStream)
        {
            byte[] voiceBytes = voiceStream.ToArray();
            return Convert.ToBase64String(voiceBytes);
        }

        /// <summary>
        /// Converts a string of bytes to voice stream
        /// </summary>
        /// <param name="voiceStreamString"> The string of bytes to be converted. </param>
        /// <returns> An array of bytes that the input string of voice stream represents. </returns>
        public static byte[] DecodeVoice(string voiceStreamString)
        {
            return Convert.FromBase64String(voiceStreamString);
        }

        /// <summary>
        /// Converts a GPS location to string. 
        /// </summary>
        /// <param name="coordinate"> The GPS location to be converted. </param>
        /// <returns> A string of code that represents the input GPS location. </returns>
        public static string EncodeGPS(GeoCoordinate coordinate) 
        {
            return coordinate.Latitude + "," + coordinate.Longitude;
        }

        /// <summary>
        /// Converts a string to GPS location. 
        /// </summary>
        /// <param name="coordinateString"> The string to be converted. </param>
        /// <returns> An GPS location object that contains the latitude and longitude contained in the input string. </returns>
        public static GeoCoordinate DecodeGPS(string coordinateString)
        {
            string[] coordinateComponents = coordinateString.Split(',');
            double latitude = Convert.ToDouble(coordinateComponents[0]);
            double longitide = Convert.ToDouble(coordinateComponents[1]);
            return new GeoCoordinate(latitude, longitide);
        }

        /// <summary>
        /// Finds the header of a received message. 
        /// </summary>
        /// <param name="body"></param>
        /// <returns> The header text of the message. </returns>
        public static string GetHeader(string body)
        {
            int index = body.IndexOf('@');
            return body.Substring(0, index + 1);
        }

    }
}
