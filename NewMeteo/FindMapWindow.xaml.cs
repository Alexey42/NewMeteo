using Newtonsoft.Json;
using OpenCvSharp;
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
using System.Windows.Shapes;
using static NewMeteo.ServerRequestForms;

namespace NewMeteo
{
    /// <summary>
    /// Логика взаимодействия для FindMapWindow.xaml
    /// </summary>
    public partial class FindMapWindow : System.Windows.Window
    {
        public Map result;

        public FindMapWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private async void Enter(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = false;
            ServerRequest sr = new ServerRequest();
            var resp = await sr.GetMap(name.Text);

            if (resp.values.Length == 0)
            {
                error_message.Content = "Not found";
            }
            else
            {
                result = resp;
                DialogResult = true;
                Close();
            }
            button.IsEnabled = true;
        }
    }
}
