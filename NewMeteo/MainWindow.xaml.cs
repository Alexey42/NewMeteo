using Microsoft.Win32;
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
using OpenCvSharp;

namespace NewMeteo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public bool isAuth = false;

        public MainWindow()
        {
            this.Loaded += new RoutedEventHandler(AuthorizeUser);
            InitializeComponent();
            
        }

        private void AuthorizeUser(object sender, RoutedEventArgs e)
        {
            Authorisation auth = new Authorisation();
            auth.ShowDialog();
            if (auth.DialogResult == true)
                isAuth = true;
        }

        private void OpenMap(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            
            if (ofd.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                image.UriSource = new Uri(ofd.FileName);
                image.EndInit();
                img.Source = image;
            }
        }

    }
}
