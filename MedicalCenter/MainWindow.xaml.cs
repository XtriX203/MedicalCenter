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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void _equipment_Click(object sender, RoutedEventArgs e)
        {
            Equipment nw = new Equipment();
            nw.Show();
            this.Close();
        }

        private void _nurses_Click(object sender, RoutedEventArgs e)
        {
            Nurses nw = new Nurses();
            nw.Show();
            this.Close();
        }

        private void _patients_Click(object sender, RoutedEventArgs e)
        {
            Patients nw = new Patients();
            nw.Show();
            this.Close();
        }

        private void _room_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
