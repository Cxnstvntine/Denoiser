using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace Denoiser
{
    public partial class MainWindow : Window
    {
        private static Bitmap _bitmap = null;
        private bool _loaded;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_loaded)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Image files (*.PNG)|*.PNG";
                if (saveFileDialog.ShowDialog() == true)
                {
                    PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
                    pngBitmapEncoder.Frames.Add(BitmapFrame.Create(mainImage.Source as BitmapSource));
                    FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                    pngBitmapEncoder.Save(fileStream);
                }
                MessageBox.Show("Изображение сохранено");
            }
            else
            {
                MessageBox.Show("Сначала загрузите изображение");
            }
        }

        private void btnFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.PNG)|*.PNG";
            if (openFileDialog.ShowDialog() == true)
            {
                _bitmap = (Bitmap) System.Drawing.Image.FromFile(openFileDialog.FileName);
                mainImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
            _loaded = true;
        }

        private void btnDenoise_Click(object sender, RoutedEventArgs e)
        {
            if (_loaded)
            {
                _bitmap = App.Denoise(_bitmap);
                
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Image files (*.PNG)|*.PNG";
                if (saveFileDialog.ShowDialog() == true)
                {
                    _bitmap.Save(saveFileDialog.FileName);
                }
                MessageBox.Show("Изображение сохранено");
            }
            else
            {
                MessageBox.Show("Сначала загрузите изображение");
            } 
        }
        
    }
}