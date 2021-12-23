using System;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Denoiser
{
    public partial class App : Application
    {
       
        public static Bitmap Denoise(Bitmap scrBitmap)
        {
            for (var i = 0; i < scrBitmap.Width; i++)
            {
                for (var j = 0; j < scrBitmap.Height; j++)
                {
                    if(Broken(scrBitmap.GetPixel(i, j), i , j, scrBitmap))
                    {
                        //Среднее значение цвета вокруг пикселя
                        Color averageColor = FindAveragePixel(scrBitmap,i, j); 
                        //Меняем значение пикселя на среднее
                        scrBitmap.SetPixel(i, j, averageColor);
                    }
                    //Иначе ничего не делаем

                }
            }

            return scrBitmap;
        }
        
        //Ищем среднее значение пикселей вокруг
        private static Color FindAveragePixel(Bitmap scrBitmap, int i, int j)
        {
            const int radius = 1;
            var count = 0;
            int a = 0, r = 0, g = 0, b = 0;
            //Если это не один из крайних пикселей
            if (i + 1 <= scrBitmap.Height)
            {
                for (var x = i; x < i + radius; x++)
                {
                    if (j + 1 <= scrBitmap.Width)
                    {
                        for (var y = j; y < j + radius; j++)
                        {
                            a += scrBitmap.GetPixel(x, y).A;
                            r += scrBitmap.GetPixel(x, y).R;
                            g += scrBitmap.GetPixel(x, y).G;
                            b += scrBitmap.GetPixel(x, y).B;
                            
                            count++;
                        }
                    }
                    //Если справа не дошли до края, но находимся внизу картинки
                    else
                    {
                        for (var y = j; y < scrBitmap.Width; j++)
                        {
                            a += scrBitmap.GetPixel(x, y).A;
                            r += scrBitmap.GetPixel(x, y).R;
                            g += scrBitmap.GetPixel(x, y).G;
                            b += scrBitmap.GetPixel(x, y).B;
                            count++;
                        }
                    }
                }
            }
            else
            {
                for (var x = i; x < scrBitmap.Height; x++)
                {
                    if (j + 1 <= scrBitmap.Width)
                    {
                        for (var y = j; y < j + radius; j++)
                        {
                            a += scrBitmap.GetPixel(x, y).A;
                            r += scrBitmap.GetPixel(x, y).R;
                            g += scrBitmap.GetPixel(x, y).G;
                            b += scrBitmap.GetPixel(x, y).B;
                            count++;
                        }
                    }
                    else
                    {
                        for (var y = j; y < scrBitmap.Width; j++)
                        {
                            a += scrBitmap.GetPixel(x, y).A;
                            r += scrBitmap.GetPixel(x, y).R;
                            g += scrBitmap.GetPixel(x, y).G;
                            b += scrBitmap.GetPixel(x, y).B;
                            count++;
                        }
                    }
                }
            }

            if (count != 0)
            {
                a /= count;
                r /= count;
                g /= count;
                b /= count;
                var average = Color.FromArgb(a, r, g, b);
                return average;
            }

            return scrBitmap.GetPixel(i, j);
        }

        private static bool Broken(Color color, int x, int y, Bitmap scrBitmap)
        {
            var averageColor = FindAveragePixel(scrBitmap, x, y);
            //Если значение полей цвета пикселя не больше и не меньше среднего, то вовзращаем true
            return (color.A >= averageColor.A + 10 || color.R >= averageColor.R + 10 ||
                    color.G >= averageColor.G + 10 ||
                    color.B >= averageColor.B + 10);
        }
        
    }
}