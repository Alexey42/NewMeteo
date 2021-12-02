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
    /// Логика взаимодействия для ConfirmHeightWindow.xaml
    /// </summary>
    public partial class ConfirmHeightWindow : Window
    {
        public float result = 0;

        public ConfirmHeightWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            result = float.Parse(TextBox.Text);
            this.DialogResult = true;
        }
    }
}
