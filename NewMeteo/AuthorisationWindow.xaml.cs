using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NewMeteo
{
    /// <summary>
    /// Логика взаимодействия для Authorisation.xaml
    /// </summary>
    public partial class AuthorisationWindow : Window
    {
        public string ErrorText = "";

        public AuthorisationWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private async void Enter(object sender, RoutedEventArgs e)
        {
            var tabitem = (TabItem)_TabControl.SelectedItem;
            var way = (string)tabitem.Header;
            button.IsEnabled = false;
            ServerRequest sr = new ServerRequest();
            var resp = await sr.Auth(name.Text, password.Password, way);

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
