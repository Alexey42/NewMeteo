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

namespace NewMeteo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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

    }
}
