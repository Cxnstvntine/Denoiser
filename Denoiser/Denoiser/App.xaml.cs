using System;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Denoiser
{
    public partial class App : Application
    {
        public static Bitmap Denoize(System.Drawing.Bitmap scrBitmap)
        {
            for (int i = 0; i < scrBitmap.Height; i++)
            {
                for (int j = 0; j < scrBitmap.Width; j++)
                {
                    if (Broken(scrBitmap.GetPixel(i, j), i , j, scrBitmap))
                    {
                        //Среднее значение цвета вокруг пикселя
                        Color averageColor = FindAveragePixel(scrBitmap,i, j); 
                        //Меняем значение пикселя на среднее
                        try
                        {
                            scrBitmap.SetPixel(i, j, averageColor);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    //Иначе ничего не делаем

                }
            }

            return scrBitmap;
        }
        
        //Ищем среднее значение пикселей вокруг
        private static Color FindAveragePixel(Bitmap scrBitmap, int i, int j)
        {
            int Radius = 2;
            int count = 0;
            int height = scrBitmap.Height;
            int width = scrBitmap.Width;
            int a = 0, r = 0, g = 0, b = 0;
            //Если это не один из крайних пикселей
            if (i + 1 <= height)
            {
                for (int x = i; x < i + Radius; x++)
                {
                    if (x >= height)
                    {
                        goto END;
                    }
                    if (j + 1 <= width)
                    {
                        for (int y = j; y < j + Radius; y++)
                        {
                            if (y >= width)
                            {
                                goto END;
                            }
                            Color temp = scrBitmap.GetPixel(x, y);
                            if (temp.IsEmpty == false)
                            {
                                a += temp.A;
                                r += temp.R;
                                g += temp.G;
                                b += temp.B;
                                count++;
                            }
                        }
                    }
                    //Если справа не дошли до края, но находимся внизу картинки
                    else
                    {
                        for (int y = j; y < width; y++)
                        {
                            if (y >= width)
                            {
                                goto END;
                            }
                            Color temp = scrBitmap.GetPixel(x, y);
                            if (temp.IsEmpty == false)
                            {
                                a += temp.A;
                                r += temp.R;
                                g += temp.G;
                                b += temp.B;
                                count++;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int x = i; x < height; x++)
                {
                    if (j + 1 <= width)
                    {
                        for (int y = j; y < j + Radius; y++)
                        {
                            if (y >= width)
                            {
                                goto END;
                            }
                            Color temp = scrBitmap.GetPixel(x, y);
                            if (temp.IsEmpty == false)
                            {
                                a += temp.A;
                                r += temp.R;
                                g += temp.G;
                                b += temp.B;
                                count++;
                            }
                        }
                    }
                    else
                    {
                        for (int y = j; y < width; y++)
                        {
                            if (y >= width)
                            {
                                goto END;
                            }
                            Color temp = scrBitmap.GetPixel(x, y);
                            if (temp.IsEmpty == false)
                            {
                                a += temp.A;
                                r += temp.R;
                                g += temp.G;
                                b += temp.B;
                                count++;
                            }
                        }
                    }
                }
            }
            END:
            if (count != 0)
            {
                a /= count;
                r /= count;
                g /= count;
                b /= count;
                Color average = Color.FromArgb(a, r, g, b);
                return average;
            }

            return scrBitmap.GetPixel(i, j);
        }

        private static bool Broken(Color color, int x, int y, Bitmap scrBitmap)
        {
            int compressionRatio = 0;
            Color averageColor = FindAveragePixel(scrBitmap, x, y);
            //Если значение полей цвета пикселя не больше и не меньше среднего, то вовзращаем true
            if (color.A >= averageColor.A + compressionRatio || color.R >= averageColor.R + compressionRatio || 
                color.G >= averageColor.G + compressionRatio || color.B >= averageColor.B + compressionRatio)
            {
                return true;
            }
            return false;
        }
    }
}