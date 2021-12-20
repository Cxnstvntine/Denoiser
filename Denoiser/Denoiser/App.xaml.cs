using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media.Animation;

//Надо нормально импортнуть класс
[System.Runtime.InteropServices.ComVisible(true)]
[System.Serializable]
public abstract class Bitmap : Image
{
    public abstract void SetPixel (int x, int y, Color color);
    public abstract Color GetPixel (int x, int y);
}
//Конец класса Bitmap

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
                    if (broken(scrBitmap.GetPixel(i, j), i , j, scrBitmap) == true)
                    {
                        //Ищем среднее значение пикселей вокруг
                        averageColor = findAveragePixel(scrBitmap,i, j);
                        //Меняем значение пикселя на среднее
                        scrBitmap.SetPixel(i, j, averageColor);
                    }
                    //Иначе ничего не делаем
                }
            }            
            return scrBitmap;
        }

        private static Color findAveragePixel(Bitmap scrBitmap, int i, int j)
        {
            int count = 0;
            Color average = (Color.Empty);
            //Если это не один из крайних пикселей
            if (i + 20 <= scrBitmap.Height)
            {
                for (int x = i; x < i + 20; x++)
                {
                    if (j + 20 <= scrBitmap.Width)
                    {
                        for (int y = j; y < j + 20; j++)
                        {
                            average.A += scrBitmap.GetPixel(x, y).A;
                            average.R += scrBitmap.GetPixel(x, y).R;
                            average.G += scrBitmap.GetPixel(x, y).G;
                            average.B += scrBitmap.GetPixel(x, y).B;
                            
                            count++;
                        }
                    }
                    //Если справа не дошли до края, но находимся внизу картинки
                    else
                    {
                        for (int y = j; y < scrBitmap.Width; j++)
                        {
                            average.A += scrBitmap.GetPixel(x, y).A;
                            average.R += scrBitmap.GetPixel(x, y).R;
                            average.G += scrBitmap.GetPixel(x, y).G;
                            average.B += scrBitmap.GetPixel(x, y).B;
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
                            average.A += scrBitmap.GetPixel(x, y).A;
                            average.R += scrBitmap.GetPixel(x, y).R;
                            average.G += scrBitmap.GetPixel(x, y).G;
                            average.B += scrBitmap.GetPixel(x, y).B;
                            count++;
                        }
                    }
                    else
                    {
                        for (int y = j; y < scrBitmap.Width; j++)
                        {
                            average.A += scrBitmap.GetPixel(x, y).A;
                            average.R += scrBitmap.GetPixel(x, y).R;
                            average.G += scrBitmap.GetPixel(x, y).G;
                            average.B += scrBitmap.GetPixel(x, y).B;
                            count++;
                        }
                    }
                }
            }
            average.A = average.A / count;
            average.R = average.R / count;
            average.G = average.G / count;
            average.B = average.B / count;
            return average;
        }

        private bool broken(Color color, int x, int y, Bitmap scrBitmap)
        {
            Color averageColor = findAveragePixel(scrBitmap, x, y);
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