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
    public partial class Authorisation : Window
    {
        public string ErrorText = "";

        private class AuthRequestForm
        {
            public string Name { get; set; }
            public string Password { get; set; }
            public string Type { get; set; }
        }

        public Authorisation()
        {
            InitializeComponent();
        }

        private async void Enter(object sender, RoutedEventArgs e)
        {
            var tabitem = (TabItem)_TabControl.SelectedItem;
            var way = (string)tabitem.Header;
            HttpClient client = new HttpClient();
            var u = new AuthRequestForm { Name = name.Text, Password = password.Password, Type = way };
            var json = JsonConvert.SerializeObject(u);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:8888/", data);
            var respText = response.Content.ReadAsStringAsync().Result;

            
            if (respText == "ok")
            {
                DialogResult = true;
                Close();
            }
            else
            {
                error_message.Content = respText;
            }
        }

        

    }
}
