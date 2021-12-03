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

namespace NewMeteo
{
    /// <summary>
    /// Логика взаимодействия для SendMapWindow.xaml
    /// </summary>
    public partial class SendMapWindow : Window
    {
        Map map;

        public SendMapWindow(Map _m)
        {
            map = _m;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private async void Enter(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = false;
            ServerRequest sr = new ServerRequest();
            var resp = await sr.SendMap(name.Text, map);

            if (resp == "ok")
            {
                DialogResult = true;
                Close();
            }
            else
            {
                error_message.Content = resp;
            }
            button.IsEnabled = true;
        }
    }
}
