using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace CRYPTOGRAPHYimage
{
    class libraryfile
    {
        public static byte[] formhex(string hextext)
        {
           byte[] arra = new byte[0]; 
            try
            {
                String[] arr = hextext.Split('-');
                byte[] array = new byte[arr.Length];
                for (int i = 0; i < arr.Length; i++)
                    array[i] = Convert.ToByte(arr[i], 16);
                return array;
            }
            catch (Exception e)
            {
                string message = "Wrong Input given";
                string title = "ERROR Generated";
                System.Windows.Forms.MessageBox.Show(message,title);
                return arra;
            }

        }

        public static bool IsPrime(int number)
        {
            if (number < 2) return false;
            if (number % 2 == 0) return (number == 2);
            int root = (int)Math.Sqrt((double)number);
            for (int i = 3; i <= root; i += 2)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }

        public static Bitmap ConvertByteToImage(byte[] bytes)
        {
            try
            {
                return (new Bitmap(Image.FromStream(new MemoryStream(bytes))));
            }
            catch (Exception e)
            {
                return (new Bitmap(Image.FromFile("D:\\bat.bmp",true)));
            }

        }

        public static byte[] ConvertImageToByte(Image My_Image)
        {
            MemoryStream m1 = new MemoryStream();
            new Bitmap(My_Image).Save(m1, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] header = new byte[] { 255, 216 };
            header = m1.ToArray();
            return (header);
        }
    }
}
