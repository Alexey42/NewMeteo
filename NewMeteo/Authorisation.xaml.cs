using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для Authorisation.xaml
    /// </summary>
    public partial class Authorisation : Window
    {
        public string ErrorText = "";

        public Authorisation()
        {
            InitializeComponent();
        }

        private void Enter(object sender, RoutedEventArgs e)
        {
            ErrorText = "";
            DBContext db = new DBContext();
            User user = FindUser(db, name.Text);
            var way = (TabItem)_TabControl.SelectedItem;

            if ((string)way.Header == "Sign in")
            {
                if (user == null || user.Password != password.Password)
                    ErrorText = "Wrong name or password";
                else
                {
                    this.DialogResult = true;
                    this.Close();
                }
            }
            else
            {
                if (user != null)
                    ErrorText = "Name is already registered";
                else
                {
                    try
                    {
                        db.Users.Add(new User { Name = name.Text, Password = password.Password, Role = "Developer" });
                        db.SaveChanges();
                    }
                    catch
                    {
                        ErrorText = "Database isn't responding";
                    }
                }
            }

            error_message.Content = ErrorText;
        }

        private User FindUser(DBContext db, string param)
        {
            var list = db.Users.ToList();
            foreach (var user in list)
            {
                if (user.Name == param)
                    return user;
            }
            return null;
        }

    }
}
