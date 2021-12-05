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
    /// Логика взаимодействия для SetSmoothingCoefsWindow.xaml
    /// </summary>
    public partial class SetSmoothingCoefsWindow : Window
    {
        public double range;
        public double decrease;

        public SetSmoothingCoefsWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            range = double.Parse(rangeCoef.Text, System.Globalization.NumberStyles.Any);
            decrease = double.Parse(decreaseCoef.Text, System.Globalization.NumberStyles.Any);
        }

        private void Enter(object sender, RoutedEventArgs e)
        {
            range = double.Parse(rangeCoef.Text, System.Globalization.NumberStyles.Any);
            decrease = double.Parse(decreaseCoef.Text, System.Globalization.NumberStyles.Any);
            DialogResult = true;
            Close();
        }
    }
}
