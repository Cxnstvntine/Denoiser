using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO.Enumeration;
using Microsoft.Win32;

namespace Denoiser
{
    public partial class App : Application
    {
        public static Bitmap Denoize(Bitmap scrBitmap)
        {    
            //Среднее значение цвета вокруг пикселя
            Color averageColor = (Color.Black);
            for (int i = 0; i < scrBitmap.Width; i++)
            {
                for (int j = 0; j < scrBitmap.Height; j++)
                {
                    averageColor = findAveragePixel();
                    //scrBitmap.pixel = averageColor;
                }
            }            
            return scrBitmap;
        }

        private static int findAveragePixel(Bitmap scrBitmap)
        {
            int count = 0;
            int average = 0;
            int Sum = 0;
            //for x+-delta:
                //for y+-delta:
                    //Sum += scrBitmap.Pixel(x, y)
                    //count ++
            average = Sum / count;
            return average;
        }
    }
}