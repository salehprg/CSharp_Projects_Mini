using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Helpers
{
    public class ImageHelper
    {
        public static bool IsImageData(string data){
            return data.StartsWith("data:image");
        }
        public static Image LoadImage(string data)
        {
            if(data.StartsWith("data:image"))
            {
                var ind = data.IndexOf(',');
                data = data.Substring(ind + 1);
            }
            byte[] bytes = Convert.FromBase64String(data);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }
        public static void SaveImage(Image img,string path,string ext = "png")
        {
            Bitmap newBitmap = new Bitmap(img);
            img.Dispose();
            img = null;
            switch (ext)
            {
                case "png":
                    newBitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case "jpg":
                    newBitmap.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case "jpeg":
                    newBitmap.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case "bmp":
                    newBitmap.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case "tiff":
                    newBitmap.Save(path, System.Drawing.Imaging.ImageFormat.Tiff);
                    break;
            }
        }
    }
}