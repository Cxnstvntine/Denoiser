using System.Windows;
using System.Drawing;
using System.Windows.Controls;

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
            Color average;
            //Если это не один из крайних пикселей
            if (i + 20 <= scrBitmap.Height)
            {
                for (int x = i; x < i + 20; x++)
                {
                    if (j + 20 <= scrBitmap.Width)
                    {
                        for (int y = j; y < j + 20; j++)
                        {
                            //Алгоритм
                            count++;
                        }
                    }
                    //Если справа не дошли до края, но находимся внизу картинки
                    else
                    {
                        for (int y = j; y < scrBitmap.Width; j++)
                        {
                            //Алгоритм
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
                            //Алгоритм
                            count++;
                        }
                    }
                    else
                    {
                        for (int y = j; y < scrBitmap.Width; j++)
                        {
                            //Алгоритм
                            count++;
                        }
                    }
                }
            }
            average = average / count;
            return average;
        }

        private bool broken(Color color, int x, int y, Bitmap scrBitmap)
        {
            Color averageColor = findAveragePixel(scrBitmap, x, y);
            //Если значение полей цвета пикселя не больше и не меньше среднего, то вовзращаем true
            if (pixel_is_broken)
            {
                return true;
            }
            return false;
        }
    }
}