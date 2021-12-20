using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Denoiser
{
    public partial class App : Application
    {
        public static void Denoize(System.Drawing.Image newImage)
        {
            Bitmap scrBitmap = new (newImage);
            for (int i = 0; i < scrBitmap.Width; i++)
            {
                for (int j = 0; j < scrBitmap.Height; j++)
                {
                    if (Broken(scrBitmap.GetPixel(i, j), i , j, scrBitmap))
                    {
                        //Среднее значение цвета вокруг пикселя
                        Color averageColor = FindAveragePixel(scrBitmap,i, j); 
                        //Меняем значение пикселя на среднее
                        scrBitmap.SetPixel(i, j, averageColor);
                    }
                    //Иначе ничего не делаем

                }
            }            
            newImage = scrBitmap;
        }
        
        //Ищем среднее значение пикселей вокруг
        private static Color FindAveragePixel(Bitmap scrBitmap, int i, int j)
        {
            int count = 0;
            int a = 0, r = 0, g = 0, b = 0;
            //Если это не один из крайних пикселей
            if (i + 20 <= scrBitmap.Height)
            {
                for (int x = i; x < i + 20; x++)
                {
                    if (j + 20 <= scrBitmap.Width)
                    {
                        for (int y = j; y < j + 20; j++)
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
                        for (int y = j; y < scrBitmap.Width; j++)
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
                for (int x = i; x < scrBitmap.Height; x++)
                {
                    if (j + 20 <= scrBitmap.Width)
                    {
                        for (int y = j; y < j + 20; j++)
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
                        for (int y = j; y < scrBitmap.Width; j++)
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
            a /= count;
            r /= count;
            g /= count;
            b /= count;
            Color average = Color.FromArgb(a, r, g, b);
            return average;
        }

        private static bool Broken(Color color, int x, int y, Bitmap scrBitmap)
        {
            Color averageColor = FindAveragePixel(scrBitmap, x, y);
            //Если значение полей цвета пикселя не больше и не меньше среднего, то вовзращаем true
            if (color.A >= averageColor.A + 10 || color.R >= averageColor.R + 10 || color.G >= averageColor.G + 10 ||
                color.B >= averageColor.B + 10)
            {
                return true;
            }
            return false;
        }
    }
}