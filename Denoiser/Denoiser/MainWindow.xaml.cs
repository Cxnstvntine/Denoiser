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
            MessageBox.Show("Изображение сохранено");
            App.ImageSaving();
        }

        private void btnFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                
                mainImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}