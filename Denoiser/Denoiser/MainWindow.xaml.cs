using System;
using System.Collections.Generic;
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
using System.Windows;
using Microsoft.Win32;

namespace Denoiser
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
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

        private void btnFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.PNG)|*.PNG";
            if (openFileDialog.ShowDialog() == true)
            {
                mainImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}