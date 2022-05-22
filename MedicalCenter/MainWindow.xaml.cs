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

namespace MedicalCenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceReference.WebService1SoapClient c = new ServiceReference.WebService1SoapClient();
        bool IsInspector;
        public MainWindow(bool isIns)
        {
            this.IsInspector = isIns;
            InitializeComponent();
            if(!isIns)
            {
                _users.Visibility = Visibility.Visible;
            }
            showWeather();
        }

        private void _equipment_Click(object sender, RoutedEventArgs e)
        {
            Equipment nw = new Equipment(IsInspector);
            nw.Show();
            this.Close();
        }

        private void _nurses_Click(object sender, RoutedEventArgs e)
        {
            Nurses nw = new Nurses(IsInspector);
            nw.Show();
            this.Close();
        }

        private void _patients_Click(object sender, RoutedEventArgs e)
        {
            Patients nw = new Patients(IsInspector);
            nw.Show();
            this.Close();
        }

        private void _room_Click(object sender, RoutedEventArgs e)
        {
            Room nw = new Room(IsInspector);
            nw.Show();
            this.Close();
        }

        private void quit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private async void showWeather()
        {
            temperature.Text = await c.getWeatherAsync() +" C";
        }
        private void _users_Click(object sender, RoutedEventArgs e)
        {
            Users nw = new Users();
            nw.Show();
            this.Close();
        }
    }
}
